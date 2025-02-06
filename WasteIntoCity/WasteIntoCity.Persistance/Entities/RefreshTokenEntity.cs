namespace WasteIntoCity.Core.Models
{
    public class RefreshTokenEntity
    {
        public Guid UserId { get; }

        public string Value { get; } = string.Empty;

        public DateTime ExpirationTimestamp { get; }
    }
}
