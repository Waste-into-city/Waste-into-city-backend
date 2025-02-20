using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ITrashcanPointReportEachMarkRepository
    {
        Task Create(TrashcanPointReportEachMark trashcan);

        Task Delete(Guid id);

        Task<List<TrashcanPointReportEachMark>> Get();

        Task<TrashcanPointReportEachMark> GetById(Guid id);

        Task Update(TrashcanPointReportEachMark trashcan);
    }
}
