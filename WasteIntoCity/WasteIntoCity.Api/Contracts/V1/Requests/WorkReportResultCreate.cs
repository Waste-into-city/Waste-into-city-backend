namespace WasteIntoCity.Api.Contracts.V1.Requests
{
    public record WorkReportResultCreate
    {
        public required string Title { get; init; }

        public required string Description { get; init; }

        public required int WorkComplexityTypesId { get; init; }

        public required int WorkStatusTypesId { get; init; }
    }
}
