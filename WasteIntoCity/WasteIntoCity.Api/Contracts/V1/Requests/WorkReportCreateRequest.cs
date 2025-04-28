namespace WasteIntoCity.Api.Contracts.V1.Requests
{
    public record WorkReportCreateRequest
    {
        public required string Title { get; init; }

        public required string Description { get; init; }

        public required int WorkComplexityTypesId { get; init; }

        public required int WorkStatusTypesId { get; init; }
    }
}
