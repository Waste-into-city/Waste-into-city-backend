using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Enum;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
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

        [Authorize(Roles = $"{nameof(RoleEnum.User)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.Identity.LOGOUT)]
        public async Task<IActionResult> Logout()
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            await _identityService.LogoutAsync(userId);

            HttpContext.DeleteTokensContextResponse();

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Identity.GET_USER_INFO)]
        public async Task<IActionResult> GetUserInfo(Guid userId)
        {
            User user = await _identityService.GetUserInfo(userId);

            GetUserInfoResponse getUserInfoResponse = new GetUserInfoResponse
            {
                Id = user.Id,
                Nickname = user.Nickname.Value,
                AvatarImageName = user.AvatarImageName != null ? user.AvatarImageName.Value : null
            };

            return Ok(getUserInfoResponse);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.User)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpGet(ApiRoutes.Identity.GET_SELF_USER_INFO)]
        public async Task<IActionResult> GetSelfUserInfo()
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            User user = await _identityService.GetSelfUserInfo(userId);

            GetSelfUserInfoResponse getSelfUserInfoResponse = new GetSelfUserInfoResponse
            {
                Id = user.Id,
                Nickname = user.Nickname.Value,
                Email = user.Email.Value,
                AvatarImageName = user.AvatarImageName != null ? user.AvatarImageName.Value : null
            };

            return Ok(getSelfUserInfoResponse);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)}")]
        [HttpGet(ApiRoutes.Identity.GET_USER_INFO_FOR_ADMIN)]
        public async Task<IActionResult> GetUserInfoForAdmin(Guid userId)
        {
            User user = await _identityService.GetUserInfo(userId);

            GetUserInfoForAdminResponse getUserInfoForAdminResponse = new GetUserInfoForAdminResponse
            {
                Id = user.Id,
                Nickname = user.Nickname.Value,
                Email = user.Email.Value,
                AvatarImageName = user.AvatarImageName != null ? user.AvatarImageName.Value : null
            };

            return Ok(getUserInfoForAdminResponse);
        }
    }
}
