namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkUpdateStatusRequest
    {
        required public Guid WorkStatusesId { get; init; }
    }
}
