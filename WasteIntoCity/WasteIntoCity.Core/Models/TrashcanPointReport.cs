namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReport
    {
        private TrashcanPointReport(Guid id, bool isReviewed, Guid usersId, Guid trashcanPointsId, DateTime submissionTime)
        {
            Id = id;
            this.isReviewed = isReviewed;
            UsersId = usersId;
            TrashcanPointsId = trashcanPointsId;
            SubmissionTime = submissionTime;
        }

        public Guid Id { get; }

        public bool isReviewed { get; }

        public Guid UsersId { get; }

        public Guid TrashcanPointsId { get; }

        public DateTime SubmissionTime { get; }

        public TrashcanPointReport Create(Guid id, bool isReviewed, Guid usersId, Guid trashcanPointsId, DateTime submissionTime)
        {
            return new TrashcanPointReport(id, isReviewed, usersId, trashcanPointsId, submissionTime);
        }
    }
}
