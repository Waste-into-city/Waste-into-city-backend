using WasteIntoCity.Api.Extensions.BuilderExtensions;
using WasteIntoCity.Api.Extensions.ServiceExtensions;
using WasteIntoCity.Api.Middleware;

DotNetEnv.Env.Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;

string[]? allowedOrigins = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS")
    ?.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

//var certPath = Path.Combine(AppContext.BaseDirectory, "localhost.pfx");

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(8081, listenOptions =>
//    {
//        listenOptions.UseHttps(certPath, "MyPassword123");
//    });
});

services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.SetIsOriginAllowed(origin => allowedOrigins?.Contains(origin) ?? true)
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
