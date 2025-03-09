namespace WasteIntoCity.Api.Contracts.V1.Requests
{
    public record RefreshTokenRequest
    {
        required public string AccessTokenValue { get; init; }

        required public string RefreshTokenValue { get; init; }
    }
}
