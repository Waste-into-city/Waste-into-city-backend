namespace WasteIntoCity.Core.Models
{
    public class AccessTokensEntity
    {
        public Guid UserId { get; }

        public string Value { get; } = string.Empty;

        public DateTime ExpiratonTimestamp { get; }
    }
}
