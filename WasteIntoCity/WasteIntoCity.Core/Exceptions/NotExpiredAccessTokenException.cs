namespace WasteIntoCity.Core.Errors
{
    public class NotExpiredAccessTokenException : BadRequest400Exception
    {
        public NotExpiredAccessTokenException(string? message) : base($"Error Access token => {message ?? "Access token hasn't expired yet"}.")
        {
        }

        public NotExpiredAccessTokenException() : base($"Error Access token => Access token hasn't expired yet")
        {
        }
    }
}
