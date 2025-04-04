using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IRolesRepository
    {
        Task<Role> FindById(int id);

        Task AddAllIfEachNotExist(List<Role> roles);
    }
}
