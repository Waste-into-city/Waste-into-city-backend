namespace WasteIntoCity.Core.Errors
{
    public class NullOrWhiteSpaceException : Exception
    {
        protected NullOrWhiteSpaceException(string message) : base(message)
        {
        }

        public NullOrWhiteSpaceException(string argName, string? message) : base($"{argName}: {message ?? "Value should be not null, empty, or only with white spaces."}")
        {
        }
    }
}
