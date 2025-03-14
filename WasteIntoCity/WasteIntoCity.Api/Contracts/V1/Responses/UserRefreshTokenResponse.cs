namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record UserRefreshTokenResponse
    {
        required public string AccessTokenValue { get; init; }

        required public string RefreshTokenValue { get; init; }
    }
}
