using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Notification
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private Notification(Guid id, Title title, Description description, Guid? fromUsersId, Guid toUsersId)
        {
            Id = id;
            Title = title;
            Description = description;
            FromUsersId = fromUsersId;
            ToUsersId = toUsersId;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public Guid? FromUsersId { get; }

        public Guid ToUsersId { get; }

        public static Notification Create(Guid id, Title title, Description description, Guid? fromUsersId, Guid toUsersId)
        {
            return new Notification(id, title, description, fromUsersId, toUsersId);
        }
    }
}
