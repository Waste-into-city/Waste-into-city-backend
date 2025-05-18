using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Enum;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
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

            IdentityGetUserInfoResponse getUserInfoResponse = new IdentityGetUserInfoResponse
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

            if (user.Roles == null)
            {
                throw new NullValueServerException(46, "roles", null);
            }

            Role highRole = user.Roles.OrderByDescending(r => r.Id).FirstOrDefault()
                ?? throw new NullValueServerException(47, "roles", null); //TODO: Add new type exception

            IdentityGetSelfUserInfoResponse getSelfUserInfoResponse = new IdentityGetSelfUserInfoResponse
            {
                Id = user.Id,
                Nickname = user.Nickname.Value,
                Email = user.Email.Value,
                AvatarImageName = user.AvatarImageName != null ? user.AvatarImageName.Value : null,
                HighRoleName = highRole.Name
            };

            return Ok(getSelfUserInfoResponse);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)}")]
        [HttpGet(ApiRoutes.Identity.GET_USER_INFO_FOR_ADMIN)]
        public async Task<IActionResult> GetUserInfoForAdmin(Guid userId)
        {
            User user = await _identityService.GetUserInfoForAdmin(userId);

            IdentityGetUserInfoForAdminResponse getUserInfoForAdminResponse = new IdentityGetUserInfoForAdminResponse
            {
                Id = user.Id,
                Nickname = user.Nickname.Value,
                Email = user.Email.Value,
                AvatarImageName = user.AvatarImageName != null ? user.AvatarImageName.Value : null
            };

            return Ok(getUserInfoForAdminResponse);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Identity.GET_LEADERBOARD_PAGE_BY_BEST_RANKING)]
        public async Task<IActionResult> GetLeaderboardBySkipItems([FromQuery] int skipItems, [FromQuery] int size)
        {
            (List<User> users, int total) = await _identityService.GetLeaderboardBySkipItemsAndSize(skipItems, size);

            List<IdentityGetLeaderboardByPageResponse> items = users.Select(u => new IdentityGetLeaderboardByPageResponse
            {
                Nickname = u.Nickname.Value,
                Email = u.Email.Value,
                Ranking = u.Ranking
            }).ToList();

            BySkipItemsResponse<IdentityGetLeaderboardByPageResponse> byPageResponse = new BySkipItemsResponse<IdentityGetLeaderboardByPageResponse>
            {
                SkippedItems = skipItems + size,
                Size = size,
                Items = items,
                Total = total
            };

            return Ok(byPageResponse);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.User)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPut(ApiRoutes.Identity.UPDATE_OWN_USER_INFO)]
        public async Task<IActionResult> UpdateOwnUserInfoAsync([FromBody] UserUpdateOwnUserInfo request)
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            await _identityService.UpdateOwnUserInfoAsync(userId, request.Email, request.Password,
                request.NewPassword, request.Nickname, request.AvatarImageName);

            return Ok();
        }
    }
}
