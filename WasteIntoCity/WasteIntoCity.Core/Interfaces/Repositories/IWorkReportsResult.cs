using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkReportsResult
    {
        Task Create(WorkReportResult workReportResult);

        Task Delete(Guid id);

        Task<List<WorkReportResult>> Get();

        Task<WorkReportResult> GetById(Guid id);

        Task Update(WorkReportResult workReportResult);
    }
}
