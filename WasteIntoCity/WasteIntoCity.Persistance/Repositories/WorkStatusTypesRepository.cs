using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Configurations;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Extensions;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkStatusTypesRepository : IWorkStatusTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkStatusTypesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddAllIfEachNotExist(List<WorkStatusType> workStatusTypes)
        {
            List<WorkStatusTypeEntity> workStatusTypeEntities = workStatusTypes.Select(w =>
                new WorkStatusTypeEntity
                {
                    Id = (int)w.Id,
                    Name = w.Name.Value,
                    AddingRanking = w.AddingRanking,
                }
            ).ToList();

            await _mainDbContext.AddToDbTypesBasedEntitiesIfEachNotExistById(_mainDbContext.WorkStatusTypes, workStatusTypeEntities,
                WorkStatusTypeConfiguration.TABLE_NAME);
        }
    }
}
