namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record UserUpdateOwnUserInfo
    {
        required public string Email { get; init; }

        required public string? Password { get; init; }

        required public string? NewPassword { get; init; }

        required public string Nickname { get; init; }

        required public string? AvatarImageName { get; init; }
    }
}
