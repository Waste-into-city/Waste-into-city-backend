using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WasteIntoCity.Application;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class SwaggerServiceExtension
    {
        private static void AddCustomSecuritySwaggerGenOptions(this SwaggerGenOptions swaggerGenOptions)
        {
            OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "JWT Authorization header using the bearer scheme",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            OpenApiSecurityRequirement securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securityScheme, new List<string>()
                    }
                };

            swaggerGenOptions.AddSecurityDefinition("Bearer", securityScheme);
            swaggerGenOptions.AddSecurityRequirement(securityRequirement);
        }


        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(ApiRoutes.VERSION, new OpenApiInfo
                {
                    Title = "WasteIntoCity API",
                    Version = ApiRoutes.VERSION
                });

                s.AddCustomSecuritySwaggerGenOptions();
            });

            return services;
        }
    }
}
