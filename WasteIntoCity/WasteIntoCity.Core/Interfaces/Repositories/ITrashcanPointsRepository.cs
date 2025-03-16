using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ITrashcanPointsRepository
    {
        Task Create(TrashcanPoint trashcan);

        Task Delete(Guid id);

        Task<List<TrashcanPoint>> Get();

        Task<TrashcanPoint> GetById(Guid id);

        Task Update(TrashcanPoint trashcan);
    }
}
