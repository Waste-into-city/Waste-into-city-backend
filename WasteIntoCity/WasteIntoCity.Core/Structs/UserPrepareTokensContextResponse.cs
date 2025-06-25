namespace WasteIntoCity.Core.Structs
{
    public record UserPrepareTokensContextResponse
    {
        required public string AccessTokenValue { get; init; }

        required public string RefreshTokenValue { get; init; }

        required public DateTime AccessTokenExpiredTimestamp { get; init; }

        required public DateTime RefreshTokenExpiredTimestamp { get; init; }
    }
}
