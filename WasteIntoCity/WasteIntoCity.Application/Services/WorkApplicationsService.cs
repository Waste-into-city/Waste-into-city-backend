using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Application.Services
{
    public class WorkApplicationsService : IWorkApplicationsService
    {
        private const int WORK_APPLICATION_RANKING_SUBSTRACTING = 2;
        private const int WORK_APPLICATION_NEGATIVE_ADDING_MULTIPLIER = 2;
        private const int WORK_APPLICATION_RANKING_ADDING = 1;
        private const int WORK_APPLICATION_NEGATIVE_SUBSTRACTING = 2;

        private const int USER_BAN_RANKING_AT_LEAST = -30;

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
        private readonly IScoreSettingsTypeRepository _scoreSettingsTypeRepository;


        public WorkApplicationsService(IWorkApplicationsRepository workApplicationsRepository, ICoordinatesRepository coordinatesRepository,
            IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository, IWorkComplexityTypesRepository workComplexityTypesRepository,
            IWorksRepository worksRepository, IScoreSettingsTypeRepository scoreSettingsTypeRepository)
        {
            _workApplicationsRepository = workApplicationsRepository;
            _coordinatesRepository = coordinatesRepository;
            _usersRepository = usersRepository;
            _refreshTokensRepository = refreshTokensRepository;
            _workComplexityTypesRepository = workComplexityTypesRepository;
            _worksRepository = worksRepository;
            _scoreSettingsTypeRepository = scoreSettingsTypeRepository;
        }

        public async Task CreateOwnAsync(string title, string description, int workComplexityId, string lat, string lng,
            Guid userId)
        {
            Coordinates coordinates = Coordinates.Create(Guid.NewGuid(), lat, lng);

            await _coordinatesRepository.AddAsync(coordinates);

            WorkApplication workApplication = WorkApplication.Create(Guid.NewGuid(), Title.Create(title), Description.Create(description),
                (WorkComplexityEnum)workComplexityId, coordinates.Id, DateTime.UtcNow, userId, WorkReportStatusEnum.Pending);

            await _workApplicationsRepository.AddAsync(workApplication);
        }

        public async Task RejectAsync(Guid workApplicationsId)
        {
            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await _scoreSettingsTypeRepository.FindAllValuesByIdsAsync(_scoreSettingsIdsForReject);

            WorkApplication workApplication = await _workApplicationsRepository.FindById(workApplicationsId);

            if (workApplication.WorkReportStatusTypesId != WorkReportStatusEnum.Pending)
            {
                throw new ValueOutOfRangeException<WorkReportStatusEnum>(
                    nameof(WorkApplication), $"Application is not in {nameof(WorkReportStatusEnum.Pending)} state");
            }

            User user = await _usersRepository.FindByIdWithRolesAsync(workApplication.FromUsersId);

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
                negativeScore = user.NegativeScore * scoreSettingsValues[ScoreSettingsEnum.WorkNegativeAddingMultiplier];
            }

            bool isBanned = false;

            if (ranking <= scoreSettingsValues[ScoreSettingsEnum.UserBanRankingAtLeast])
            {
                isBanned = true;

                await _refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, user.Id);
            }

            User updatedUser = User.Create(user.Id, user.Nickname, user.Email, user.Password, ranking, user.Roles, negativeScore, isBanned);

            await _usersRepository.UpdateAsync(updatedUser);

            await _workApplicationsRepository.UpdateWorkReportStatusTypesIdByIdAsync(workApplicationsId, WorkReportStatusEnum.Denied);
        }

        public async Task ConfirmAsync(Guid workApplicationsId)
        {
            Dictionary<ScoreSettingsEnum, int> scoreSettingsValues = await _scoreSettingsTypeRepository.FindAllValuesByIdsAsync(_scoreSettingsIdsForConfirm);

            WorkApplication workApplication = await _workApplicationsRepository.FindById(workApplicationsId);

            if (workApplication.WorkReportStatusTypesId != WorkReportStatusEnum.Pending)
            {
                throw new ValueOutOfRangeException<WorkReportStatusEnum>(
                    nameof(WorkApplication), $"Application is not in {nameof(WorkReportStatusEnum.Pending)} state");
            }

            User user = await _usersRepository.FindByIdWithRolesAsync(workApplication.FromUsersId);

            int ranking = user.Ranking + scoreSettingsValues[ScoreSettingsEnum.WorkApplicationRankingAdding];

            int negativeScore = Math.Max(user.NegativeScore - scoreSettingsValues[ScoreSettingsEnum.WorkApplicationNegativeSubstracting], 0);

            User updatedUser = User.Create(user.Id, user.Nickname, user.Email, user.Password, ranking, user.Roles, negativeScore, user.IsBanned);

            await _usersRepository.UpdateAsync(updatedUser);

            WorkComplexityType workComplexityType = await _workComplexityTypesRepository.FindById((int)workApplication.WorkComplexityTypesId);

            DateTime startDateTime = DateTime.UtcNow;

            Work work = Work.Create(Guid.NewGuid(), workApplication.Title, workApplication.Description, startDateTime,
                startDateTime.AddHours(workComplexityType.DurationHours), workApplication.WorkComplexityTypesId, WorkStatusEnum.Avaliable,
                workApplication.CoordinatesId, null, null, null, null, null);

            await _worksRepository.AddAsync(work);

            await _workApplicationsRepository.UpdateWorkReportStatusTypesIdByIdAsync(workApplicationsId, WorkReportStatusEnum.Accepted);
        }
    }
}
