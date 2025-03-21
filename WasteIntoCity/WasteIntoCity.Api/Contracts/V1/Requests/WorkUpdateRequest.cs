namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkUpdateRequest
    {
        required public string Title { get; init; }

        required public string Description { get; init; }

        required public DateTime StartedDateTime { get; init; }

        required public DateTime FinishDatetime { get; init; }

        required public Guid WorkComplexityId { get; init; }

        required public Guid WorkStatusesId { get; init; }

        required public int CoordinatesId { get; init; }
    }
}
