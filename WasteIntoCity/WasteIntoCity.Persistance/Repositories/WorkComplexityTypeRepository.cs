using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkComplexityTypeRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkComplexityTypeRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
