namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReportEntity
    {
        public Guid Id { get; set; }

        public bool IsReviewed { get; set; }

        public Guid UsersId { get; set; }

        public Guid TrashcanPointsId { get; set; }

        public DateTime SubmissionTime { get; set; }

        public UserEntity? User { get; set; }

        public TrashcanPointEntity? TrashcanPoint { get; set; }

        public List<TrashcanPointReportEachMarkEntity> TrashcanPointReportEachMarkList { get; set; } = [];
    }
}
