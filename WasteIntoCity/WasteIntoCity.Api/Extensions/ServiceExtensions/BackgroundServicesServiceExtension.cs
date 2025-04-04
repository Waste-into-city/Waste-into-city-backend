using WasteIntoCity.Api.BackgroundServices;
using WasteIntoCity.Api.Options;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class BackgroundServicesServiceExtension
    {
        public static IServiceCollection AddCustomBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RefreshTokensCleanupBackgroundServiceOptions>(configuration.GetSection(nameof(RefreshTokensCleanupBackgroundServiceOptions)));

            //return services;

            return services.AddHostedService<RefreshTokensCleanupBackgroundService>();
        }
    }
}
