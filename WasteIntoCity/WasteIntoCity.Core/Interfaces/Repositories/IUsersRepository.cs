using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddWithRolesAsync(User user);

        Task<bool> IsExistByEmailAsync(string email);

        Task<User> FindByIdAsync(Guid id);

        Task<User> FindWithRolesByIdAsync(Guid id);

        Task<User> FindByEmailWithRolesAsync(string email);

        Task<User> FindWithImagesById(Guid id);

        Task UpdateByIdAsync(User user);

        Task AddAllIfNotExistWithRolesByEmailAsync(List<User> users);

        Task UpdateAllByIdAsync(List<User> users);
    }
}
