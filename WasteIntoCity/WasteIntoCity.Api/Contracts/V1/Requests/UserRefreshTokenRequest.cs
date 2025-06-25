namespace WasteIntoCity.Api.Contracts.V1.Requests
{
    public record UserRefreshTokenRequest
    {
        required public string AccessTokenValue { get; init; }

        required public string RefreshTokenValue { get; init; }
    }
}
