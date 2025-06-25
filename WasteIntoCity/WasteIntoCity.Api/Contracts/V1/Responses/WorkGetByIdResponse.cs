namespace WasteIntoCity.Api.Contracts.V1.Responses
{
    public record WorkGetByIdResponse
    {
        public record WorkGetByIdResponseParticipant
        {
            required public Guid Id { get; init; }

            required public string Nickname { get; init; }

            required public string? AvatarImageName { get; init; }
        }

        required public Guid Id { get; init; }

        required public string Title { get; init; }

        required public string Description { get; init; }

        required public List<WorkGetByIdResponseParticipant> Participants { get; init; }

        required public List<string> ImageNames { get; init; }

        required public List<int> TrashTypesIds { get; init; }

        required public DateTime? StartedDatetime { get; init; }

        required public DateTime? FinishDatetime { get; init; }

        required public int WorkComplexityTypesId { get; init; }

        required public int WorkStatusTypesId { get; init; }

        required public decimal Lat { get; init; }

        required public decimal Lng { get; init; }
    }
}
