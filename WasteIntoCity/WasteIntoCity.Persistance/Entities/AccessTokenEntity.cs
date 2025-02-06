namespace WasteIntoCity.Core.Models
{
    public class AccessTokenEntity
    {
        public Guid UserId { get; }

        public string Value { get; } = string.Empty;

        public DateTime ExpiratonTimestamp { get; }
    }
}
