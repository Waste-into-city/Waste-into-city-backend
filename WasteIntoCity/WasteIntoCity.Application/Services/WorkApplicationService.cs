using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Application.Services
{
    public class WorkApplicationService : IWorkApplicationService
    {
        private readonly IWorkApplicationsRepository _workApplicationsRepository;
        private readonly ICoordinatesRepository _coordinatesRepository;

        public WorkApplicationService(IWorkApplicationsRepository workApplicationsRepository, ICoordinatesRepository coordinatesRepository)
        {
            _workApplicationsRepository = workApplicationsRepository;
            _coordinatesRepository = coordinatesRepository;
        }

        public async Task CreateOwnAsync(string title, string description, int workComplexityId, string lat, string lng,
            Guid userId)
        {
            Coordinates coordinates = Coordinates.Create(Guid.NewGuid(), lat, lng);

            await _coordinatesRepository.AddAsync(coordinates);

            WorkApplication workApplication = WorkApplication.Create(Guid.NewGuid(), Title.Create(title), Description.Create(description), workComplexityId,
                coordinates.Id, DateTime.UtcNow, userId, (int)WorkReportStatusEnum.Pending);

            await _workApplicationsRepository.AddAsync(workApplication);
        }
    }
}
