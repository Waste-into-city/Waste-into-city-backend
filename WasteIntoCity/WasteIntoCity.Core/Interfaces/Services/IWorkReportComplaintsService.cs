using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkReportComplaintsService
    {
        Task CreateAsync(string title, string description, Guid worksId, Guid fromUsersId, List<string> imageNamesLines);

        Task<WorkReportComplaint> GetAsync(Guid id);

        Task<(WorkReportComplaint, Guid)> GetFromQueueAsync();

        Task ConfirmAsync(Guid id);

        Task RejectAsync(Guid id);
    }
}
