using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class ImageRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public ImageRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
