namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record ErrorResponse
    {
        public int Code { get; init; }

        public string Message { get; init; } = string.Empty;
    }
}
