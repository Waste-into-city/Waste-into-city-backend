using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkComplexityTypesRepository : IWorkComplexityTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkComplexityTypesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public Task<WorkComplexityType> FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
