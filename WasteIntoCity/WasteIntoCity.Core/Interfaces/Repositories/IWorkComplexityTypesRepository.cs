using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkComplexityTypesRepository
    {
        Task<WorkComplexityType> FindById(int id);

        Task AddAllIfEachNotExist(List<WorkComplexityType> roles);

        Task<List<WorkComplexityType>> FindAll();
    }
}
