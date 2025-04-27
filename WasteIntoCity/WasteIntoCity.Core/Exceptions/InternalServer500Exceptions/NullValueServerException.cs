namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class NullValueServerException : InternalServer500Exception
    {
        protected NullValueServerException(string message, int code) : base(message, code)
        {
        }

        public NullValueServerException(int code, string argName, string? message)
            : base($"Error: {argName} => {message ?? "Value should be not null."}.", code)
        {
        }
    }
}
