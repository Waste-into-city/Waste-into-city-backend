using WasteIntoCity.Api.Extensions.BuilderExtensions;
using WasteIntoCity.Api.Extensions.ServiceExtensions;
using WasteIntoCity.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:7079")
               .AllowCredentials()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

services.AddCustomOptions(configuration);

services.AddCustomValueObjects(configuration);

services.AddCustomMainDbContext(configuration);

services.AddCustomAdapters(configuration);

services.AddCustomBackgroundServices(configuration);

services.AddCustomServices(configuration);

services.AddCustomControllers(configuration);

services.AddCustomSwagger(configuration);

services.AddCustomAuthentication(configuration);

services.AddCustomAuthorization(configuration);

services.AddEndpointsApiExplorer();



WebApplication app = builder.Build();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseCustomMainDbContext(configuration);

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCustomSwagger(configuration);

if (app.Environment.IsDevelopment())
{

}

app.Run();
