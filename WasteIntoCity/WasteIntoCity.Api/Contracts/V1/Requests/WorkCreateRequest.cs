namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkCreateRequest
    {
        required public string Title { get; init; }

        required public string Description { get; init; }

        required public DateTime StartedDateTime { get; init; }

        required public DateTime FinishDatetime { get; init; }

        required public Guid WorkComplexityId { get; init; }

        required public Guid WorkStatusesId { get; init; }
    }
}
