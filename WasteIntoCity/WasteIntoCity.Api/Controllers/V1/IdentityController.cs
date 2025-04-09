using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Application.Enum;
using WasteIntoCity.Core.Enums;
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
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegistrationRequest request)
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
        public async Task<IActionResult> RefreshTokenAsync()
        {
            string accessTokenValue = HttpContext.TakeTokenValueByTokenType(TokenEnum.ACCESS);

            string refreshTokenValue = HttpContext.TakeTokenValueByTokenType(TokenEnum.REFRESH);

            UserPrepareTokensContextResponse userPrepareTokensContextResponse = await _identityService.RefreshAsync(accessTokenValue, refreshTokenValue);

            HttpContext.AppendTokensContextResponse(userPrepareTokensContextResponse, _jwtOptions);

            return Ok();
        }

        //[Authorize(Policy = PolicyType.HONEST_USER)]
        [Authorize(Roles = $"{nameof(RoleEnum.User)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.SuperAdmin)}")]
        [HttpPost(ApiRoutes.Identity.LOGOUT)]
        public async Task<IActionResult> Logout()
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            await _identityService.LogoutAsync(userId);

            HttpContext.DeleteTokensContextResponse();

            return Ok();
        }
    }
}
