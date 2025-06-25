namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkColleagueReportGetAllByWorksIdResponse
    {
        required public string AboutColleagueNickname { get; init; }

        required public string AboutColleagueEmail { get; init; }

        required public int WorkMarkTypesId { get; init; }

        required public string? AvatarImageName { get; init; }
    }
}
