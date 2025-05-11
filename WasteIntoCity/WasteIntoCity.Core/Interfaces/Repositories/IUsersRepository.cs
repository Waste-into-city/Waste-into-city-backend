using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task AddWithRolesAsync(User user);

        Task<int> CountByUserRoleAsync();

        Task<List<User>> FindAllByRoleUserAndRankingDescendingAndPageAsync(int page, int pageSize);

        Task<bool> IsExistByEmailAsync(string email);

        Task<User> FindByIdAsync(Guid id);

        Task<User> FindWithRolesByIdAsync(Guid id);

        Task<User> FindByEmailWithRolesAsync(string email);

        Task<User> FindWithImageById(Guid id);

        Task<User> FindWithImageAndRolesById(Guid id);

        Task UpdateByIdAsync(User user);

        Task AddAllIfNotExistWithRolesByEmailAsync(List<User> users);

        Task UpdateAllByIdAsync(List<User> users);
    }
}
