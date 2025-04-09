using WasteIntoCity.Api.Adapters;
using WasteIntoCity.Core.Interfaces.Adapters;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class AdapterServiceExtension
    {
        public static IServiceCollection AddCustomAdapters(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddScoped<IAppEnvironmentAdapter, WebAppEnvironmentAdapter>();
        }
    }
}
