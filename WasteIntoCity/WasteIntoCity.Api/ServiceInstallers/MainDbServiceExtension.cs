using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Persistance;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Application.ServiceInstallers
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
