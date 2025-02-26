using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class TrashcanPointReportsRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public TrashcanPointReportsRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
