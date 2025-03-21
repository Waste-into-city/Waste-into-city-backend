using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorksService
    {
        Task CreateAsync(string title, string description, DateTime startedDatetime, DateTime finishDatetime, int workComplexityTypesId,
            Guid workStatusesId, Guid coordinatesId);

        Task<List<Work>> GetAll();

        Task<Work> GetById(Guid id);

        Task<List<Work>> GetAllOwnTakePartIn(Guid userId);

        Task UpdateAsync(Guid id, string title, string description, DateTime startedDatetime, DateTime finishDatetime, int workComplexityTypesId,
            Guid workStatusesId, Guid coordinatesId);

        Task UpdateWorkStatusAsync(Guid id, Guid workStatusesId);
    }
}
