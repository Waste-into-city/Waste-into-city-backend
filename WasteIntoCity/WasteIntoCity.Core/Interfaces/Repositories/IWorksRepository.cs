using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorksRepository
    {
        Task Create(Work work);

        Task Delete(Guid id);

        Task<List<Work>> Get();

        Task<Work> GetById(Guid id);

        Task Update(Work work);
    }
}
