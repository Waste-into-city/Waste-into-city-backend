using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Application.Services
{
    public class WorkReportComplaintsService : IWorkReportComplaintsService
    {
        private readonly IWorkReportComplaintsRepository _workReportComplaintsRepository;
        private readonly IScoreSettingsTypesRepository _scoreSettingsTypeRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IWorksRepository _worksRepository;
        private readonly RefreshTokensRepository _refreshTokensRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly IWorkReportResultsRepository _workReportResultsRepository;

        private readonly List<ScoreSettingsEnum> _scoreSettingsIdsForReject = new List<ScoreSettingsEnum>
        {
            ScoreSettingsEnum.WorkComplaintUserNegativeAddingMultiplier,
            ScoreSettingsEnum.UserBanRankingAtLeast,
            ScoreSettingsEnum.WorkComplaintUserRankingSubstracting,
        };

        private readonly List<ScoreSettingsEnum> _scoreSettingsIdsForConfirm = new List<ScoreSettingsEnum>
        {
            ScoreSettingsEnum.UserBanRankingAtLeast,
            ScoreSettingsEnum.WorkComplaintUserNegativeSubstracting,
            ScoreSettingsEnum.WorkComplaintUserRankingAdding,
            ScoreSettingsEnum.WorkComplaintParticipantRankingSubstracting
        };

        public WorkReportComplaintsService(IWorkReportComplaintsRepository workReportComplaintsRepository,
            IScoreSettingsTypesRepository scoreSettingsTypeRepository, IUsersRepository usersRepository, IWorksRepository worksRepository,
            RefreshTokensRepository refreshTokensRepository, IImagesRepository imagesRepository, IWorkReportResultsRepository workReportResultsRepository)
        {
            _workReportComplaintsRepository = workReportComplaintsRepository;
            _scoreSettingsTypeRepository = scoreSettingsTypeRepository;
            _usersRepository = usersRepository;
            _worksRepository = worksRepository;
            _refreshTokensRepository = refreshTokensRepository;
            _imagesRepository = imagesRepository;
            _workReportResultsRepository = workReportResultsRepository;
        }

        public async Task CreateAsync(string title, string description, Guid worksId, Guid fromUsersId, List<string> imageNamesLines)
        {
            WorkReportComplaint workReportComplaint = WorkReportComplaint.Create(Guid.NewGuid(), Title.Create(title),
                Description.Create(description), DateTime.UtcNow, worksId, fromUsersId, WorkReportStatusEnum.Pending, null, null);

            List<ImageName> imageNames = imageNamesLines.Select(i => ImageName.Create(i)).ToList();

            await _workReportComplaintsRepository.CreateAsync(workReportComplaint);
            await _imagesRepository.UpdateWorkReportComplaintsIdByNamesAsync(imageNames, workReportComplaint.Id);
        }

        public async Task<WorkReportComplaint> GetAsync(Guid id)
        {
            return await _workReportComplaintsRepository.FindByIdAsync(id);
        }

        public async Task<(WorkReportComplaint, Guid)> GetFromQueueAsync()
        {
            WorkReportComplaint workReportComplaint = await _workReportComplaintsRepository.FindPendingWithFromUserAndImageNamesByStartedDatetimeAscending();

            WorkReportResult workReportResult = await _workReportResultsRepository.FindByWorksIdAsync(workReportComplaint.WorksId);

            return (workReportComplaint, workReportResult.Id);
        }

        public async Task ConfirmAsync(Guid id)
        {
            WorkReportComplaint workReportComplaint = await _workReportComplaintsRepository.FindByIdAsync(id);

            Work work = await _worksRepository.FindWithParticipantsByIdAsync(workReportComplaint.WorksId);

            if (work.Participants == null)
            {
                throw new NullValueServerException(27, "participants", null);
            }

            if (work.WorkStatusTypesId != WorkStatusEnum.FinishedSuccessfully)
            {
                throw new NullValueServerException(50, "work status types id", null);
            }

            if (workReportComplaint.WorkReportStatusTypesId != WorkReportStatusEnum.Pending)
            {
                throw new ValueOutOfRangeException<WorkReportStatusEnum>(
                   nameof(WorkReportComplaint), $"Work report complaint is not in {nameof(WorkReportStatusEnum.Pending)} state", 67);
            }

            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await _scoreSettingsTypeRepository.
                FindAllValuesByIdsAsync(_scoreSettingsIdsForConfirm);

            User complaintUser = await _usersRepository.FindByIdAsync(workReportComplaint.FromUsersId);

            int negativeScore = Math.Max(0, complaintUser.NegativeScore -
                scoreSettingsValues[ScoreSettingsEnum.WorkComplaintUserNegativeSubstracting]);

            int ranking = complaintUser.Ranking + scoreSettingsValues[ScoreSettingsEnum.WorkComplaintUserRankingAdding];

            List<User> updatedUsers = new List<User>();

            updatedUsers.Add(User.Create(complaintUser.Id, complaintUser.Nickname, complaintUser.Email, complaintUser.Password,
                ranking, complaintUser.Roles, negativeScore, complaintUser.IsBanned, null, null));

            for (int i = 0; i < work.Participants.Count; i++)
            {
                User participant = work.Participants[i];

                if (participant.NegativeScore == 0)
                {
                    negativeScore = 1;
                    ranking = participant.Ranking - (scoreSettingsValues[ScoreSettingsEnum.WorkComplaintParticipantRankingSubstracting]
                        * participant.NegativeScore);
                }
                else
                {
                    ranking = participant.Ranking - (scoreSettingsValues[ScoreSettingsEnum.WorkComplaintParticipantRankingSubstracting] *
                        participant.NegativeScore);
                    negativeScore = participant.NegativeScore *
                        scoreSettingsValues[ScoreSettingsEnum.WorkComplaintParticipantNegativeAddingMultiplier];
                }

                bool isBanned = false;
                if (ranking <= scoreSettingsValues[ScoreSettingsEnum.UserBanRankingAtLeast])
                {
                    isBanned = true;

                    await _refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, participant.Id);
                }

                updatedUsers.Add(User.Create(participant.Id, participant.Nickname, participant.Email, participant.Password,
                    ranking, null, negativeScore, isBanned, null, null));
            }

            await _usersRepository.UpdateAllByIdAsync(updatedUsers);
            await _worksRepository.UpdateStatusIdByIdAsync(work.Id, WorkStatusEnum.Closed);
            await _workReportComplaintsRepository.UpdateWorkReportStatusTypesIdByIdAsync(id, WorkReportStatusEnum.Accepted);
        }

        public async Task RejectAsync(Guid id)
        {
            WorkReportComplaint workReportComplaint = await _workReportComplaintsRepository.FindByIdAsync(id);

            if (workReportComplaint.WorkReportStatusTypesId != WorkReportStatusEnum.Pending)
            {
                throw new ValueOutOfRangeException<WorkReportStatusEnum>(
                   nameof(WorkReportComplaint), $"Work report complaint is not in {nameof(WorkReportStatusEnum.Pending)} state", 68);
            }

            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await _scoreSettingsTypeRepository.FindAllValuesByIdsAsync(_scoreSettingsIdsForReject);

            User complaintUser = await _usersRepository.FindByIdAsync(workReportComplaint.FromUsersId);

            int negativeScore;
            int ranking;
            if (complaintUser.NegativeScore == 0)
            {
                negativeScore = 1;
                ranking = complaintUser.Ranking - (scoreSettingsValues[ScoreSettingsEnum.WorkComplaintUserRankingSubstracting]
                    * complaintUser.NegativeScore);
            }
            else
            {
                ranking = complaintUser.Ranking - (scoreSettingsValues[ScoreSettingsEnum.WorkComplaintUserRankingSubstracting] *
                    complaintUser.NegativeScore);
                negativeScore = complaintUser.NegativeScore *
                    scoreSettingsValues[ScoreSettingsEnum.WorkComplaintUserNegativeAddingMultiplier];
            }

            bool isBanned = false;
            if (ranking <= scoreSettingsValues[ScoreSettingsEnum.UserBanRankingAtLeast])
            {
                isBanned = true;

                await _refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, complaintUser.Id);
            }

            User complaintUserUpdated = User.Create(complaintUser.Id, complaintUser.Nickname, complaintUser.Email, complaintUser.Password,
                ranking, null, negativeScore, isBanned, null, null);

            await _usersRepository.UpdateByIdAsync(complaintUserUpdated);
            await _workReportComplaintsRepository.UpdateWorkReportStatusTypesIdByIdAsync(id, WorkReportStatusEnum.Denied);
        }
    }
}
