using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Notification
    {
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

        public Notification Create(Guid id, Title title, Description description, Guid? fromUsersId, Guid toUsersId)
        {
            return new Notification(id, title, description, fromUsersId, toUsersId);
        }
    }
}
