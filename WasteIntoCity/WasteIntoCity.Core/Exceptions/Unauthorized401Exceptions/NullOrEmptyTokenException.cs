namespace WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions
{
    public class NullOrEmptyTokenException : Unauthorized401Exception
    {
        protected NullOrEmptyTokenException(string message, int code) : base(message, code)
        {
        }

        public NullOrEmptyTokenException(string tokenType, string? message, int code) : base(TakeDefaultMessage(tokenType, message), code)
        {
        }

        public static string TakeDefaultMessage(string tokenType, string? message)
        {
            return $"Error: {tokenType} => {message ?? "Token is null or empty"}.";
        }
    }
}
