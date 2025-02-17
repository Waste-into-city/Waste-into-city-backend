namespace WasteIntoCity.Core.Models
{
    public class WorkReportResultEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid FromParticipantId { get; set; }

        public Guid WorkStatusesId { get; set; }

        public UserEntity? FromParticipant { get; set; }

        public WorkStatusTypeEntity? WorkStatusType { get; set; }

        public List<ImageEntity> Images { get; set; } = [];
    }
}
