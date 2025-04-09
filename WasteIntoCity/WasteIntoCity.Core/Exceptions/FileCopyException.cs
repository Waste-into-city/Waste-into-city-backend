namespace WasteIntoCity.Core.Exceptions
{
    public class FileCopyException : InternalServer500Exception
    {
        public FileCopyException(string message) : base(message)
        {
        }

        public FileCopyException(string fileName, string? message) : base($"Error: file {fileName} => {message ?? "Failed copy file"}.")
        {
        }
    }
}
