namespace WasteIntoCity.Core.Errors
{
    public class NullOrWhiteSpaceException : BadRequest400Exception
    {
        protected NullOrWhiteSpaceException(string message) : base(message)
        {
        }

        public NullOrWhiteSpaceException(string argName, string? message) : base($"Error: {argName} => {message ?? "Value should be not null, empty, or only with white spaces."}.")
        {
        }
    }
}
