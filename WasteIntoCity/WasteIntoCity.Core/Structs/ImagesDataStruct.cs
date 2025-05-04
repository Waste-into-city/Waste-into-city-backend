namespace WasteIntoCity.Core.Structs
{
    public struct ImagesDataStruct
    {
        public string FileName { get; set; }

        public Stream ImageStream { get; set; }

        public string MimeType { get; set; }

        public ImagesDataStruct(string fileName, Stream imageStream, string mimeType)
        {
            FileName = fileName;
            ImageStream = imageStream;
            MimeType = mimeType;
        }
    }
}
