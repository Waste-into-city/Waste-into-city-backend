namespace WasteIntoCity.Core.Errors
{
    public class InvalidValueFormatException : Exception
    {
        protected InvalidValueFormatException(string message) : base(message)
        {
        }

        public InvalidValueFormatException(string argName, string? message) : base($"{argName}: {message ?? "Invalid value format"}.")
        {
        }
    }
}
