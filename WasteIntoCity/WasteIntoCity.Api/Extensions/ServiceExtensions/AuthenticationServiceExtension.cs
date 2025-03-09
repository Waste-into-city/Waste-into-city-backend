using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WasteIntoCity.Application.Options;

namespace WasteIntoCity.Api.Extensions.ServiceExtensions
{
    public static class AuthenticationServiceExtension
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtOptions jwtOptions = new JwtOptions();
            configuration.Bind(nameof(jwtOptions), jwtOptions);

            services.AddSingleton(jwtOptions);

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = false
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(a =>
                {
                    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(j =>
                {
                    j.SaveToken = true;
                    j.TokenValidationParameters = tokenValidationParameters;
                }
            );

            return services;
        }
    }
}
