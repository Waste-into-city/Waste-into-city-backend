namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record ImageUploadResponse
    {
        required public string FileName { get; init; }
    }
}
