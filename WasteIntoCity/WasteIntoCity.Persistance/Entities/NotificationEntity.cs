namespace WasteIntoCity.Persistance.Entities
{
    public class NotificationEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid? FromUsersId { get; set; }

        public Guid ToUsersId { get; set; }

        public UserEntity? FromUser { get; set; }

        public UserEntity? ToUser { get; set; }
    }
}
