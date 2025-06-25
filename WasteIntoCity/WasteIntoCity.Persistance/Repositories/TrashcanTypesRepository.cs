using WasteIntoCity.Core.Interfaces.Repositories;

namespace WasteIntoCity.Persistance.Repositories
{
    public class TrashcanTypesRepository : ITrashcanTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public TrashcanTypesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }
    }
}
