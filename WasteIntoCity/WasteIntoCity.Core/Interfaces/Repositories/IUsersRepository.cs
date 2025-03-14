using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(User user);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByIdAsync(string id);
    }
}
