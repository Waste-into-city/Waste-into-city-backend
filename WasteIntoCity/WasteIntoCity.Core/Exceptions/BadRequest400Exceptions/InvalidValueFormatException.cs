namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class InvalidValueFormatException : BadRequest400Exception
    {
        protected InvalidValueFormatException(string message, int code) : base(message, code)
        {
        }

        public InvalidValueFormatException(string argName, string? message, int code)
            : base($"Error: {argName} => {message ?? "Invalid value format"}.", code)
        {
        }
    }
}
