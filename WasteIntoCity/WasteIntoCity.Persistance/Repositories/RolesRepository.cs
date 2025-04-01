using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public RolesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Role> FindById(int id)
        {
            RoleEntity roleEntity = await _mainDbContext.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id)
                 ?? throw new DbIsNotFoundException(nameof(Role), null);

            return Role.Create((RoleEnum)roleEntity.Id, roleEntity.Name);
        }
    }
}
