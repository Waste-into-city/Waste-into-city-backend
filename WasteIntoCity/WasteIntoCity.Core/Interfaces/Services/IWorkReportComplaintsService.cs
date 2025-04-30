using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkReportComplaintsService
    {
        Task CreateAsync(string title, string description, Guid worksId, Guid fromUsersId);

        Task<WorkReportComplaint> GetAsync(Guid id);

        Task ConfirmAsync(Guid id);

        Task RejectAsync(Guid id);
    }
}
