using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.AuthorizationPolicies;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Api.Controllers.V1
{
    public class WorkReportComplaintsController : ControllerBase
    {
        private readonly IWorkReportComplaintsService _workReportComplaintsService;

        public WorkReportComplaintsController(IWorkReportComplaintsService workReportComplaintsService)
        {
            _workReportComplaintsService = workReportComplaintsService;
        }

        [Authorize(Policy = PolicyType.HONEST_USER)]
        [HttpPost(ApiRoutes.WorkReportComplaint.CREATE)]
        public async Task<IActionResult> CreateAsync([FromBody] WorkReportComplaintCreateRequest workReportComplaintCreateRequest)
        {
            Guid fromUsersId = HttpContext.TakeUserIdFromAccessToken();

            await _workReportComplaintsService.CreateAsync(workReportComplaintCreateRequest.Title, workReportComplaintCreateRequest.Description,
                workReportComplaintCreateRequest.WorksId, fromUsersId);

            return Created();
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.WorkReportComplaint.GET)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid workReportComplaintId)
        {
            WorkReportComplaint workReportComplaint = await _workReportComplaintsService.GetAsync(workReportComplaintId);

            return Ok(workReportComplaint);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.WorkReportComplaint.CONFIRM)]
        public async Task<IActionResult> ConfirmAsync([FromRoute] Guid workReportComplaintId)
        {
            await _workReportComplaintsService.ConfirmAsync(workReportComplaintId);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.WorkReportComplaint.REJECT)]
        public async Task<IActionResult> RejectAsync([FromRoute] Guid workReportComplaintId)
        {
            await _workReportComplaintsService.RejectAsync(workReportComplaintId);

            return Ok();
        }
    }
}
