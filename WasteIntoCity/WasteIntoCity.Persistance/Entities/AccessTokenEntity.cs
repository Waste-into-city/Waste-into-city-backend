namespace WasteIntoCity.Core.Models
{
    public class AccessTokenEntity
    {
        public Guid UserId { get; set; }

        public string Value { get; set; } = string.Empty;

        public DateTime ExpiratonTimestamp { get; set; }
    }
}
