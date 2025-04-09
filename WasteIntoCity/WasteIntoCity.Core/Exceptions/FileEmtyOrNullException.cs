namespace WasteIntoCity.Core.Exceptions
{
    public class FileEmtyOrNullException : BadRequest400Exception
    {
        public FileEmtyOrNullException(string? message) : base($"Error: null file  => {message ?? "File is null or empty"}.")
        {
        }
    }
}
