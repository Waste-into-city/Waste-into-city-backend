using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ITrashcanPointReportsRepository
    {
        Task Create(TrashcanPointReport trashcanPointReport);

        Task Delete(Guid id);

        Task<List<TrashcanPointReport>> Get();

        Task<TrashcanPointReport> GetById(Guid id);

        Task Update(TrashcanPointReport trashcanPointReport);
    }
}
