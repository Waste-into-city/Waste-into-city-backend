using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task Add(User user);

        Task<User> GetByEmail(string email);
    }
}
