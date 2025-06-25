using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Application.Services
{
    public class WorkApplicationsService : IWorkApplicationsService
    {
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

        private readonly IWorkApplicationsRepository _workApplicationsRepository;
        private readonly ICoordinatesRepository _coordinatesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly RefreshTokensRepository _refreshTokensRepository;
        private readonly IWorkComplexityTypesRepository _workComplexityTypesRepository;
        private readonly IWorksRepository _worksRepository;
        private readonly IScoreSettingsTypesRepository _scoreSettingsTypeRepository;
        private readonly IImagesRepository _imagesRepository;


        public WorkApplicationsService(IWorkApplicationsRepository workApplicationsRepository, ICoordinatesRepository coordinatesRepository,
            IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository, IWorkComplexityTypesRepository workComplexityTypesRepository,
            IWorksRepository worksRepository, IScoreSettingsTypesRepository scoreSettingsTypeRepository, IImagesRepository imagesRepository)
        {
            _workApplicationsRepository = workApplicationsRepository;
            _coordinatesRepository = coordinatesRepository;
            _usersRepository = usersRepository;
            _refreshTokensRepository = refreshTokensRepository;
            _workComplexityTypesRepository = workComplexityTypesRepository;
            _worksRepository = worksRepository;
            _scoreSettingsTypeRepository = scoreSettingsTypeRepository;
            _imagesRepository = imagesRepository;
        }

        public async Task CreateOwnAsync(string title, string description, int workComplexityId, decimal lat, decimal lng, List<int> trashTypesIds,
            List<string> imageNamesLines, Guid userId)
        {
            Coordinates coordinates = Coordinates.Create(Guid.NewGuid(), lat, lng);

            List<TrashEnum> trashTypes = new List<TrashEnum>();
            foreach (int trashTypesId in trashTypesIds)
            {
                EnumOperationsExtension.CheckEnumIntValue<TrashEnum>(trashTypesId, "trash type");
                trashTypes.Add((TrashEnum)trashTypesId);
            }

            List<ImageName> imageNames = imageNamesLines.Select(i => ImageName.Create(i)).ToList();

            WorkApplication workApplication = WorkApplication.Create(Guid.NewGuid(), Title.Create(title), Description.Create(description),
                (WorkComplexityEnum)workComplexityId, coordinates.Id, DateTime.UtcNow, userId, WorkReportStatusEnum.Pending, trashTypes,
                null, null, null);

            await _coordinatesRepository.CreateAsync(coordinates);
            await _workApplicationsRepository.CreateAsync(workApplication);
            await _imagesRepository.UpdateWorkApplicationsIdByNamesAsync(imageNames, workApplication.Id);
        }

        public async Task RejectAsync(Guid workApplicationsId)
        {
            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await _scoreSettingsTypeRepository.FindAllValuesByIdsAsync(_scoreSettingsIdsForReject);

            WorkApplication workApplication = await _workApplicationsRepository.FindById(workApplicationsId);

            if (workApplication.WorkReportStatusTypesId != WorkReportStatusEnum.Pending)
            {
                throw new ValueOutOfRangeException<WorkReportStatusEnum>(
                    nameof(WorkApplication), $"Application is not in {nameof(WorkReportStatusEnum.Pending)} state", 50);
            }

            User user = await _usersRepository.FindByIdAsync(workApplication.FromUsersId);

            int negativeScore;
            int ranking;

            if (user.NegativeScore == 0)
            {
                negativeScore = 1;
                ranking = user.Ranking - (scoreSettingsValues[ScoreSettingsEnum.WorkApplicationRankingSubstracting] * user.NegativeScore);
            }
            else
            {
                ranking = user.Ranking - (scoreSettingsValues[ScoreSettingsEnum.WorkApplicationRankingSubstracting] * user.NegativeScore);
                negativeScore = user.NegativeScore * scoreSettingsValues[ScoreSettingsEnum.WorkApplicationNegativeAddingMultiplier];
            }

            bool isBanned = false;

            if (ranking <= scoreSettingsValues[ScoreSettingsEnum.UserBanRankingAtLeast])
            {
                isBanned = true;

                await _refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, user.Id);
            }

            User updatedUser = User.Create(user.Id, user.Nickname, user.Email, user.Password, ranking, null, negativeScore, isBanned,
                null, null);

            await _usersRepository.UpdateByIdAsync(updatedUser);

            await _workApplicationsRepository.UpdateWorkReportStatusTypesIdByIdAsync(workApplicationsId, WorkReportStatusEnum.Denied);
        }

        public async Task ConfirmAsync(Guid workApplicationsId)
        {
            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await _scoreSettingsTypeRepository.FindAllValuesByIdsAsync(_scoreSettingsIdsForConfirm);

            WorkApplication workApplication = await _workApplicationsRepository.FindWithTrashTypesAndImageNameById(workApplicationsId);

            if (workApplication.ImageNames == null)
            {
                throw new NullValueServerException(33, "image names", null);
            }

            if (workApplication.WorkReportStatusTypesId != WorkReportStatusEnum.Pending)
            {
                throw new ValueOutOfRangeException<WorkReportStatusEnum>(
                    nameof(WorkApplication), $"Application is not in {nameof(WorkReportStatusEnum.Pending)} state", 51);
            }

            User user = await _usersRepository.FindByIdAsync(workApplication.FromUsersId);

            int ranking = user.Ranking + scoreSettingsValues[ScoreSettingsEnum.WorkApplicationRankingAdding];

            int negativeScore = Math.Max(user.NegativeScore - scoreSettingsValues[ScoreSettingsEnum.WorkApplicationNegativeSubstracting], 0);

            User updatedUser = User.Create(user.Id, user.Nickname, user.Email, user.Password, ranking, user.Roles, negativeScore, user.IsBanned,
                null, null);

            await _usersRepository.UpdateByIdAsync(updatedUser);

            WorkComplexityType workComplexityType = await _workComplexityTypesRepository.FindById((int)workApplication.WorkComplexityTypesId);

            Work work = Work.Create(Guid.NewGuid(), workApplication.Title, workApplication.Description, null,
                null, workApplication.WorkComplexityTypesId, WorkStatusEnum.NotFinished,
                workApplication.CoordinatesId, null, null, null, null, null, null, null, null, workApplication.TrashTypesIds);

            await _worksRepository.CreateAsync(work);

            await _imagesRepository.UpdateWorksIdByNamesAsync(workApplication.ImageNames, work.Id);

            await _workApplicationsRepository.UpdateWorkReportStatusTypesIdByIdAsync(workApplicationsId, WorkReportStatusEnum.Accepted);
        }

        public async Task<WorkApplication> GetFromQueueAsync()
        {
            return await _workApplicationsRepository.FindPendingWithFromUserAndTrashTypesIdsAndImageNamesAndCoordinatesByStartedDatetimeAscending();
        }
    }
}
