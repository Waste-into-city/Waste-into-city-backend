namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkReportComplaintGetFromQueueResponse
    {
        required public string Title { get; init; }

        required public string Description { get; init; }

        required public DateTime StartedDatime { get; init; }

        required public List<string> ImageNames { get; init; }

        required public Guid WorksId { get; init; }

        required public string FromUserNickname { get; init; }

        required public string FromUserEmail { get; init; }
    }
}
