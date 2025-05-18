using WasteIntoCity.Application.Services;
using WasteIntoCity.Core.Interfaces.Services;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class ContollerServicesServiceExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IWorksService, WorksService>();
            services.AddScoped<IWorkApplicationsService, WorkApplicationsService>();
            services.AddScoped<IImagesService, ImagesService>();
            services.AddScoped<IAdminPanelService, AdminPanelService>();
            services.AddScoped<IWorkColleagueReportsService, WorkColleagueReportsService>();
            services.AddScoped<IWorkReportComplaintsService, WorkReportComplaintsService>();

            return services;
        }
    }
}
