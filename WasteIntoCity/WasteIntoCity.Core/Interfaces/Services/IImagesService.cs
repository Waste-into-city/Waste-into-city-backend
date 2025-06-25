using Microsoft.AspNetCore.Http;
using WasteIntoCity.Core.Structs;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IImagesService
    {
        Task<string> SaveImageAsync(IFormFile file);

        ImagesDataStruct GetImageStreamAndMimeTypeAsync(string fileName);

        List<ImagesDataStruct> GetImageStreamsAndMimeTypes(List<string> fileNames);
    }
}
