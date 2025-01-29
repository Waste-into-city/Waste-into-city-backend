namespace WasteIntoCity.Core.Models
{
    public class RefreshTokensEntity
    {
        public Guid UserId { get; }

        public string Value { get; } = string.Empty;

        public DateTime ExpirationTimestamp { get; }
    }
}
