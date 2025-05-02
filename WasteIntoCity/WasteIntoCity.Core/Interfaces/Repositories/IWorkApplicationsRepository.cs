using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkApplicationsRepository
    {
        Task CreateAsync(WorkApplication workApplication);

        Task<WorkApplication> FindById(Guid id);

        Task<WorkApplication> FindPendingWithFromUserAndTrashTypesIdsAndImageNamesAndCoordinatesByStartedDatetimeAscending();

        Task UpdateWorkReportStatusTypesIdByIdAsync(Guid id, WorkReportStatusEnum workReportStatusTypesId);
    }
}
