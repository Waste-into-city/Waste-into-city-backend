namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkApplicationService
    {
        Task CreateOwnAsync(string title, string description, int workComplexityId, string lat, string lng, Guid userId);
    }
}
