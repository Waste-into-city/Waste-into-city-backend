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
        private const int RANKING_BASE_DECREASING = 2;
        private const int NEGATIVE_SCORE_MULTIPLIER_INCREASING = 2;

        private const int USER_BAR_RANKING_AT_LEAST = -30;

        private const int RANKING_BASE_INCREASING = 1;
        private const int NEGATIVE_SCORE_DECREASING = 2;

        private readonly IWorkApplicationsRepository _workApplicationsRepository;
        private readonly ICoordinatesRepository _coordinatesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly RefreshTokensRepository _refreshTokensRepository;
        private readonly IWorkComplexityTypesRepository _workComplexityTypesRepository;
        private readonly IWorksRepository _worksRepository;


        public WorkApplicationsService(IWorkApplicationsRepository workApplicationsRepository, ICoordinatesRepository coordinatesRepository,
            IUsersRepository usersRepository, RefreshTokensRepository refreshTokensRepository, IWorkComplexityTypesRepository workComplexityTypesRepository, IWorksRepository worksRepository)
        {
            _workApplicationsRepository = workApplicationsRepository;
            _coordinatesRepository = coordinatesRepository;
            _usersRepository = usersRepository;
            _refreshTokensRepository = refreshTokensRepository;
            _workComplexityTypesRepository = workComplexityTypesRepository;
            _worksRepository = worksRepository;
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
            WorkApplication workApplication = await _workApplicationsRepository.FindById(workApplicationsId);

            User user = await _usersRepository.FindByIdWithRolesAsync(workApplication.FromUsersId);

            int ranking = user.Ranking - (RANKING_BASE_DECREASING * user.NegativeScore);

            int negativeScore = user.NegativeScore * NEGATIVE_SCORE_MULTIPLIER_INCREASING;

            bool isBanned = false;

            if (ranking <= USER_BAR_RANKING_AT_LEAST)
            {
                isBanned = true;

                await _refreshTokensRepository.UpdateInvalidatedByUserIdTokensAsync(true, user.Id);
            }

            User updatedUser = User.Create(user.Id, user.Nickname, user.Email, user.Password, ranking, user.Roles, negativeScore, isBanned);

            await _usersRepository.UpdateAsync(updatedUser);

            await _workApplicationsRepository.UpdateWorkReportStatusTypesIdByIdAsync(workApplicationsId, (int)WorkReportStatusEnum.Denied);
        }

        public async Task ConfirmAsync(Guid workApplicationsId)
        {
            WorkApplication workApplication = await _workApplicationsRepository.FindById(workApplicationsId);

            User user = await _usersRepository.FindByIdWithRolesAsync(workApplication.FromUsersId);

            int ranking = user.Ranking + RANKING_BASE_INCREASING;

            int negativeScore = user.NegativeScore - NEGATIVE_SCORE_DECREASING;

            if (negativeScore < 0)
            {
                negativeScore = 0;
            }

            User updatedUser = User.Create(user.Id, user.Nickname, user.Email, user.Password, ranking, user.Roles, negativeScore, user.IsBanned);

            await _usersRepository.UpdateAsync(updatedUser);

            WorkComplexityType workComplexityType = await _workComplexityTypesRepository.FindById((int)workApplication.WorkComplexityTypesId);

            Guid statusOpen = Guid.NewGuid();
            DateTime startDateTime = DateTime.UtcNow;

            Work work = Work.Create(Guid.NewGuid(), workApplication.Title, workApplication.Description, startDateTime,
                startDateTime.AddHours(workComplexityType.DurationHours), workApplication.WorkComplexityTypesId, WorkStatusEnum.Avaliable,
                workApplication.CoordinatesId, []);

            await _worksRepository.AddAsync(work);

            await _workApplicationsRepository.UpdateWorkReportStatusTypesIdByIdAsync(workApplicationsId, (int)WorkReportStatusEnum.Accepted);
        }
    }
}
