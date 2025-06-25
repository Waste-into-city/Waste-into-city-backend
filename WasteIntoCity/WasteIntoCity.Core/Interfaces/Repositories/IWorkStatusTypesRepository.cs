using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkStatusTypesRepository
    {
        Task AddAllIfEachNotExist(List<WorkStatusType> workStatusTypes);
    }
}
