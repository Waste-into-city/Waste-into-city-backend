namespace WasteIntoCity.Persistance.Entities
{
    public class TrashcanPointReportEntity
    {
        public Guid Id { get; set; }

        public bool IsReviewed { get; set; }

        public Guid UsersId { get; set; }

        public Guid CoordinatesId { get; set; }

        public DateTime SubmissionTime { get; set; }

        public UserEntity? User { get; set; }

        public CoordinatesEntity? Coordinates { get; set; }

        public List<TrashcanPointReportEachMarkEntity> TrashcanPointReportEachMarkList { get; set; } = [];
    }
}
