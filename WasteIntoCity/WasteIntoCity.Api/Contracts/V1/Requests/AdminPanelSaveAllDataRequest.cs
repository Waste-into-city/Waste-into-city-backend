namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record AdminPanelSaveAllDataRequest
    {
        required public int ScoreSettingsTypesId { get; init; }

        required public int Value { get; init; }
    }
}
