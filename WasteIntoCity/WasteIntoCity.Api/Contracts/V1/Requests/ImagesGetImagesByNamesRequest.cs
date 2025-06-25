namespace WasteIntoCity.Api.Contracts.V1.Requests
{
    public record ImagesGetImagesByNamesRequest
    {
        public required List<string> Names { get; init; }
    }
}
