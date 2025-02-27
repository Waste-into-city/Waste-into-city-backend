namespace WasteIntoCity.Api.Contracts.V1.Users
{
    public record LoginRequest
    {
        required public string Email { get; init; }
        required public string Password { get; init; }
    }
}
