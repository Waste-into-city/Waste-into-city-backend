using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkReportComplaintRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkReportComplaintRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
