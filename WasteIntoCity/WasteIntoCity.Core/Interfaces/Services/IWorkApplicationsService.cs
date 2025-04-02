namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkApplicationsService
    {
        Task CreateOwnAsync(string title, string description, int workComplexityId, string lat, string lng, Guid userId);

        Task RejectAsync(Guid workApplicationsId);

        Task ConfirmAsync(Guid workApplicationsId);
    }
}
