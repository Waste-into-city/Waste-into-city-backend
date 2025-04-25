namespace WasteIntoCity.Core.Exceptions.BadRequest400Exceptions
{
    public class FileInvalidExtensionException : BadRequest400Exception
    {
        public FileInvalidExtensionException(string message, int code) : base(message, code)
        {
        }

        public FileInvalidExtensionException(int code, string fileName, List<string> extensions, string? message)
            : base($"Error: file {fileName} => {message ?? "File extension is not supported"}. Send file with" +
                  $" supported extension ({string.Join(", ", extensions)}).", code)
        {
        }
    }
}
