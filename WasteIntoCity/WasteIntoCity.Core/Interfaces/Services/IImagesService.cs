using Microsoft.AspNetCore.Http;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IImagesService
    {
        Task<string> SaveImageAsync(IFormFile file);

        (Stream imageStream, string mimeType) GetImageStreamAndMimeTypeAsync(string fileName);
    }
}
