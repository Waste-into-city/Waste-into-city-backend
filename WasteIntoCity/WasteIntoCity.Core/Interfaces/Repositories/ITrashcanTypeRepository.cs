using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ITrashcanTypeRepository
    {
        Task Create(TrashcanType trashcanType);

        Task Delete(Guid id);

        Task<List<TrashcanType>> Get();

        Task<TrashcanType> GetById(Guid id);

        Task Update(TrashcanType trashcanType);
    }
}
