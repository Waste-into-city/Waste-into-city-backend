using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorksRepository
    {
        Task AddAsync(Work work);

        Task<List<Work>> FindAllAsync();

        Task<Work> FindByIdAsync(Guid id);

        Task<List<Work>> FindAllByParticipantIdAsync(Guid participantId);

        Task UpdateAsync(Work work);

        Task UpdateStatusesIdByIdAsync(Guid id, Guid statusGuid);
    }
}
