namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkApplicationCreateRequest
    {
        required public string Title { get; init; }

        required public string Description { get; init; }

        required public int WorkComplexityId { get; init; }

        required public double Lat { get; init; }

        required public double Lng { get; init; }
    }
}
