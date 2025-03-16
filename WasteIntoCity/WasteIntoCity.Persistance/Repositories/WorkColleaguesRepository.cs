using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkColleaguesRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkColleaguesRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
