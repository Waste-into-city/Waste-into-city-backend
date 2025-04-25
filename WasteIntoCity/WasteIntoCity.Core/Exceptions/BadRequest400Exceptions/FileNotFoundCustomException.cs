namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class FileNotFoundCustomException : BadRequest400Exception
    {
        public FileNotFoundCustomException(string message, int code) : base(message, code)
        {
        }

        public FileNotFoundCustomException(string fileName, string? message, int code) :
            base($"Error: file {fileName} => {message ?? "Cannot find file"}.", code)
        {
        }
    }
}
