using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkMarkTypesRepository
    {
        Task Create(WorkMarkType workMarkType);

        Task Delete(Guid id);

        Task<List<WorkMarkType>> Get();

        Task<WorkMarkType> GetById(Guid id);

        Task Update(WorkMarkType workMarkType);
    }
}
