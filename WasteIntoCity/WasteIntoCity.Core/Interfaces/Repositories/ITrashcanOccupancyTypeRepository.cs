using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface ITrashcanOccupancyTypeRepository
    {
        Task Create(TrashcanOccupancyType trashcanOccupancyType);

        Task Delete(Guid id);

        Task<List<TrashcanOccupancyType>> Get();

        Task<TrashcanOccupancyType> GetById(Guid id);

        Task Update(TrashcanOccupancyType trashcanOccupancyType);
    }
}
