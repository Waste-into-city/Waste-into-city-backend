namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class NullOrWhiteSpaceException : BadRequest400Exception
    {
        protected NullOrWhiteSpaceException(string message, int code) : base(message, code)
        {
        }

        public NullOrWhiteSpaceException(string argName, string? message, int code)
            : base($"Error: {argName} => {message ?? "Value should be not null, empty, or only with white spaces."}.", code)
        {
        }
    }
}
