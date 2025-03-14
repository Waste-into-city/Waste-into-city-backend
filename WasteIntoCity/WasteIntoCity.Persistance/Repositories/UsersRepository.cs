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
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new DbIsNotFoundException(nameof(User), null);

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email), Password.Create(userEntity.Password),
                userEntity.Ranking);
        }

        public async Task<User> FindByIdAsync(string id)
        {
            UserEntity userEntity = await _mainDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id.ToString() == id)
                ?? throw new DbIsNotFoundException(nameof(User), null);

            return User.Create(userEntity.Id, Nickname.Create(userEntity.Nickname), Email.Create(userEntity.Email), Password.Create(userEntity.Password),
                userEntity.Ranking);
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
