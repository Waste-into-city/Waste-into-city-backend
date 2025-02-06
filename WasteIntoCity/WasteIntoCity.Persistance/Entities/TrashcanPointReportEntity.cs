namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReportEntity
    {
        public Guid Id { get; }

        public Guid UsersId { get; }

        public Guid TrashcanPointsId { get; }

        public bool isReviewed { get; }

        public DateTime SubmissionTime { get; }
    }
}
