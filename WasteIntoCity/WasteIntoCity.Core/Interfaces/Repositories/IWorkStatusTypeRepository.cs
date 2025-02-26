using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkStatusTypeRepository
    {
        Task Create(WorkStatusType workStatusType);

        Task Delete(Guid id);

        Task<List<WorkStatusType>> Get();

        Task<WorkStatusType> GetById(Guid id);

        Task Update(WorkStatusType workStatusType);
    }
}
