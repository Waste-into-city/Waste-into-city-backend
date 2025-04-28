using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkMarkTypesRepository
    {
        Task<Dictionary<WorkMarkEnum, int>> TakeDictionaryAllWithKeyIdAndValueAdditionRanking();

        Task AddAllIfEachNotExist(List<WorkMarkType> workMarkTypes);
    }
}
