namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkReportResultGetResponse
    {
        required public Guid FromParticipantId { get; init; }

        required public string FromParticipantEmail { get; init; }

        required public string FromParticipantNickname { get; init; }

        required public string Title { get; init; }

        required public string Description { get; init; }

        required public int WorkComplexityTypesId { get; init; }

        required public int WorkStatusTypesId { get; init; }
    }
}
