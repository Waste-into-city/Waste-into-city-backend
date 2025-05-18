using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorksService
    {
        Task<List<Work>> GetAll();

        Task<List<Work>> GetAllLookup();

        Task<Work> GetByIdAsync(Guid id);

        Task<List<Work>> GetAllOwnTakePartIn(Guid userId);

        Task UpdateAsync(Guid id, string title, string description, DateTime startedDatetime, DateTime finishDatetime, int workComplexityTypesId,
            int workStatusesId, Guid coordinatesId);

        Task UpdateWorkStatusAsync(Guid id, int workStatusesId);

        Task TakePartInFirstSelfAsync(Guid id, Guid userId, DateTime startedDatetime);

        Task TakePartInSelfAsync(Guid id, Guid userId);

        Task LeaveFromParticipationSelfAsync(Guid id, Guid userId);
    }
}
