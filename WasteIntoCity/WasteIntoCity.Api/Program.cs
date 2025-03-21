using WasteIntoCity.Api.Extensions.BuilderExtensions;
using WasteIntoCity.Api.Extensions.ServiceExtensions;
using WasteIntoCity.Api.Middleware;
using WasteIntoCity.Application.Services;
using WasteIntoCity.Core.Interfaces.Services;

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

services.AddCustomMainDbContext(configuration);

services.AddScoped<IIdentityService, IdentityService>();
services.AddScoped<IWorksService, WorksService>();

//services.AddAutoMapper();

services.AddControllers();

services.AddCustomAuthentication(configuration);

services.AddCustomAuthorization(configuration);

services.AddCustomSwagger(configuration);

services.AddEndpointsApiExplorer();


WebApplication app = builder.Build();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseCustomMainDbContext(configuration);

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCustomSwagger(configuration);

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    //    c.RoutePrefix = string.Empty;
    //    c.InjectJavascript("/custom-swagger.js");
    //});
}

app.Run();


// TODO: remove naming for roles in database, change token claim role to int, we don't need in types, 
