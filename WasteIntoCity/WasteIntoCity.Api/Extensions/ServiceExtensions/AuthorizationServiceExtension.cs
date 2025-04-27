using Microsoft.AspNetCore.Authorization;
using WasteIntoCity.Api.AuthorizationPolicies;
using WasteIntoCity.Api.AuthorizationPolicies.Handlers;
using WasteIntoCity.Api.AuthorizationPolicies.Requirements;
using WasteIntoCity.Core.Enums;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class AuthorizationServiceExtension
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyType.HONEST_USER, policy =>
                {
                    policy.RequireRole([nameof(RoleEnum.Admin), nameof(RoleEnum.User), nameof(RoleEnum.Moderator)]);
                    policy.AddRequirements(new HonestUserRequirement(1));
                });
            });

            services.AddScoped<IAuthorizationHandler, HonestUserHandler>();

            return services;
        }
    }
}
