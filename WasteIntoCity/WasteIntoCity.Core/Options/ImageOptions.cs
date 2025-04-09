namespace WasteIntoCity.Core.Options
{
    public class ImageOptions
    {
        public string FolderPathFromRoute { get; set; } = string.Empty;

        public int MaxSizeBytes { get; set; }

        public Dictionary<string, string> MimeTypes { get; set; } = new Dictionary<string, string>();
    }
}
