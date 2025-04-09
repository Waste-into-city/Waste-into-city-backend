namespace WasteIntoCity.Core.Exceptions
{
    public class FolderCreateException : InternalServer500Exception
    {
        protected FolderCreateException(string message) : base(message)
        {
        }

        public FolderCreateException(string folderPath, string? message) : base($"Error: folder {folderPath} => {message ?? "Failed to create folder"}.")
        {
        }
    }
}
