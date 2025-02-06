namespace WasteIntoCity.Core.Models
{
    public class WorkReportResultEntity
    {
        public Guid Id { get; }

        public Guid FromParticipantId { get; }

        string Description { get; } = string.Empty;

        public Guid WorkStatusesId { get; }
    }
}
