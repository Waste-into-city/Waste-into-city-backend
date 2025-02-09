namespace WasteIntoCity.Core.Models
{
    public class WorkReportResult
    {
        public Guid Id { get; }

        public Guid FromParticipantId { get; }

        string Description { get; } = string.Empty;

        public Guid WorkStatusesId { get; }

        public User? FromParticipant { get; set; }

        public WorkStatus? WorkStatus { get; set; }

        public List<Image> Images { get; set; } = [];
    }
}
