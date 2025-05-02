namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkApplicationGetFromQueueResponse
    {
        required public Guid Id { get; init; }

        required public string Title { get; init; }

        required public string Description { get; init; }

        required public decimal Lat { get; init; }

        required public decimal Lng { get; init; }

        required public DateTime StartedDatetime { get; init; }

        required public string FromUserNickname { get; init; }

        required public string FromUserEmail { get; init; }

        required public List<int> TrashTypesIds { get; init; }

        required public List<string> ImageNames { get; init; }
    }
}
