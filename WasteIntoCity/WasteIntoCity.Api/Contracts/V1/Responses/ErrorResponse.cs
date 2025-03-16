namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record ErrorResponse
    {
        public string Message { get; init; } = string.Empty;
    }
}
