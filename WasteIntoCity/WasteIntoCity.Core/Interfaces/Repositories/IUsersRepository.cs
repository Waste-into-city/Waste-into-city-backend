using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(User user);

        Task<bool> IsExistByEmailAsync(string email);

        Task<User> FindByIdWithRolesAsync(string id);

        Task<User> FindByEmailWithRolesAsync(string email);
    }
}
