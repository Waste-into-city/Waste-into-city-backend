using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class UsersRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public UsersRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
