using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkApplicationRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkApplicationRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
