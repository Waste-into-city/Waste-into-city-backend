namespace WasteIntoCity.Core
{
    public class AuthenticationResult
    {
        public string Token { get; set; } = string.Empty;

        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; } = [];
    }
}
