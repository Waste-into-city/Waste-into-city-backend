using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Configurations;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Extensions;

namespace WasteIntoCity.Persistance.Repositories
{
    public class TrashTypesRepository : ITrashTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public TrashTypesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddAllIfEachNotExist(List<TrashType> trashTypes)
        {
            List<TrashTypeEntity> trashTypesEntities = trashTypes.Select(w =>
                new TrashTypeEntity
                {
                    Id = (int)w.Id,
                    Name = w.Name.Value
                }
            ).ToList();

            await _mainDbContext.AddToDbTypesBasedEntitiesIfEachNotExistById(_mainDbContext.TrashTypes, trashTypesEntities,
                TrashTypeConfiguration.TABLE_NAME);
        }
    }
}
