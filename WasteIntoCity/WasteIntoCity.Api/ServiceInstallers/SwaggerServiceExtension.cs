using Microsoft.OpenApi.Models;
using WasteIntoCity.Api.Contracts.V1;

namespace WasteIntoCity.Api.ServiceInstallers
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(ApiRoutes.VERSION, new OpenApiInfo
                {
                    Title = "WasteIntoCity API",
                    Version = ApiRoutes.VERSION
                });
            });

            return services;
        }
    }
}
