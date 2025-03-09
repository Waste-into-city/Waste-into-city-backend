namespace WasteIntoCity.Core.Errors
{
    public class InvalidTokenException : Unauthorized401Exception
    {
        public InvalidTokenException(string? message) : base($"Error user session => {message ?? "User verification has failed."}")
        {
        }

        public InvalidTokenException() : base($"Error user session => User verification has failed.")
        {
        }
    }
}
