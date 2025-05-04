namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class NotAllowedStatusException : BadRequest400Exception
    {
        public NotAllowedStatusException(int code, string message) : base(message, code)
        {
        }
    }
}
