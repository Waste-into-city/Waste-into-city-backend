namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class NotExpiredAccessTokenException : BadRequest400Exception
    {
        public NotExpiredAccessTokenException(string? message, int code)
            : base($"Error Access token => {message ?? "Access token hasn't expired yet"}.", code)
        {
        }

        public NotExpiredAccessTokenException(int code) : base($"Error Access token => Access token hasn't expired yet", code)
        {
        }
    }
}
