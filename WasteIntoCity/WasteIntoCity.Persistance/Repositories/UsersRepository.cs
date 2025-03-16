using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MainDbContext _mainDbContext;

        //private readonly IMapper _mapper;

        public UsersRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
            //_mapper = mapper;
        }

        public async Task AddAsync(User user)
        {
            List<int> roleIds = user.Roles.Select(r => r.Id).ToList();

            List<UserAccordingRoleEntity> userRoles = roleIds.Select(roleId => new UserAccordingRoleEntity
            {
                UsersId = user.Id,
                RolesId = roleId

            }).ToList();

            UserEntity userEntity = new UserEntity()
            {
                Id = user.Id,
                Nickname = user.Nickname.Value,
                Email = user.Email.Value,
                Password = user.Password.Value,
                Ranking = user.Ranking,
            };

            await _mainDbContext.Users.AddAsync(userEntity);
            await _mainDbContext.SaveChangesAsync();

            await _mainDbContext.UserAccordingRoles.AddRangeAsync(userRoles);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExistByEmailAsync(string email)
        {
            if (await _mainDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email) == null)
            {
                return false;
            }

            return true;
        }

        public async Task<User> FindByEmailWithRolesAsync(string email)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new DbIsNotFoundException(nameof(User), null);

            List<Role> roles = userEntity.Roles.Select(r => Role.Create(r.Id, r.Name)).ToList();

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email), Password.Create(userEntity.Password),
                userEntity.Ranking, roles);
        }

        public async Task<User> FindByIdWithRolesAsync(string id)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id.ToString() == id)
                ?? throw new DbIsNotFoundException(nameof(User), null);

            List<Role> roles = userEntity.Roles.Select(r => Role.Create(r.Id, r.Name)).ToList();

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email), Password.Create(userEntity.Password),
                userEntity.Ranking, roles);
        }

        //public async Task<User> GetByEmail(string email)
        //{
        //    throw new Exception();
        //    //UserEntity userEntity = await _mainDbContext.Users
        //    //    .AsNoTracking()
        //    //    .FirstOrDefaultAsync(u => u.Email == email) ?? throw new ;

        //    //return _mapper.Map<User>(userEntity);
        //}
    }
}
