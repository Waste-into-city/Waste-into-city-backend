using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkReportResult
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkReportResult(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
