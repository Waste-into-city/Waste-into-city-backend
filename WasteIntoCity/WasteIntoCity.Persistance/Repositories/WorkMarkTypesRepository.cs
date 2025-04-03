using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Persistance.Configurations;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Extensions;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkMarkTypesRepository : IWorkMarkTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkMarkTypesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddAllIfEachNotExist(List<WorkMarkType> workMarkTypes)
        {
            List<WorkMarkTypeEntity> workMarkTypeEntities = workMarkTypes.Select(w =>
                new WorkMarkTypeEntity
                {
                    Id = (int)w.Id,
                    Name = w.Name.Value,
                    AdditionRanking = w.AdditionRanking,
                }
            ).ToList();

            await _mainDbContext.AddToDbTypesBasedEntitiesIfEachNotExistById(_mainDbContext.WorkMarkTypes, workMarkTypeEntities,
                WorkMarkTypeConfiguration.TABLE_NAME);
        }

        public Task<Dictionary<WorkMarkEnum, int>> TakeDictionaryAllWithKeyIdAndValueAdditionRanking()
        {
            throw new NotImplementedException();
        }
    }
}
