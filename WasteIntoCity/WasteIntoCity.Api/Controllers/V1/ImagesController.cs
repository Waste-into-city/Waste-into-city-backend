using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.AuthorizationPolicies;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application;
using WasteIntoCity.Core.Interfaces.Services;

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

        [Authorize(Policy = PolicyType.HONEST_USER)]
        [HttpGet(ApiRoutes.Images.GET_BY_NAME)]
        public IActionResult GetImageByName(string name)
        {
            (Stream imageStream, string mimeType) = _imageService.GetImageStreamAndMimeTypeAsync(name);

            return File(imageStream, mimeType);
        }
    }
}
