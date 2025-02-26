using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class TrashcanOccupancyTypeRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public TrashcanOccupancyTypeRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
