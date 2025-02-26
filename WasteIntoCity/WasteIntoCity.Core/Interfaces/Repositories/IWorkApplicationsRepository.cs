using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkApplicationsRepository
    {
        Task Create(WorkApplication workApplication);

        Task Delete(Guid id);

        Task<List<WorkApplication>> Get();

        Task<WorkApplication> GetById(Guid id);

        Task Update(WorkApplication workApplication);
    }
}
