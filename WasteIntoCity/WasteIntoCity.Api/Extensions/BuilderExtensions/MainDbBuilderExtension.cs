using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Persistance;
using WasteIntoCity.Persistance.Extension;

namespace WasteIntoCity.Api.Extensions.BuilderExtensions
{
    public static class MainDbBuilderExtension
    {
        public static IApplicationBuilder UseCustomMainDbContext(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                MainDbContext context = scope.ServiceProvider.GetRequiredService<MainDbContext>();
                context.Database.Migrate();
                context.SeedRoles();
                context.SeedWorkReportComplaintStatusTypes();
            }

            return app;
        }
    }
}
