using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class NotificationRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public NotificationRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
