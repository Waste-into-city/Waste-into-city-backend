namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class FIleContentIsNotImageException : BadRequest400Exception
    {
        public FIleContentIsNotImageException(string message, int code) : base(message, code)
        {
        }

        public FIleContentIsNotImageException(string fileName, string? message, int code)
            : base($"Error: file {fileName} => {message ?? "File content is not image"}.", code)
        {
        }
    }
}
