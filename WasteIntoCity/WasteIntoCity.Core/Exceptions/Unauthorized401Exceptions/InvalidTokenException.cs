namespace WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions
{
    public class InvalidTokenException : Unauthorized401Exception
    {
        private const string MESSAGE_DEFAULT_FIRST_PART = "Error token";

        private const string MESSAGE_DEFAULT_SECOND_PART = "Access or refresh token is not valid";

        public const string MESSAGE_DEFAULT = $"{MESSAGE_DEFAULT_FIRST_PART} => {MESSAGE_DEFAULT_SECOND_PART}.";

        public InvalidTokenException(string? message, int code)
            : base($"{MESSAGE_DEFAULT_FIRST_PART} => {message ?? MESSAGE_DEFAULT_SECOND_PART}.", code)
        {
        }

        public InvalidTokenException(int code) : base(MESSAGE_DEFAULT, code)
        {
        }
    }
}
