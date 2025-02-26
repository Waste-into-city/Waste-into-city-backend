using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Create(User user);

        Task Delete(Guid id);

        Task<List<User>> Get();

        Task<User> GetById(Guid id);

        Task Update(User User);
    }
}
