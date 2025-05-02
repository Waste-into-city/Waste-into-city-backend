using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorksRepository
    {
        Task CreateAsync(Work work);

        Task<int> CountAsync();

        Task<List<Work>> FindAllAsync();

        Task<List<Work>> FindAllWithCoordinatesByPageAsync(int page, int pageSize);

        Task<Work> FindWithCoordinatesAndParticipantsAndImagesAndTrashTypesByIdAsync(Guid id);

        Task<Work> FindWithParticipantsByIdAsync(Guid id);

        Task<List<Work>> FindAllWithCoordinatesByParticipantIdAsync(Guid participantId);

        Task UpdateAsync(Work work);

        Task UpdateStatusIdByIdAsync(Guid id, WorkStatusEnum workStatusTypesId);

        Task UpdateToAvailableWorksByIds(List<Guid> ids);

        Task<List<Work>> FindFirstFinishedWithParticipantsAndMultiplierRankingAndWorkColleagueReportsByFinishedTimeAndClientStatuses(int worksAmount, TimeSpan minWorkIntervalAfterFinished);

        Task<List<Work>> FindFirstPendingFinalizationWorksWithParticipantsByFinishedTimeAndClientStatuses(int worksAmount, TimeSpan minWorkIntervalAfterFinished);

        Task<List<Guid>> FindFirstPreparingWorksIdsByBeforeStartedTime(int worksAmount, TimeSpan minWorkIntervalBeforeStart);
    }
}
