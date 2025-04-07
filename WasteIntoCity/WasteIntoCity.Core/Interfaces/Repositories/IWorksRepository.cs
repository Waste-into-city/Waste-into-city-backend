using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorksRepository
    {
        Task AddAsync(Work work);

        Task<List<Work>> FindAllAsync();

        Task<Work> FindByIdAsync(Guid id);

        Task<List<Work>> FindAllWithCoordinatesByParticipantIdAsync(Guid participantId);

        Task UpdateAsync(Work work);

        Task UpdateStatusesIdByIdAsync(Guid id, WorkStatusEnum workStatusTypesId);

        Task<List<Work>> FindFirstByFinishedTimeWithParticipantsAndMultiplierRankingAndWorkColleagueReportsAndWorkStatus(int worksAmount, TimeSpan minWorkIntervalAfterFinished);

        Task<List<Work>> FindWithParticipants(int worksAmount, TimeSpan minWorkIntervalAfterFinished);
    }
}
