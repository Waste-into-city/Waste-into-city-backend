namespace WasteIntoCity.Application.Contracts.V1.Users
{
    public record RegisterRequest
    {
        required public string Nickname { get; init; }
        required public string Email { get; init; }
        required public string Password { get; init; }
    }
}
