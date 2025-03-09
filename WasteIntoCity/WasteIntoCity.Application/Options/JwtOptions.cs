namespace WasteIntoCity.Application.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; } = string.Empty;

        public TimeSpan TokenLifetime { get; set; }
    }
}
