using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ITrashTypesRepository
    {
        Task AddAllIfEachNotExist(List<TrashType> trashTypes);
    }
}
