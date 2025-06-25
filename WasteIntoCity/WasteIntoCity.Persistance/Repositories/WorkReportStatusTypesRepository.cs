using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Configurations;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Extensions;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkReportStatusTypesRepository : IWorkReportStatusTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkReportStatusTypesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddAllIfEachNotExist(List<WorkReportStatusType> workReportStatusTypes)
        {
            List<WorkReportStatusTypeEntity> workReportStatusTypeEntities = workReportStatusTypes.Select(w =>
                new WorkReportStatusTypeEntity
                {
                    Id = (int)w.Id,
                    Name = w.Name.Value,

                }
            ).ToList();

            await _mainDbContext.AddToDbTypesBasedEntitiesIfEachNotExistById(_mainDbContext.WorkReportStatusTypes, workReportStatusTypeEntities,
                WorkReportStatusTypeConfiguration.TABLE_NAME);
        }
    }
}
