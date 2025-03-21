namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkApplicationCreateRequest
    {
        required public string Title { get; set; }

        required public string Description { get; set; }

        required public int WorkComplexityId { get; set; }

        required public string Lat { get; set; }

        required public string Lng { get; set; }
    }
}
