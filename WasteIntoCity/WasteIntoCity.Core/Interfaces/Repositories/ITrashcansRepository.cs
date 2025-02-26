using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ITrashcansRepository
    {
        Task Create(Trashcan trashcan);

        Task Delete(Guid id);

        Task<List<Trashcan>> Get();

        Task<Trashcan> GetById(Guid id);

        Task Update(Trashcan trashcan);
    }
}
