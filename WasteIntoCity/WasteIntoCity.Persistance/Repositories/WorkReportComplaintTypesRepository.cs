using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkReportComplaintTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkReportComplaintTypesRepository(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
