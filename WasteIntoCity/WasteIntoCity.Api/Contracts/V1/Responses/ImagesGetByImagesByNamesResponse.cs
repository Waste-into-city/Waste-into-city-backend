namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record ImagesGetByImagesByNamesResponse
    {
        public required string Name { get; init; }

        public required string MimeType { get; init; }

        public required string Base64 { get; init; }
    }
}
