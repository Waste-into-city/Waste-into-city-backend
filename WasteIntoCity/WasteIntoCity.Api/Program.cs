using WasteIntoCity.Api.BuilderExtensions;
using WasteIntoCity.Api.ServiceInstallers;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;


services.AddCustomMainDbContext(configuration);

services.AddControllers();

services.AddCustomSwagger(configuration);

services.AddEndpointsApiExplorer();


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.UseCustomSwagger(configuration);

app.Run();
