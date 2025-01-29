namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReportsEntity
    {
        public Guid Id { get; }

        public Guid UsersId { get; }

        public Guid TrashcanPointsId { get; }

        public bool isReviewed { get; }

        public DateTime SubmissionTime { get; }
    }
}
