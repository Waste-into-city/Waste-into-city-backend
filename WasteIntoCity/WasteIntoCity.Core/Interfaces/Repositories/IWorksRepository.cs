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

        Task<List<Work>> FindFirstByFinishedTimeAndStatusesWithParticipantsAndMultiplierRankingAndWorkColleagueReportsAndWorkStatus(int worksAmount, TimeSpan minWorkIntervalAfterFinished, WorkStatusEnum[] workStatuses);

        Task<List<Work>> FindFirstByFinishedTimeAndStatusesWithParticipants(int worksAmount, TimeSpan minWorkIntervalAfterFinished, WorkStatusEnum[] workStatuses);
    }
}
