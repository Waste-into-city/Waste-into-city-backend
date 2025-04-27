namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkGetAllOwnTakePartInResponse
    {
        required public Guid Id { get; init; }

        required public string Title { get; init; }

        required public string Description { get; init; }

        required public DateTime? StartedDatetime { get; init; }

        required public DateTime? FinishDatetime { get; init; }

        required public int WorkComplexityTypesId { get; init; }

        required public int WorkStatusTypesId { get; init; }

        required public string Lat { get; init; }

        required public string Lng { get; init; }
    }
}
