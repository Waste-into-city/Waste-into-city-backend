namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReport
    {
        private TrashcanPointReport(Guid id, bool isReviewed, Guid usersId, Guid trashcanPointsId, DateTime submissionTime)
        {
            Id = id;
            IsReviewed = isReviewed;
            UsersId = usersId;
            TrashcanPointsId = trashcanPointsId;
            SubmissionTime = submissionTime;
        }

        public Guid Id { get; }

        public bool IsReviewed { get; }

        public Guid UsersId { get; }

        public Guid TrashcanPointsId { get; }

        public DateTime SubmissionTime { get; }

        public static TrashcanPointReport Create(Guid id, bool isReviewed, Guid usersId, Guid trashcanPointsId, DateTime submissionTime)
        {
            return new TrashcanPointReport(id, isReviewed, usersId, trashcanPointsId, submissionTime);
        }
    }
}
