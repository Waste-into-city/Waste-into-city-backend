namespace WasteIntoCity.Core.Models
{
    public class NotificationsEntity
    {
        public Guid Id { get; }

        public Guid? FromUsersId { get; }

        public Guid ToUsersId { get; }

        public string Title { get; } = string.Empty;

        public string Description { get; } = string.Empty;
    }
}
