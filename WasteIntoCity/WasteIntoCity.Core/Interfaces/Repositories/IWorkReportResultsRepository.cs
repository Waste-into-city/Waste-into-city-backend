using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkReportResultsRepository
    {
        Task CreateAsync(WorkReportResult workReportResult);

        Task<WorkReportResult> FindByIdAsync(Guid id);

        Task<WorkReportResult> FindByWorksIdAsync(Guid worksId);

        Task<WorkReportResult> FindWithParticipantByIdAsync(Guid id);
    }
}
