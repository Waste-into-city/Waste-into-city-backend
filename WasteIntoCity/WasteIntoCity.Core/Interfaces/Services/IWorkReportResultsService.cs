using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkReportResultsService
    {
        Task CreateAsync(Guid fromParticipantId, string title, string description, int workComplexityTypesId, int workStatusTypesId);

        Task<WorkReportResult> GetAsync(Guid id);
    }
}
