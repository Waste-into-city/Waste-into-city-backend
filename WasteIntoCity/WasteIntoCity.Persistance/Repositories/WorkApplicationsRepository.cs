using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;
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
                WorkComplexityTypesId = (int)workApplication.WorkComplexityTypesId,
                CoordinatesId = workApplication.CoordinatesId,
                FromUsersId = workApplication.FromUsersId,
                WorkReportStatusTypesId = (int)workApplication.WorkReportStatusTypesId
            };

            await _mainDbContext.WorkApplications.AddAsync(workApplicationEntity);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<WorkApplication> FindById(Guid id)
        {
            WorkApplicationEntity workApplicationEntity = await _mainDbContext.WorkApplications.AsNoTracking().
                FirstOrDefaultAsync(u => u.Id == id) ?? throw new DbIsNotFoundException(nameof(WorkApplication), 6, null);

            return WorkApplication.Create(workApplicationEntity.Id, Title.Create(workApplicationEntity.Title),
                Description.Create(workApplicationEntity.Description), (WorkComplexityEnum)workApplicationEntity.WorkComplexityTypesId,
                workApplicationEntity.CoordinatesId, workApplicationEntity.StartedDatetime, workApplicationEntity.FromUsersId,
                (WorkReportStatusEnum)workApplicationEntity.WorkReportStatusTypesId);
        }

        public async Task UpdateAsync(WorkApplication workApplication)
        {
            await _mainDbContext.WorkApplications
                .Where(r => r.Id == workApplication.Id)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(r => r.Title, workApplication.Title.Value)
                    .SetProperty(r => r.Description, workApplication.Description.Value)
                    .SetProperty(r => r.StartedDatetime, workApplication.StartedDatetime)
                    .SetProperty(r => r.WorkComplexityTypesId, (int)workApplication.WorkComplexityTypesId)
                    .SetProperty(r => r.CoordinatesId, workApplication.CoordinatesId)
                    .SetProperty(r => r.FromUsersId, workApplication.FromUsersId)
                    .SetProperty(r => r.WorkReportStatusTypesId, (int)workApplication.WorkReportStatusTypesId)
                );
        }

        public async Task UpdateWorkReportStatusTypesIdByIdAsync(Guid id, WorkReportStatusEnum workReportStatusTypesId)
        {
            await _mainDbContext.WorkApplications
                .Where(r => r.Id == id)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(r => r.WorkReportStatusTypesId, (int)workReportStatusTypesId)
                );
        }
    }
}
