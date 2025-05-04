namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record AdminPanelGetAllDataResponse
    {
        required public int ScoreSettingsTypesId { get; init; }

        required public int Value { get; init; }
    }
}
