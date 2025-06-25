namespace WasteIntoCity.Application.Contracts.V1.Requests
{
    public record WorkReportComplaintCreateRequest
    {
        required public string Title { get; init; }

        required public string Description { get; init; }

        required public Guid WorksId { get; init; }

        required public List<string> ImageNames { get; init; }
    }
}
