using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Persistance;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class MainDbServiceExtension
    {
        public static IServiceCollection AddCustomMainDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(MainDbContext)));
            });

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<RefreshTokensRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IWorksRepository, WorksRepository>();
            services.AddScoped<IWorkApplicationsRepository, WorkApplicationsRepository>();
            services.AddScoped<ICoordinatesRepository, CoordinatesRepository>();
            services.AddScoped<IWorkComplexityTypesRepository, WorkComplexityTypesRepository>();
            services.AddScoped<IWorkMarkTypesRepository, WorkMarkTypesRepository>();
            services.AddScoped<IWorkReportStatusTypesRepository, WorkReportStatusTypesRepository>();
            services.AddScoped<IWorkStatusTypesRepository, WorkStatusTypesRepository>();

            return services;
        }

        //private const string REDIS_DB_CONTEXT = "RedisDbContext";

        //services.AddStackExchangeRedisCache(options =>
        //{
        //    options.Configuration = configuration.GetConnectionString(REDIS_DB_CONTEXT);

        //    if (options.Configuration is null)
        //    {
        //        throw new JsonParamException($"ConnectionStrings:{REDIS_DB_CONTEXT}", null);
        //    }

        //    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
        //    {
        //        AbortOnConnectFail = true,
        //        EndPoints = { options.Configuration }
        //    };
        //});
    }
}
