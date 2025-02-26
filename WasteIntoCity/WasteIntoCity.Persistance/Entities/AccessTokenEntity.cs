namespace WasteIntoCity.Core.Models
{
    public class AccessTokenEntity
    {
        public Guid UserId { get; set; }

        public string Value { get; set; } = string.Empty;

        public DateTime ExpirationTimestamp { get; set; }

        public UserEntity? User { get; set; }
    }
}
