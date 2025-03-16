using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkComplexityTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkComplexityTypesRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
