using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkMarkTypeRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkMarkTypeRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
