using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddAsync(User user);

        Task<bool> IsExistByEmailAsync(string email);

        Task<User> FindByIdWithRolesAsync(Guid id);

        Task<User> FindByEmailWithRolesAsync(string email);

        Task UpdateAsync(User user);
    }
}
