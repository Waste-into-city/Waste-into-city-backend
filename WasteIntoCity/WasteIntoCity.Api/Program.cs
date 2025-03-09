using WasteIntoCity.Application.BuilderExtensions;
using WasteIntoCity.Application.ServiceInstallers;
using WasteIntoCity.Application.Services;
using WasteIntoCity.Core.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;


services.AddCustomMainDbContext(configuration);

services.AddScoped<IIdentityService, IdentityService>();

//services.AddAutoMapper();

services.AddControllers();

services.AddCustomAuthentication(configuration);

services.AddCustomSwagger(configuration);

services.AddEndpointsApiExplorer();


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthentication();

//app.UseAuthorization();

app.MapControllers();

app.UseCustomSwagger(configuration);

app.Run();
