using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkApplicationsRepository : IWorkApplicationsRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkApplicationsRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddAsync(WorkApplication workApplication)
        {
            WorkApplicationEntity workApplicationEntity = new WorkApplicationEntity
            {
                Id = workApplication.Id,
                Title = workApplication.Title.Value,
                Description = workApplication.Description.Value,
                StartedDatetime = workApplication.StartedDatetime,
                WorkComplexityTypesId = workApplication.WorkComplexityTypesId,
                CoordinatesId = workApplication.CoordinatesId,
                FromUsersId = workApplication.FromUsersId
            };

            await _mainDbContext.WorkApplications.AddAsync(workApplicationEntity);
            await _mainDbContext.SaveChangesAsync();
        }
    }
}
