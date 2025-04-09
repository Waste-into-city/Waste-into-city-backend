namespace WasteIntoCity.Core.Exceptions
{
    public class FileTooLargeException : BadRequest400Exception
    {
        public FileTooLargeException(string message) : base(message)
        {
        }

        public FileTooLargeException(string fileName, int maxSize, string? message)
            : base($"Error: file {fileName} => {message ?? "File has too large size"}. Max size = {maxSize}")
        {
        }
    }
}
