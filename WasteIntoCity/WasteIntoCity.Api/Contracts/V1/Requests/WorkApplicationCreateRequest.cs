namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkApplicationCreateRequest
    {
        required public string Title { get; init; }

        required public string Description { get; init; }

        required public int WorkComplexityId { get; init; }

        required public int[] TrashTypeIds { get; init; }

        required public decimal Lat { get; init; }

        required public decimal Lng { get; init; }
    }
}
