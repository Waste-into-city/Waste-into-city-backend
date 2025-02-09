namespace WasteIntoCity.Core.Models
{
    public class Notification
    {
        private const int TITLE_LENGTH_MIN = 20;

        private const int TITLE_LENGTH_MAX = 255;

        private const int DESCRIPTION_LENGTH_MIN = 0;

        private const int DESCRIPTION_LENGTH_MAX = ;

        private Notification(Guid id, string title, string description, Guid? fromUsersId, Guid toUsersId)
        {
            Id = id;
            Title = title;
            Description = description;
            FromUsersId = fromUsersId;
            ToUsersId = toUsersId;
        }

        public Guid Id { get; }

        public string Title { get; }

        public string Description { get; }

        public Guid? FromUsersId { get; }

        public Guid ToUsersId { get; }

        public Notification Create(Guid id, string title, string description, Guid? fromUsersId, Guid toUsersId)
        {

        }
    }
}
