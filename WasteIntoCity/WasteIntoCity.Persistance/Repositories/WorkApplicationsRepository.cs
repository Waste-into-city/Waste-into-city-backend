using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkApplicationsRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkApplicationsRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
