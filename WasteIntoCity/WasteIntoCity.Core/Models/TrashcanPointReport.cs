namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReport
    {
        public Guid Id { get; set; }

        public bool isReviewed { get; set; }

        public Guid UsersId { get; set; }

        public Guid TrashcanPointsId { get; set; }

        public DateTime SubmissionTime { get; set; }

        public User? User { get; set; }

        public TrashcanPoint? TrashcanPoint { get; set; }

        public List<TrashcanPointReportEachMark> TrashcanPointReportEachMark { get; set; } = [];
    }
}
