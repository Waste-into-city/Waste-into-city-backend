using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Application.Types;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Structs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WasteIntoCity.Application.Controllers.V1
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly JwtOptions _jwtOptions;

        public IdentityController(IIdentityService identityService, JwtOptions jwtOptions)
        {
            _identityService = identityService;
            _jwtOptions = jwtOptions;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.REGISTER)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            await _identityService.RegisterAsync(request.Nickname, request.Email, request.Password);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.LOGIN)]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            UserPrepareTokensContextResponse userPrepareTokensContextResponse = await _identityService.LoginAsync(request.Email, request.Password);

            HttpContext.AppendTokensContextResponse(userPrepareTokensContextResponse, _jwtOptions);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.REFRESH)]
        public async Task<IActionResult> RefreshToken()
        {
            string accessTokenValue = HttpContext.TakeTokenValueByTokenType(TokenType.ACCESS);

            string refreshTokenValue = HttpContext.TakeTokenValueByTokenType(TokenType.REFRESH);

            UserPrepareTokensContextResponse userPrepareTokensContextResponse = await _identityService.RefreshAsync(accessTokenValue, refreshTokenValue);

            HttpContext.AppendTokensContextResponse(userPrepareTokensContextResponse, _jwtOptions);

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Identity.LOGOUT)]
        public async Task<IActionResult> Logout()
        {
            string userId = HttpContext.TakeUserIdFromAccessToken();

            await _identityService.LogoutAsync(userId);

            HttpContext.DeleteTokensContextResponse();

            return Ok();
        }
    }
}
