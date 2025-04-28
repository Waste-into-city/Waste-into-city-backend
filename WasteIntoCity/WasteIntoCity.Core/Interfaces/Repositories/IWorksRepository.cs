using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorksRepository
    {
        Task AddAsync(Work work);

        Task<List<Work>> FindAllAsync();

        Task<Work> FindByIdAsync(Guid id);

        Task<List<Work>> FindAllWithCoordinatesByParticipantIdAsync(Guid participantId);

        Task UpdateAsync(Work work);

        Task UpdateStatusIdByIdAsync(Guid id, WorkStatusEnum workStatusTypesId);

        Task UpdateToAvailableWorksByIds(List<Guid> ids);

        Task<List<Work>> FindFirstFinishedWithParticipantsAndMultiplierRankingAndWorkColleagueReportsByFinishedTimeAndClientStatuses(int worksAmount, TimeSpan minWorkIntervalAfterFinished);

        Task<List<Work>> FindFirstPendingFinalizationWorksWithParticipantsByFinishedTimeAndClientStatuses(int worksAmount, TimeSpan minWorkIntervalAfterFinished);

        Task<List<Guid>> FindFirstPreparingWorksIdsByBeforeStartedTime(int worksAmount, TimeSpan minWorkIntervalBeforeStart);
    }
}
