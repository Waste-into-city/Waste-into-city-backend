namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class BackgroundServicesServiceExtension
    {
        public static IServiceCollection AddCustomBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            //return services.AddHostedService<>();

            return services;
        }
    }
}
