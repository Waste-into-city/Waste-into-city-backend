namespace WasteIntoCity.Core.Exceptions
{
    public class FileMimeTypeException : BadRequest400Exception
    {
        public FileMimeTypeException(string message) : base(message)
        {
        }

        public FileMimeTypeException(string fileName, Dictionary<string, string> mimeTypes, string contentType, string? message)
            : base($"Error: file {fileName} => {message ?? "File extension doesn't match to content type "}{contentType}. " +
                  $"Mime types: {string.Join(", ", mimeTypes.Select(kv => $"{kv.Key}->{kv.Value}"))}")
        {
        }
    }
}
