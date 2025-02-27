using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Persistance;

namespace WasteIntoCity.Api.ServiceInstallers
{
    public static class MainDbServiceExtension
    {
        public static IServiceCollection AddCustomMainDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(MainDbContext)));
            });

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

        //services.AddScoped<IImageRepository, ImageRepository>();
    }
}
