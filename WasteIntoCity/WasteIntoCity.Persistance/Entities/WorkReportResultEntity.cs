namespace WasteIntoCity.Core.Models
{
    public class WorkReportResultEntity
    {
        public Guid Id { get; }

        string Description { get; } = string.Empty;

        public Guid FromParticipantId { get; }

        public Guid WorkStatusesId { get; }

        public UserEntity? FromParticipant { get; set; }

        public WorkStatusEntity? WorkStatus { get; set; }

        public List<ImageEntity> Images { get; set; } = [];
    }
}
