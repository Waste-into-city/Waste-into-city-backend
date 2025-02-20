using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class TrashcanPointReportEachMarkRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public TrashcanPointReportEachMarkRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
