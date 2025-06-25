namespace WasteIntoCity.Api.Contracts.V1.Requests
{
    public record ImageUploadRequest
    {
        public required IFormFile File { get; init; }
    }
}
