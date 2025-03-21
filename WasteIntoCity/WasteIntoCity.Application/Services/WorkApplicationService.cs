using WasteIntoCity.Core.Interfaces.Services;

namespace WasteIntoCity.Application.Services
{
    public class WorkApplicationService : IWorkApplicationService
    {
        public Task CreateAsync(Guid id, string title, string description, DateTime startedDatetime, int workComplexityId, string lat, string lng)
        {
            throw new NotImplementedException();
        }
    }
}
