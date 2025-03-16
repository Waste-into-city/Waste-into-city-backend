using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkStatusTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkStatusTypesRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
