using AutoMapper;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkReportsResult
    {
        private readonly MainDbContext _mainDbContext;

        private readonly IMapper _mapper;

        public WorkReportsResult(MainDbContext mainDbContext, IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }
    }
}
