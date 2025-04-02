using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkApplicationsRepository
    {
        Task AddAsync(WorkApplication workApplication);

        Task<WorkApplication> FindById(Guid id);

        Task UpdateWorkReportStatusTypesIdByIdAsync(Guid id, WorkReportStatusEnum workReportStatusTypesId);
    }
}
