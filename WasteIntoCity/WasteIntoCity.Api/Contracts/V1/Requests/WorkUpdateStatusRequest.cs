namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkUpdateStatusRequest
    {
        required public int WorkStatusesId { get; init; }
    }
}
