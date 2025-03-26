using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Application.Types;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Repositories;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

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
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = ClaimTypes.Role
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

                    j.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
                            {
                                return Task.CompletedTask;
                            }

                            TokenType accessTokenType = TokenType.ACCESS;
                            string? accessTokenValue = null;

                            try
                            {
                                accessTokenValue = context.HttpContext.TakeTokenValueByTokenType(accessTokenType);
                            }
                            catch
                            {
                                context.Fail(NullOrEmptyTokenException.TakeDefaultMessage(
                                    HttpContextExtension.TokenContextKeys.GetValueOrDefault(accessTokenType)!, null));

                                return Task.CompletedTask;
                            }

                            context.Token = accessTokenValue;

                            return Task.CompletedTask;
                        },
                        OnTokenValidated = async context =>
                        {
                            if (context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
                            {
                                return;
                            }

                            if (context.SecurityToken is JsonWebToken accesstoken)
                            {
                                RefreshTokensRepository refreshTokensRepository = context.HttpContext.RequestServices
                                    .GetRequiredService<RefreshTokensRepository>();

                                RefreshTokenEntity? refreshTokenEntity = null;

                                try
                                {
                                    string jti = accesstoken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                                    refreshTokenEntity = await refreshTokensRepository.FindByJwtIdAsync(jti);
                                }
                                catch
                                {
                                    context.Fail(InvalidTokenException.MESSAGE_DEFAULT);
                                    return;
                                }

                                if (refreshTokenEntity?.Used == true || refreshTokenEntity?.Invalidated == true)
                                {
                                    context.Fail(InvalidTokenException.MESSAGE_DEFAULT);
                                    return;
                                }
                            }
                            else
                            {
                                context.Fail(InvalidTokenException.MESSAGE_DEFAULT);
                                return;
                            }
                        },
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception is not null)
                            {
                                throw new InvalidTokenException(context.Exception?.Message);
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                    };
                }
            );

            return services;
        }
    }
}
