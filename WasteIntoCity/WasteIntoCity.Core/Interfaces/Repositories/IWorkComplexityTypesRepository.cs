using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkComplexityTypesRepository
    {
        Task Create(WorkComplexityType workComplexityType);

        Task Delete(Guid id);

        Task<List<WorkComplexityType>> Get();

        Task<WorkComplexityType> GetById(Guid id);

        Task Update(WorkComplexityType workComplexityType);
    }
}
