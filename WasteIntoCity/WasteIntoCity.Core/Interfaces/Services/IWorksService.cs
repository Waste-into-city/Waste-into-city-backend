using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorksService
    {
        Task<List<Work>> GetAll();

        Task<Work> GetByIdAsync(Guid id);

        Task<List<Work>> GetAllOwnTakePartIn(Guid userId);

        Task UpdateAsync(Guid id, string title, string description, DateTime startedDatetime, DateTime finishDatetime, int workComplexityTypesId,
            int workStatusesId, Guid coordinatesId);

        Task UpdateWorkStatusAsync(Guid id, int workStatusesId);
    }
}
