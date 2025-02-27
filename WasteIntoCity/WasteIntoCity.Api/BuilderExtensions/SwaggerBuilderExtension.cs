
using WasteIntoCity.Api.Options;

namespace WasteIntoCity.Api.BuilderExtensions
{
    public static class SwaggerBuilderExtension
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            SwaggerOptions swaggerOptions = new SwaggerOptions();

            configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });

            return app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndPoint, swaggerOptions.Description);
            });
        }
    }
}
