namespace WasteIntoCity.Application.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; } = string.Empty;

        public TimeSpan RefreshTokenLifetime { get; set; }

        public TimeSpan AccessTokenLifetime { get; set; }

        public TimeSpan AdditionalAccessTokenCookieLifetime { get; set; }
    }
}
