namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record UserRegistrationRequest
    {
        required public string Email { get; init; }

        required public string Password { get; init; }

        required public string Nickname { get; init; }
    }
}
