using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddWithRolesAsync(User user);

        Task<bool> IsExistByEmailAsync(string email);

        Task<User> FindByIdWithRolesAsync(Guid id);

        Task<User> FindByEmailWithRolesAsync(string email);

        Task<User> FindWithImagesById(Guid id);

        Task UpdateAsync(User user);

        Task AddAllIfNotExistWithRolesByEmailAsync(List<User> users);

        Task UpdateAllByIdAsync(List<User> users);
    }
}
