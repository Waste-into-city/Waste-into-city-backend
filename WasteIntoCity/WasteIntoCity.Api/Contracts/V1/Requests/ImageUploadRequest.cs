namespace WasteIntoCity.Api.Contracts.V1.Requests
{
    public class ImageUploadRequest
    {
        public required IFormFile File { get; set; }
    }
}
