using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkColleagueRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkColleagueRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
