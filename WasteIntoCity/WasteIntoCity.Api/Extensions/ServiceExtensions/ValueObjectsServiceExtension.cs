using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Options;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class ValueObjectsServiceExtension
    {
        private static IServiceCollection AddImageName(this IServiceCollection services, IConfiguration configuration)
        {
            ImageOptions imageOptions = configuration.GetSection(nameof(ImageOptions)).Get<ImageOptions>()
                ?? throw new NullValueServerException(14, nameof(ImageOptions), "Image options are null or empty");

            ImageName.Configure(imageOptions);

            return services;
        }

        public static IServiceCollection AddCustomValueObjects(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddImageName(configuration);

            return services;
        }
    }

    public static class OptionsServiceExtension
    {
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FinishedWorksHandlerBackgroundServiceOptions>(configuration.GetSection(
                nameof(FinishedWorksHandlerBackgroundServiceOptions)));

            services.Configure<ImagesCleanupBackgroundServiceOptions>(configuration.GetSection(
              nameof(ImagesCleanupBackgroundServiceOptions)));

            services.Configure<PendingFinalizationWorksHandlerBackgroundServiceOptions>(configuration.GetSection(
                nameof(PendingFinalizationWorksHandlerBackgroundServiceOptions)));

            services.Configure<PreparingWorksHandlerBackgroundServiceOptions>(configuration.GetSection(
                nameof(PreparingWorksHandlerBackgroundServiceOptions)));

            services.Configure<RefreshTokensCleanupBackgroundServiceOptions>(configuration.GetSection(
                nameof(RefreshTokensCleanupBackgroundServiceOptions)));

            services.Configure<PreparingWorksStatusOptions>(configuration.GetSection(nameof(PreparingWorksStatusOptions)));

            services.Configure<ImageOptions>(configuration.GetSection(nameof(ImageOptions)));

            return services;
        }
    }
}
