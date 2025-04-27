using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Interfaces.Adapters;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Options;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Application.Services
{
    public class ImagesService : IImagesService
    {
        private readonly string _uploadPath;
        private readonly IImagesRepository _imagesRepository;
        private readonly Dictionary<string, string> _mimeTypes;
        private readonly int _maxFileSize;

        public ImagesService(IAppEnvironmentAdapter env, IImagesRepository imagesRepository, IOptions<ImageOptions> imageOptions)
        {
            _uploadPath = Path.Combine(env.GetRootPath(), imageOptions.Value.FolderPathFromRoute);

            if (!Directory.Exists(_uploadPath))
            {
                try
                {
                    Directory.CreateDirectory(_uploadPath);
                }
                catch
                {
                    throw new FolderCreateException(10, imageOptions.Value.FolderPathFromRoute, null);
                }
            }

            _imagesRepository = imagesRepository;
            _mimeTypes = imageOptions.Value.MimeTypes;
            _maxFileSize = imageOptions.Value.MaxSizeBytes;
        }

        private string GetMimeType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            return _mimeTypes.TryGetValue(extension, out string? mime)
                ? mime
                : throw new FileInvalidExtensionException(13, fileName, _mimeTypes.Keys.ToList(), null);
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            //if (file == null || file.Length == 0)
            //{
            //    throw new FileEmtyOrNullException(null);
            //}

            if (file.Length > _maxFileSize)
            {
                throw new FileTooLargeException(17, file.FileName, _maxFileSize, null);
            }

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_mimeTypes.ContainsKey(extension))
            {
                throw new FileInvalidExtensionException(14, file.FileName, _mimeTypes.Keys.ToList(), null);
            }

            string expectedMimeType = _mimeTypes[extension];
            if (!string.Equals(file.ContentType, expectedMimeType, StringComparison.OrdinalIgnoreCase))
            {
                throw new FileMimeTypeException(15, file.FileName, _mimeTypes, file.ContentType, null);
            }

            try
            {
                using Stream imageStream = file.OpenReadStream();
                using SixLabors.ImageSharp.Image image = await SixLabors.ImageSharp.Image.LoadAsync(imageStream);
            }
            catch (SixLabors.ImageSharp.UnknownImageFormatException)
            {
                throw new FIleContentIsNotImageException(file.FileName, null, 12);
            }

            string fileName = Path.GetRandomFileName() + extension;
            string filePath = Path.Combine(_uploadPath, fileName);

            try
            {
                using FileStream stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch
            {
                throw new FileCopyException(8, fileName, null);
            }

            Core.Models.Image imageEntity = Core.Models.Image.Create(
                Guid.NewGuid(),
                ImageName.Create(fileName),
                DateTime.UtcNow,
                null, null, null, null
            );

            await _imagesRepository.Create(imageEntity);

            return fileName;
        }


        public (Stream imageStream, string mimeType) GetImageStreamAndMimeTypeAsync(string fileName)
        {
            string filePath = Path.Combine(_uploadPath, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundCustomException(fileName, null, 16);

            return (new FileStream(filePath, FileMode.Open, FileAccess.Read), GetMimeType(fileName));
        }
    }
}
