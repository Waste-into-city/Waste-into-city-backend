using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkMarkTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkMarkTypesRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
