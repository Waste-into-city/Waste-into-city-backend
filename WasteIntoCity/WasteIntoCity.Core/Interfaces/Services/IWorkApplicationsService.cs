namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IWorkApplicationsService
    {
        Task CreateOwnAsync(string title, string description, int workComplexityId, decimal lat, decimal lng, Guid userId);

        Task RejectAsync(Guid workApplicationsId);

        Task ConfirmAsync(Guid workApplicationsId);
    }
}
