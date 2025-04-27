using WasteIntoCity.Api.Options;
using WasteIntoCity.Application.BackgroundServices;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class BackgroundServicesServiceExtension
    {
        public static IServiceCollection AddCustomBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FinishedWorksHandlerBackgroundServiceOptions>(configuration.GetSection(
                nameof(FinishedWorksHandlerBackgroundServiceOptions)));
            services.AddHostedService<FinishedWorksHandlerBackgroundService>();

            services.Configure<ImagesCleanupBackgroundServiceOptions>(configuration.GetSection(
                nameof(ImagesCleanupBackgroundServiceOptions)));
            services.AddHostedService<ImagesCleanupBackgroundService>();

            services.Configure<PendingFinalizationWorksHandlerBackgroundServiceOptions>(configuration.GetSection(
                nameof(PendingFinalizationWorksHandlerBackgroundServiceOptions)));
            services.AddHostedService<PendingFinalizationWorksHandlerBackgroundService>();

            services.Configure<PreparingWorksHandlerBackgroundServiceOptions>(configuration.GetSection(
                nameof(PreparingWorksHandlerBackgroundServiceOptions)));
            services.AddHostedService<PreparingWorksHandlerBackgroundService>();

            services.Configure<RefreshTokensCleanupBackgroundServiceOptions>(configuration.GetSection(
                nameof(RefreshTokensCleanupBackgroundServiceOptions)));
            services.AddHostedService<RefreshTokensCleanupBackgroundService>();

            return services;
        }
    }
}
