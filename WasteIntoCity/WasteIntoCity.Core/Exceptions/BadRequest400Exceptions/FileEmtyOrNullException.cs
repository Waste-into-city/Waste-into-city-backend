namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class FileEmtyOrNullException : BadRequest400Exception
    {
        public FileEmtyOrNullException(string? message, int code) :
            base($"Error: null file  => {message ?? "File is null or empty"}.", code)
        {
        }
    }
}
