using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class TrashcanTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public TrashcanTypesRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
