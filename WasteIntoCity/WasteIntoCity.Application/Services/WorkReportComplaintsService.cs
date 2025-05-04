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

        private readonly List<ScoreSettingsEnum> _scoreSettingsIdsForReject = new List<ScoreSettingsEnum>
        {
            ScoreSettingsEnum.WorkApplicationRankingSubstracting,
            ScoreSettingsEnum.WorkApplicationNegativeAddingMultiplier,
            ScoreSettingsEnum.UserBanRankingAtLeast,
        };

        private readonly List<ScoreSettingsEnum> _scoreSettingsIdsForConfirm = new List<ScoreSettingsEnum>
        {
            ScoreSettingsEnum.WorkApplicationRankingAdding,
            ScoreSettingsEnum.WorkApplicationNegativeSubstracting,
        };

        public WorkReportComplaintsService(IWorkReportComplaintsRepository workReportComplaintsRepository,
            IScoreSettingsTypesRepository scoreSettingsTypeRepository, IUsersRepository usersRepository, IWorksRepository worksRepository,
            RefreshTokensRepository refreshTokensRepository)
        {
            _workReportComplaintsRepository = workReportComplaintsRepository;
            _scoreSettingsTypeRepository = scoreSettingsTypeRepository;
            _usersRepository = usersRepository;
            _worksRepository = worksRepository;
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task CreateAsync(string title, string description, Guid worksId, Guid fromUsersId)
        {
            WorkReportComplaint workReportComplaint = WorkReportComplaint.Create(Guid.NewGuid(), Title.Create(title),
                Description.Create(description), DateTime.UtcNow, worksId, fromUsersId, WorkReportStatusEnum.Pending, null, null);

            await _workReportComplaintsRepository.CreateAsync(workReportComplaint);
        }

        public async Task<WorkReportComplaint> GetAsync(Guid id)
        {
            return await _workReportComplaintsRepository.FindByIdAsync(id);
        }

        public async Task<WorkReportComplaint> GetFromQueueAsync()
        {
            return await _workReportComplaintsRepository.FindPendingWithFromUserAndImageNamesByStartedDatetimeAscending();
        }

        public async Task ConfirmAsync(Guid id)
        {
            WorkReportComplaint workReportComplaint = await _workReportComplaintsRepository.FindByIdAsync(id);

            if (workReportComplaint.WorkReportStatusTypesId != WorkReportStatusEnum.Pending)
            {
                throw new ValueOutOfRangeException<WorkReportStatusEnum>(
                   nameof(WorkReportComplaint), $"Work report complaint is not in {nameof(WorkReportStatusEnum.Pending)} state", 67);
            }

            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await _scoreSettingsTypeRepository.
                FindAllValuesByIdsAsync(_scoreSettingsIdsForConfirm);

            User complaintUser = await _usersRepository.FindByIdAsync(workReportComplaint.FromUsersId);

            int negativeScore = Math.Max(0, complaintUser.NegativeScore -
                scoreSettingsValues[ScoreSettingsEnum.WorkNegativeSubstracting]);

            int ranking = complaintUser.Ranking + scoreSettingsValues[ScoreSettingsEnum.WorkComplaintUserRankingAdding];

            List<User> updatedUsers = new List<User>();

            updatedUsers.Add(User.Create(complaintUser.Id, complaintUser.Nickname, complaintUser.Email, complaintUser.Password,
                ranking, complaintUser.Roles, negativeScore, complaintUser.IsBanned, null, null));

            Work work = await _worksRepository.FindWithParticipantsByIdAsync(id);

            if (work.Participants == null)
            {
                throw new NullValueServerException(27, "participants", null);
            }


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
                    ranking = participant.Ranking - (scoreSettingsValues[ScoreSettingsEnum.WorkApplicationRankingSubstracting] *
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

            int negativeScore = Math.Max(0, complaintUser.NegativeScore -
                scoreSettingsValues[ScoreSettingsEnum.WorkNegativeSubstracting]);

            int ranking = complaintUser.Ranking + scoreSettingsValues[ScoreSettingsEnum.WorkComplaintUserRankingAdding];

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
