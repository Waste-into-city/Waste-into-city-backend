namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class AuthorizationServiceExtension
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("HonestUser", builder =>
                //{
                //    builder.RequireRole()
                //});
            });

            return services;
        }
    }
}
