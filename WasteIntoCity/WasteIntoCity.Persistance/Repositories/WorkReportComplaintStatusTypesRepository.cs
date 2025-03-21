using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkReportComplaintStatusTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkReportComplaintStatusTypesRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
