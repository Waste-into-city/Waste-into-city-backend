using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class TrashcanOccupancyTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public TrashcanOccupancyTypesRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
