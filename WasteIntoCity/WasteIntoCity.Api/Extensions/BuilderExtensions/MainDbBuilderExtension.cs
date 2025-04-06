using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Persistance;
using WasteIntoCity.Persistance.DefaultInitData;

namespace WasteIntoCity.Api.Extensions.BuilderExtensions
{
    public static class MainDbBuilderExtension
    {
        private static async Task SeedDefaultInitData(IServiceScope scope)
        {
            await scope.ServiceProvider.GetRequiredService<IRolesRepository>().AddAllIfEachNotExist(DefaultData.Roles.ToList());
            await scope.ServiceProvider.GetRequiredService<IWorkComplexityTypesRepository>().AddAllIfEachNotExist(DefaultData.WorkComplexityTypes.ToList());
            await scope.ServiceProvider.GetRequiredService<IWorkMarkTypesRepository>().AddAllIfEachNotExist(DefaultData.WorkMarkTypes.ToList());
            await scope.ServiceProvider.GetRequiredService<IWorkReportStatusTypesRepository>().AddAllIfEachNotExist(DefaultData.WorkReportStatusTypes.ToList());
            await scope.ServiceProvider.GetRequiredService<IWorkStatusTypesRepository>().AddAllIfEachNotExist(DefaultData.WorkStatusTypes.ToList());
            await scope.ServiceProvider.GetRequiredService<IScoreSettingsTypeRepository>().AddAllIfEachNotExist(DefaultData.ScoreSettingsTypes.ToList());

            await scope.ServiceProvider.GetRequiredService<IUsersRepository>().AddAllIfNotExistWithRolesByEmailAsync(DefaultData.Users.ToList());

        }

        public static IApplicationBuilder UseCustomMainDbContext(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<MainDbContext>().Database.Migrate();

                SeedDefaultInitData(scope).Wait();
            }

            return app;
        }
    }
}
