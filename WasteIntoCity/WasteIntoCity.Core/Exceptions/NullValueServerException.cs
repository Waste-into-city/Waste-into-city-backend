namespace WasteIntoCity.Core.Exceptions
{
    public class NullValueServerException : InternalServer500Exception
    {
        protected NullValueServerException(string message) : base(message)
        {
        }

        public NullValueServerException(string argName, string? message) : base($"Error: {argName} => {message ?? "Value should be not null."}.")
        {
        }
    }
}
