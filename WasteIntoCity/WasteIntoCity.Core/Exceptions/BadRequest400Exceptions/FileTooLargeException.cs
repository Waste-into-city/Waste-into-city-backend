namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class FileTooLargeException : BadRequest400Exception
    {
        public FileTooLargeException(string message, int code) : base(message, code)
        {
        }

        public FileTooLargeException(int code, string fileName, int maxSize, string? message)
            : base($"Error: file {fileName} => {message ?? "File has too large size"}. Max size = {maxSize}", code)
        {
        }
    }
}
