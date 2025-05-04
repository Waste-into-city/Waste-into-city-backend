namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record IdentityGetUserInfoResponse
    {
        required public Guid Id { get; init; }

        required public string Nickname { get; init; }

        required public string? AvatarImageName { get; init; }
    }
}
