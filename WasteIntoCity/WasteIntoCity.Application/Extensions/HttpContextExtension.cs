using Microsoft.AspNetCore.Http;
using WasteIntoCity.Application.Enum;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions;
using WasteIntoCity.Core.Structs;

namespace WasteIntoCity.Application.Extensions
{
    public static class HttpContextExtension
    {
        public static readonly Dictionary<TokenEnum, string> TokenContextKeys = new Dictionary<TokenEnum, string>
        {
            {TokenEnum.ACCESS, "AccessToken" },
            {TokenEnum.REFRESH, "RefreshToken" },
        };

        public static string TakeTokenValueByTokenType(this HttpContext httpContext, TokenEnum tokenType)
        {
            string tokenContextKey = TokenContextKeys.GetValueOrDefault(tokenType)!;

            string? accessTokenValue = null;

            try
            {
                accessTokenValue = httpContext.Request.Cookies.Single(x => x.Key == tokenContextKey).Value;
            }
            catch
            {
                throw new NullOrEmptyTokenException(tokenContextKey, null, 3);
            }

            if (accessTokenValue == string.Empty)
            {
                throw new NullOrEmptyTokenException(tokenContextKey, null, 4);
            }

            return accessTokenValue;
        }

        public static Guid TakeUserIdFromAccessToken(this HttpContext httpContext)
        {
            if (httpContext.User is null)
            {
                throw new NullValueServerException(21, nameof(HttpContext), "User field in httpContext of accessToken was not found ");
            }

            Guid userId = Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);

            return userId;
        }

        public static void AppendTokensContextResponse(this HttpContext httpContext, UserPrepareTokensContextResponse userPrepareTokensContextResponse,
            JwtOptions jwtOptions)
        {
            httpContext.Response.Cookies.Append(TokenContextKeys.GetValueOrDefault(TokenEnum.ACCESS)!,
                userPrepareTokensContextResponse.AccessTokenValue, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = userPrepareTokensContextResponse.RefreshTokenExpiredTimestamp
                });

            httpContext.Response.Cookies.Append(TokenContextKeys.GetValueOrDefault(TokenEnum.REFRESH)!,
                userPrepareTokensContextResponse.RefreshTokenValue, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = userPrepareTokensContextResponse.RefreshTokenExpiredTimestamp
                });
        }

        public static void DeleteTokensContextResponse(this HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(TokenContextKeys.GetValueOrDefault(TokenEnum.ACCESS)!);
            httpContext.Response.Cookies.Delete(TokenContextKeys.GetValueOrDefault(TokenEnum.REFRESH)!);
        }
    }
}
