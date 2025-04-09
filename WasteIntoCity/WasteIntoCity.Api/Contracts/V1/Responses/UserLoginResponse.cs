namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record UserLoginResponse
    {
        required public string AccessTokenValue { get; init; }

        required public string RefreshTokenValue { get; init; }
    }

    public record ImageUploadResponse
    {
        required public string FileName { get; init; }
    }
}
