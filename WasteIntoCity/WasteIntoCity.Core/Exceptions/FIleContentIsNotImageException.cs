namespace WasteIntoCity.Core.Exceptions
{
    public class FIleContentIsNotImageException : BadRequest400Exception
    {
        public FIleContentIsNotImageException(string message) : base(message)
        {
        }

        public FIleContentIsNotImageException(string fileName, string? message)
            : base($"Error: file {fileName} => {message ?? "File content is not image"}.")
        {
        }
    }
}
