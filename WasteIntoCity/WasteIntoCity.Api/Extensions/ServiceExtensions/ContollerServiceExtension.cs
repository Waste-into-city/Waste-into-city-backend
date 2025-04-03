using WasteIntoCity.Api.Filters;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class ContollerServiceExtension
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<RequestsValidationFilter>();
            });

            return services;
        }
    }
}
