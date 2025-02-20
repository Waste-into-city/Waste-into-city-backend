using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkReportComplaintRepository
    {
        Task Create(WorkReportComplaint workReportComplaint);

        Task Delete(Guid id);

        Task<List<WorkReportComplaint>> Get();

        Task<WorkReportComplaint> GetById(Guid id);

        Task Update(WorkReportComplaint workReportComplaint);
    }
}
