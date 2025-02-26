using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WasteIntoCity.Persistance
{
    public static class PersistenceExtensions
    {
        private const string REDIS_DB_CONTEXT = "RedisDbContext";

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
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

            services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(MainDbContext)));
            });

            //services.AddScoped<IImageRepository, ImageRepository>();

            return services;
        }
    }
}
