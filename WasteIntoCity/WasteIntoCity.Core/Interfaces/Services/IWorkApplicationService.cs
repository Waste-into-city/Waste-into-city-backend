using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkApplicationService
    {
        Task CreateAsync(string title, string description, DateTime startedDatetime, DateTime finishDatetime, Guid workComplexityId, Guid workStatusesId);

        Task<List<Work>> GetAll();

        Task<Work> GetById(Guid id);

        Task<List<Work>> GetAllOwnTakePartIn(Guid userId);

        Task UpdateAsync(Guid id, string title, string description, DateTime startedDatetime, DateTime finishDatetime, Guid workComplexityId,
            Guid workStatusesId);

        Task UpdateWorkStatusAsync(Guid id, Guid workStatusesId);
    }
}
