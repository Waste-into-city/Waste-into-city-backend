using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class TrashcanPointReportRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public TrashcanPointReportRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
