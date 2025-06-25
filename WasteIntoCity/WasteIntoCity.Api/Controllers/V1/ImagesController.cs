using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.AuthorizationPolicies;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Structs;

namespace WasteIntoCity.Api.Controllers.V1
{
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService _imageService;

        public ImagesController(IImagesService imageService)
        {
            _imageService = imageService;
        }

        [Authorize(Policy = PolicyType.HONEST_USER)]
        [HttpPost(ApiRoutes.Images.UPLOAD)]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequest imageUploadRequest)
        {
            string name = await _imageService.SaveImageAsync(imageUploadRequest.File);

            ImageUploadResponse imageUploadResponse = new ImageUploadResponse
            {
                FileName = name
            };

            return Ok(imageUploadResponse);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Images.GET_BY_NAME)]
        public IActionResult GetImageByName(string name)
        {
            ImagesDataStruct imagesDataStruct = _imageService.GetImageStreamAndMimeTypeAsync(name);

            return File(imagesDataStruct.ImageStream, imagesDataStruct.MimeType);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Images.GET_BY_NAMES)]
        public async Task<IActionResult> GetImagesByNames([FromBody] ImagesGetImagesByNamesRequest imagesGetImagesByNamesRequest)
        {
            var result = new List<ImagesGetByImagesByNamesResponse>();

            List<ImagesDataStruct> imageDataStructs = _imageService.GetImageStreamsAndMimeTypes(imagesGetImagesByNamesRequest.Names);

            foreach (var s in imageDataStructs)
            {
                using var ms = new MemoryStream();
                await s.ImageStream.CopyToAsync(ms);
                string base64 = Convert.ToBase64String(ms.ToArray());

                result.Add(new ImagesGetByImagesByNamesResponse
                {
                    Name = s.FileName,
                    MimeType = s.MimeType,
                    Base64 = $"data:{s.MimeType};base64,{base64}"
                });

                s.ImageStream.Dispose();
            }

            return Ok(result);
        }
    }
}
