namespace WasteIntoCity.Core.Exceptions
{
    public class FileNotFoundCustomException : BadRequest400Exception
    {
        public FileNotFoundCustomException(string message) : base(message)
        {
        }

        public FileNotFoundCustomException(string fileName, string? message) : base($"Error: file {fileName} => {message ?? "Cannot find file"}.")
        {
        }
    }
}
