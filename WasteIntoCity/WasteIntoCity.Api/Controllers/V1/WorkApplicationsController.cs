using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.AuthorizationPolicies;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Core.Interfaces.Services;

namespace WasteIntoCity.Api.Controllers.V1
{
    public class WorkApplicationsController : ControllerBase
    {
        public IWorkApplicationService _workApplicationService;

        public WorkApplicationsController(IWorkApplicationService workApplicationService)
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
    }
}
