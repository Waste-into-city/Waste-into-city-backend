namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkTakePartInFirstSelfRequest
    {
        required public DateTime StartedDatetime { get; init; }
    }
}
