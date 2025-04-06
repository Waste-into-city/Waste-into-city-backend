using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkColleagueReportsRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkColleagueReportsRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
