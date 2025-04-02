using WasteIntoCity.Core.Types;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkMarkTypesRepository
    {
        Task<Dictionary<WorkMarkEnum, int>> TakeDictionaryAllWithKeyIdAndValueAdditionRanking();
    }
}
