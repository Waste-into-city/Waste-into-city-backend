using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.AuthorizationPolicies;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Interfaces.Services;

namespace WasteIntoCity.Api.Controllers.V1
{
    public class WorkApplicationsController : ControllerBase
    {
        public IWorkApplicationsService _workApplicationService;

        public WorkApplicationsController(IWorkApplicationsService workApplicationService)
        {
            _workApplicationService = workApplicationService;
        }

        [Authorize(Policy = PolicyType.HONEST_USER)]
        [HttpPost(ApiRoutes.WorkApplications.CREATE)]
        public async Task<IActionResult> CreateAsync([FromBody] WorkApplicationCreateRequest workApplicationCreateRequest)
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            await _workApplicationService.CreateOwnAsync(workApplicationCreateRequest.Title,
                workApplicationCreateRequest.Description, workApplicationCreateRequest.WorkComplexityId,
                workApplicationCreateRequest.Lat, workApplicationCreateRequest.Lng, userId);

            return Created();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.SuperAdmin)}")]
        [HttpPost(ApiRoutes.WorkApplications.REJECT)]
        public async Task<IActionResult> RejectAsync([FromRoute] Guid workApplicationsId)
        {
            await _workApplicationService.RejectAsync(workApplicationsId);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.SuperAdmin)}")]
        [HttpPost(ApiRoutes.WorkApplications.CONFIRM)]
        public async Task<IActionResult> ConfirmAsync([FromRoute] Guid workApplicationsId)
        {
            await _workApplicationService.ConfirmAsync(workApplicationsId);

            return Ok();
        }
    }
}
