namespace WasteIntoCity.Core.Models
{
    public class WorkReportResultsEntity
    {
        public Guid Id { get; }

        public Guid FromParticipantId { get; }

        string Description { get; } = string.Empty;

        public Guid WorkStatusesId { get; }
    }
}
