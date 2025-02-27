using AutoMapper;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Persistance.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public UsersRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }

        public async Task Add(User user)
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

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
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
