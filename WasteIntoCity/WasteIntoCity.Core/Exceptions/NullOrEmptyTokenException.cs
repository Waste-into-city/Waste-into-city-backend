namespace WasteIntoCity.Core.Exceptions
{
    public class NullOrEmptyTokenException : Unauthorized401Exception
    {
        protected NullOrEmptyTokenException(string message) : base(message)
        {
        }

        public NullOrEmptyTokenException(string tokenType, string? message) : base(TakeDefaultMessage(tokenType, message))
        {
        }

        public static string TakeDefaultMessage(string tokenType, string? message)
        {
            return $"Error: {tokenType} => {message ?? "Token is null or empty"}.";
        }
    }
}
