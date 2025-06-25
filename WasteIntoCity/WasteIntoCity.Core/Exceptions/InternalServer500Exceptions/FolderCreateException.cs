namespace WasteIntoCity.Core.Exceptions.InternalServer500Exceptions
{
    public class FolderCreateException : InternalServer500Exception
    {
        protected FolderCreateException(string message, int code) : base(message, code)
        {
        }

        public FolderCreateException(int code, string folderPath, string? message)
            : base($"Error: folder {folderPath} => {message ?? "Failed to create folder"}.", code)
        {
        }
    }
}
