using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Options;

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
                Type = SecuritySchemeType.Http,
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
                        securityScheme, Array.Empty<string>()
                    }
                };

            swaggerGenOptions.AddSecurityDefinition("Bearer", securityScheme);
            swaggerGenOptions.AddSecurityRequirement(securityRequirement);
        }


        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiRoutes.VERSION, new OpenApiInfo
                {
                    Title = "WasteIntoCity API",
                    Version = ApiRoutes.VERSION
                });

                //services.AddSwaggerGenNewtonsoftSupport();
                options.AddCustomSecuritySwaggerGenOptions();
            });

            SwaggerOptions swaggerOptions = new SwaggerOptions();

            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            services.Configure<SwaggerOptions>(configuration.GetSection(nameof(SwaggerOptions)));

            return services;
        }
    }
}
