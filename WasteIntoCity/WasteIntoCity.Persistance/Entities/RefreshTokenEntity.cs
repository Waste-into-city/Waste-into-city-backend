namespace WasteIntoCity.Core.Models
{
    public class RefreshTokenEntity
    {
        public Guid UserId { get; set; }

        public string Value { get; set; } = string.Empty;

        public DateTime ExpirationTimestamp { get; set; }
    }
}
