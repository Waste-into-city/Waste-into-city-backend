namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkReportComplaintGetResponse
    {
        required public string Title { get; init; }

        required public string Description { get; init; }

        required public string WorksId { get; init; }

        required public string WorksTitle { get; init; }

        required public string FromUsersId { get; init; }

        required public string FromUsersEmail { get; init; }

        required public string FromUsersNickname { get; init; }

        required public string WorkReportStatusTypesId { get; init; }

        required public DateTime StartedDatetime { get; init; }
    }
}
