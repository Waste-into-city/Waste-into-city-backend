using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface INotificationsRepository
    {
        Task Create(Notification notification);

        Task Delete(Guid id);

        Task<List<Notification>> Get();

        Task<Notification> GetById(Guid id);

        Task Update(Notification notification);
    }
}
