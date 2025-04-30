using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkReportComplaintsRepository
    {
        Task CreateAsync(WorkReportComplaint workReportComplaint);

        Task<WorkReportComplaint> FindByIdAsync(Guid id);

        Task UpdateWorkReportStatusTypesIdByIdAsync(Guid id, WorkReportStatusEnum workReportStatusTypesId);
    }
}
