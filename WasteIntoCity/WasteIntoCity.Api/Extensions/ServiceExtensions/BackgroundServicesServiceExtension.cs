using WasteIntoCity.Application.BackgroundServices;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class BackgroundServicesServiceExtension
    {
        public static IServiceCollection AddCustomBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<FinishedWorksHandlerBackgroundService>();

            services.AddHostedService<ImagesCleanupBackgroundService>();

            services.AddHostedService<PendingFinalizationWorksHandlerBackgroundService>();

            services.AddHostedService<PreparingWorksHandlerBackgroundService>();

            services.AddHostedService<RefreshTokensCleanupBackgroundService>();

            return services;
        }
    }
}
