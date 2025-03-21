using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ITrashcanPointsRepository
    {
        Task Create(Coordinates trashcan);

        Task Delete(Guid id);

        Task<List<Coordinates>> Get();

        Task<Coordinates> GetById(Guid id);

        Task Update(Coordinates trashcan);
    }
}
