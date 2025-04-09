namespace WasteIntoCity.Core.Exceptions
{
    public class FileInvalidExtensionException : BadRequest400Exception
    {
        public FileInvalidExtensionException(string message) : base(message)
        {
        }

        public FileInvalidExtensionException(string fileName, List<string> extensions, string? message)
            : base($"Error: file {fileName} => {message ?? "File extension is not supported"}. Send file with" +
                  $" supported extension ({string.Join(", ", extensions)}).")
        {
        }
    }
}
