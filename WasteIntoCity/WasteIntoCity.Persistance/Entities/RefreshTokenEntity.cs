namespace WasteIntoCity.Persistance.Entities
{
    public class RefreshTokenEntity
    {
        public Guid Value { get; set; }

        public string JwtId { get; set; } = string.Empty;

        public DateTime CreationTimestamp { get; set; }

        public DateTime ExpirationTimestamp { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public Guid UserId { get; set; }

        public UserEntity? User { get; set; }
    }
}
