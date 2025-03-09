namespace WasteIntoCity.Api.Contracts.V1.Requests
{
    public record UserLoginRequest
    {
        required public string Email { get; init; }

        required public string Password { get; init; }
    }
}
