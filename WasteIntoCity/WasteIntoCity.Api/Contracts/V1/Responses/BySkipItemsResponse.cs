namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record BySkipItemsResponse<T>
    {
        required public int SkippedItems { get; init; }

        required public int Size { get; init; }

        required public List<T> Items { get; init; }

        required public int Total { get; init; }
    }
}
