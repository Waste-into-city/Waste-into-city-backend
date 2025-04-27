using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WasteIntoCity.Api.AuthorizationPolicies.Requirements;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.ForbiddenAccessResource403Exceptions;
using WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Api.AuthorizationPolicies.Handlers
{
    public class HonestUserHandler : AuthorizationHandler<HonestUserRequirement>
    {
        private readonly IUsersRepository _usersRepository;

        public HonestUserHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HonestUserRequirement requirement)
        {
            List<string> roles = context.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            if (roles.Count == 0)
            {
                context.Fail(new AuthorizationFailureReason(this, UnhonestUserAccessDeniedException.DEFAULT_MESSAGE));
                return;
            }

            if (!roles.Contains(nameof(RoleEnum.Moderator)) && !roles.Contains(nameof(RoleEnum.Admin)) && roles.Contains(nameof(RoleEnum.User)))
            {
                if (Guid.TryParse(context.User.Claims.Single(x => x.Type == "id").Value, out Guid parsedGuid))
                {
                    User user = await _usersRepository.FindByIdWithRolesAsync(parsedGuid);

                    if (user.Ranking < requirement.RankingMin)
                    {
                        context.Fail(new AuthorizationFailureReason(this, UnhonestUserAccessDeniedException.DEFAULT_MESSAGE));
                        return;
                    }
                }
                else
                {
                    context.Fail(new AuthorizationFailureReason(this, InvalidTokenException.MESSAGE_DEFAULT));
                    return;
                }
            }

            context.Succeed(requirement);
            return;
        }
    }
}
