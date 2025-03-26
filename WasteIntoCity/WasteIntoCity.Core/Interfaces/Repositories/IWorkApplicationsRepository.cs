using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkApplicationsRepository
    {
        Task AddAsync(WorkApplication workApplication);

        Task<WorkApplication> FindById(Guid id);

        Task UpdateWorkReportStatusTypesIdByIdAsync(Guid id, int workReportStatusTypesId);
    }
}
