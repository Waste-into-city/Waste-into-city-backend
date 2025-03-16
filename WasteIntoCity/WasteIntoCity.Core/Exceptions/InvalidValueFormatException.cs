namespace WasteIntoCity.Core.Errors
{
    public class InvalidValueFormatException : BadRequest400Exception
    {
        protected InvalidValueFormatException(string message) : base(message)
        {
        }

        public InvalidValueFormatException(string argName, string? message) : base($"Error: {argName} => {message ?? "Invalid value format"}.")
        {
        }
    }
}
