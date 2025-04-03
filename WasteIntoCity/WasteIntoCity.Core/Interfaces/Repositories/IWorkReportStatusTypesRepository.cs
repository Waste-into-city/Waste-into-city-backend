using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IWorkReportStatusTypesRepository
    {
        Task AddAllIfEachNotExist(List<WorkReportStatusType> workReportStatusTypes);
    }
}
