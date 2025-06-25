namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class FileCopyException : InternalServer500Exception
    {
        public FileCopyException(string message, int code) : base(message, code)
        {
        }

        public FileCopyException(int code, string fileName, string? message)
            : base($"Error: file {fileName} => {message ?? "Failed copy file"}.", code)
        {
        }
    }
}
