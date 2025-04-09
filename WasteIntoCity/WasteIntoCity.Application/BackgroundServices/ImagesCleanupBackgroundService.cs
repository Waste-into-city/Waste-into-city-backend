using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Interfaces.Adapters;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Api.BackgroundServices
{
    public class ImagesCleanupBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IOptions<ImagesCleanupBackgroundServiceOptions> _options;

        private const string IMAGES_PATH = "uploads";

        public ImagesCleanupBackgroundService(IServiceScopeFactory scopeFactory, IOptions<ImagesCleanupBackgroundServiceOptions> options)
        {
            _scopeFactory = scopeFactory;
            _options = options;
        }

        private async Task HandleImagesAsync(IImagesRepository imagesRepository, IAppEnvironmentAdapter appEnvironmentAdapter,
             CancellationToken stoppingToken)
        {
            if (!Directory.Exists(IMAGES_PATH))
            {
                throw new FolderCreateException(IMAGES_PATH, null);
            }

            string uploadPath = Path.Combine(appEnvironmentAdapter.GetRootPath(), IMAGES_PATH);

            List<ImageName> imageNames = await imagesRepository.FindNamesFirstNotReferencedByFinished(_options.Value.ImagesAtTimeAmount, _options.Value.MinImageIntervalAfterUploaded);

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
            await Task.Delay(_options.Value.IntervalTime, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();

                IImagesRepository imagesRepository = scope.ServiceProvider.GetRequiredService<IImagesRepository>();
                IAppEnvironmentAdapter appEnvironmentAdapter = scope.ServiceProvider.GetRequiredService<IAppEnvironmentAdapter>();

                await HandleImagesAsync(imagesRepository, appEnvironmentAdapter, stoppingToken);

                await Task.Delay(_options.Value.IntervalTime, stoppingToken);
            }
        }
    }
}
