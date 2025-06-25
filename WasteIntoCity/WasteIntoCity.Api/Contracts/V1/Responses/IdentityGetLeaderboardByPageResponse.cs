namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record IdentityGetLeaderboardByPageResponse
    {
        required public string Nickname { get; init; }

        required public string Email { get; init; }

        required public int Ranking { get; init; }

        required public string? AvatarImageName { get; init; }
    }
}
