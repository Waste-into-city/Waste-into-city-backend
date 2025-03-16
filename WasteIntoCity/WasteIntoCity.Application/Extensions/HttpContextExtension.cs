using Microsoft.AspNetCore.Http;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Application.Types;
using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.Structs;

namespace WasteIntoCity.Application.Extensions
{
    public static class HttpContextExtension
    {
        public static readonly Dictionary<TokenType, string> TokenContextKeys = new Dictionary<TokenType, string>
        {
            {TokenType.ACCESS, "AccessToken" },
            {TokenType.REFRESH, "RefreshToken" },
        };

        public static string TakeTokenValueByTokenType(this HttpContext httpContext, TokenType tokenType)
        {
            string tokenContextKey = TokenContextKeys.GetValueOrDefault(tokenType)!;

            string? accessTokenValue = null;

            try
            {
                accessTokenValue = httpContext.Request.Cookies.Single(x => x.Key == tokenContextKey).Value;
            }
            catch
            {
                throw new NullOrEmptyTokenException(tokenContextKey, null);
            }

            if (accessTokenValue == string.Empty)
            {
                throw new NullOrEmptyTokenException(tokenContextKey, null);
            }

            return accessTokenValue;
        }

        //TODO: Add exceptions
        public static Guid TakeUserIdFromAccessToken(this HttpContext httpContext)
        {
            if (httpContext.User is null)
            {
                throw new Exception();
            }

            Guid userId = Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value)

            return userId;
        }

        public static void AppendTokensContextResponse(this HttpContext httpContext, UserPrepareTokensContextResponse userPrepareTokensContextResponse,
            JwtOptions jwtOptions)
        {
            httpContext.Response.Cookies.Append(TokenContextKeys.GetValueOrDefault(TokenType.ACCESS)!,
                userPrepareTokensContextResponse.AccessTokenValue, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = userPrepareTokensContextResponse.AccessTokenExpiredTimestamp.Add(jwtOptions.AdditionalAccessTokenCookieLifetime)
                });

            httpContext.Response.Cookies.Append(TokenContextKeys.GetValueOrDefault(TokenType.REFRESH)!,
                userPrepareTokensContextResponse.RefreshTokenValue, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = userPrepareTokensContextResponse.RefreshTokenExpiredTimestamp
                });
        }

        public static void DeleteTokensContextResponse(this HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(TokenContextKeys.GetValueOrDefault(TokenType.ACCESS)!);
            httpContext.Response.Cookies.Delete(TokenContextKeys.GetValueOrDefault(TokenType.REFRESH)!);
        }
    }
}
