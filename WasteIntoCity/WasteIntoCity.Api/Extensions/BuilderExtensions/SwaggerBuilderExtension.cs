using Microsoft.Extensions.Options;
using WasteIntoCity.Application.Options;

namespace WasteIntoCity.Api.Extensions.BuilderExtensions
{
    public static class SwaggerBuilderExtension
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            SwaggerOptions swaggerOptions = app.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;

            app.UseSwagger(options =>
            {
                options.RouteTemplate = $"{swaggerOptions.ControllerName}/{swaggerOptions.JsonLocalRoute}";
            });

            return app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndPoint, swaggerOptions.Description);
            })
            .UseEndpoints(endpoints =>
            {
                endpoints.MapSwagger().AllowAnonymous();
            });
        }
    }
}
