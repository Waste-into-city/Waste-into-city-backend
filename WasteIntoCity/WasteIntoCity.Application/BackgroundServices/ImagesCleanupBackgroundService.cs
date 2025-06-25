using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Interfaces.Adapters;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Options;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Application.BackgroundServices
{
    public class ImagesCleanupBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<ImageOptions> _imageOptions;
        private readonly IOptions<ImagesCleanupBackgroundServiceOptions> _serviceOptions;

        public ImagesCleanupBackgroundService(IServiceScopeFactory scopeFactory, IOptions<ImageOptions> imageOptions,
            IOptions<ImagesCleanupBackgroundServiceOptions> serviceOptions)
        {
            _scopeFactory = scopeFactory;
            _imageOptions = imageOptions;
            _serviceOptions = serviceOptions;
        }

        private async Task HandleImagesAsync(IImagesRepository imagesRepository, IAppEnvironmentAdapter appEnvironmentAdapter,
             CancellationToken stoppingToken)
        {
            string uploadPath = Path.Combine(appEnvironmentAdapter.GetRootPath(), _imageOptions.Value.FolderPathFromRoute);

            if (!Directory.Exists(uploadPath))
            {
                throw new FolderCreateException(9, _imageOptions.Value.FolderPathFromRoute, null);
            }

            List<ImageName> imageNames = await imagesRepository.
                FindNamesFirstNotReferencedByUploadedTimeInterval(_serviceOptions.Value.ImagesAtTimeAmount, _serviceOptions.Value.MinImageIntervalAfterUploaded);

            List<string> imagesForDelete = new List<string>(imageNames.Count);

            int i = 0;
            while (i < imageNames.Count && !stoppingToken.IsCancellationRequested)
            {
                bool isDeleted = true;
                try
                {
                    File.Delete(Path.Combine(uploadPath, imageNames[i].Value));
                }
                catch
                {
                    isDeleted = false;

                    Console.WriteLine($"Cannot delete image {imageNames[i].Value}");
                }

                if (isDeleted)
                {
                    imagesForDelete.Add(imageNames[i].Value);
                }

                i++;
            }

            await imagesRepository.DeleteByNames(imagesForDelete);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(_serviceOptions.Value.StartServiceWaitingTime, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();

                IImagesRepository imagesRepository = scope.ServiceProvider.GetRequiredService<IImagesRepository>();
                IAppEnvironmentAdapter appEnvironmentAdapter = scope.ServiceProvider.GetRequiredService<IAppEnvironmentAdapter>();

                await HandleImagesAsync(imagesRepository, appEnvironmentAdapter, stoppingToken);

                await Task.Delay(_serviceOptions.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
