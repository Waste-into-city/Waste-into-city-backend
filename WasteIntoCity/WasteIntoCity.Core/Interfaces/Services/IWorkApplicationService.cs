namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkApplicationService
    {
        Task CreateAsync(Guid id, string title, string description, DateTime startedDatetime, int workComplexityId, string lat, string lng);
    }
}
