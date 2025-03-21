using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WasteIntoCity.Api.AuthorizationPolicies.Requirements;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;

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

            if (!roles.Contains(nameof(RoleType.Moderator)) && !roles.Contains(nameof(RoleType.SuperAdmin)) && roles.Contains(nameof(RoleType.User)))
            {
                User user = await _usersRepository.FindByIdWithRolesAsync(context.User.Claims.Single(x => x.Type == "id").Value);

                if (user.Ranking < requirement.RankingMin)
                {
                    context.Fail(new AuthorizationFailureReason(this, UnhonestUserAccessDeniedException.DEFAULT_MESSAGE));
                    return;
                }
            }

            context.Succeed(requirement);
            return;
        }
    }
}
