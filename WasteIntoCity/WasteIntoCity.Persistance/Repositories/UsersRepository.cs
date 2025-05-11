using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MainDbContext _mainDbContext;

        public UsersRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddWithRolesAsync(User user)
        {
            if (user.Roles == null)
            {
                throw new NullValueServerException(24, "roles", null);
            }

            List<int> roleIds = user.Roles.Select(r => (int)r.Id).ToList();

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
                NegativeScore = user.NegativeScore,
                IsBanned = user.IsBanned
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

        public async Task<int> CountByUserRoleAsync()
        {
            return await _mainDbContext.Users.AsNoTracking().Include(u => u.Roles).Where(u => u.Roles.Any(r => r.Id == (int)RoleEnum.User))
                .CountAsync();
        }

        public async Task<List<User>> FindAllByRoleUserAndRankingDescendingBySkipItemsAndSizeAsync(int skipItems, int size)
        {
            List<UserEntity> userEntities = await _mainDbContext.Users.AsNoTracking().OrderByDescending(u => u.Ranking).
                Include(u => u.Roles).Where(u => u.Roles.Any(r => r.Id == (int)RoleEnum.User)).Skip(skipItems).
                Take(size).ToListAsync();

            List<User> users = userEntities.Select(u =>
            {
                return User.Create(u.Id, Nickname.Create(u.Nickname), Email.Create(u.Email), Password.Create(u.Password), u.Ranking, null,
                    u.NegativeScore, u.IsBanned, null, null);
            }).ToList();

            return users;
        }

        public async Task<User> FindByEmailWithRolesAsync(string email)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new DbIsNotFoundException(nameof(User), 4, null);

            List<Role> roles = userEntity.Roles.Select(r => Role.Create((RoleEnum)r.Id, r.Name)).ToList();

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email), Password.Create(userEntity.Password),
                userEntity.Ranking, roles, userEntity.NegativeScore, userEntity.IsBanned, null, null);
        }

        public async Task<User> FindWithRolesByIdAsync(Guid id)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new DbIsNotFoundException(nameof(User), 5, null);

            List<Role> roles = userEntity.Roles.Select(r => Role.Create((RoleEnum)r.Id, r.Name)).ToList();

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email), Password.Create(userEntity.Password),
                userEntity.Ranking, roles, userEntity.NegativeScore, userEntity.IsBanned, null, null);
        }

        public async Task<User> FindByIdAsync(Guid id)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new DbIsNotFoundException(nameof(User), 5, null);

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email), Password.Create(userEntity.Password),
                userEntity.Ranking, null, userEntity.NegativeScore, userEntity.IsBanned, null, null);
        }

        public async Task<User> FindWithImageById(Guid id)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new DbIsNotFoundException(nameof(User), 11, null);

            ImageEntity? imageEntity = await _mainDbContext.Images.AsNoTracking().FirstOrDefaultAsync(i => i.UsersId == id);
            //?? throw new DbIsNotFoundException(nameof(Image), 12, null);

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email),
                Password.Create(userEntity.Password), userEntity.Ranking, null, userEntity.NegativeScore, userEntity.IsBanned,
                imageEntity == null ? null : ImageName.Create(imageEntity.Name), null);
        }


        public async Task<User> FindWithImageAndRolesById(Guid id)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().Include(u => u.Roles).
                FirstOrDefaultAsync(u => u.Id == id) ?? throw new DbIsNotFoundException(nameof(User), 11, null);

            ImageEntity? imageEntity = await _mainDbContext.Images.AsNoTracking().FirstOrDefaultAsync(i => i.UsersId == id);
            //?? throw new DbIsNotFoundException(nameof(Image), 12, null);

            List<Role> roles = userEntity.Roles.Select(r => Role.Create((RoleEnum)r.Id, r.Name)).ToList();

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email),
                Password.Create(userEntity.Password), userEntity.Ranking, roles, userEntity.NegativeScore,
                userEntity.IsBanned, imageEntity == null ? null : ImageName.Create(imageEntity.Name), null);
        }

        public async Task UpdateByIdAsync(User user)
        {
            await _mainDbContext.Users
                .Where(r => r.Id == user.Id)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(r => r.Nickname, user.Nickname.Value)
                    .SetProperty(r => r.Email, user.Email.Value)
                    .SetProperty(r => r.Password, user.Password.Value)
                    .SetProperty(r => r.Ranking, user.Ranking)
                    .SetProperty(r => r.NegativeScore, user.NegativeScore)
                );
        }

        public async Task UpdateAllByIdAsync(List<User> users)
        {
            foreach (User user in users)
            {
                UserEntity? existingUser = await _mainDbContext.Users.FindAsync(user.Id);

                if (existingUser != null)
                {
                    existingUser.Nickname = user.Nickname.Value;
                    existingUser.Email = user.Nickname.Value;
                    existingUser.Password = user.Password.Value;
                    existingUser.Ranking = user.Ranking;
                    existingUser.NegativeScore = user.NegativeScore;
                    existingUser.IsBanned = user.IsBanned;
                }
            }

            await _mainDbContext.SaveChangesAsync();
        }

        public async Task AddAllIfNotExistWithRolesByEmailAsync(List<User> users)
        {
            foreach (var user in users)
            {
                if (user.Roles == null)
                {
                    throw new NullValueServerException(29, "roles", null);
                }

                bool userExists = await _mainDbContext.Users.AnyAsync(u => u.Email == user.Email.Value);

                if (!userExists)
                {
                    UserEntity newUser = new UserEntity
                    {
                        Id = user.Id,
                        Nickname = user.Nickname.Value,
                        Email = user.Email.Value,
                        Password = user.Password.Value,
                        Ranking = user.Ranking,
                        NegativeScore = user.NegativeScore,
                        IsBanned = user.IsBanned
                    };

                    await _mainDbContext.Users.AddAsync(newUser);

                    await _mainDbContext.SaveChangesAsync();

                    await _mainDbContext.UserAccordingRoles.AddRangeAsync(user.Roles.Select(role => new UserAccordingRoleEntity
                    {
                        UsersId = newUser.Id,
                        RolesId = (int)role.Id
                    }));

                    await _mainDbContext.SaveChangesAsync();
                }
            }

            await _mainDbContext.SaveChangesAsync();
        }
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

