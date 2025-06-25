namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorksGetAllLookupResponse
    {
        required public Guid Id { get; init; }

        required public decimal Lat { get; init; }

        required public decimal Lng { get; init; }
    }
}
