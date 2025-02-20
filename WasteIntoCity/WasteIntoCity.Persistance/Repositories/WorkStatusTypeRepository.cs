using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkStatusTypeRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkStatusTypeRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
