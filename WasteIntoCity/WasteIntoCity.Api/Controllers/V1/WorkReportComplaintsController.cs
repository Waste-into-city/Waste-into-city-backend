using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.AuthorizationPolicies;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
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
        [HttpPost(ApiRoutes.WorkReportComplaints.CREATE)]
        public async Task<IActionResult> CreateAsync([FromBody] WorkReportComplaintCreateRequest workReportComplaintCreateRequest)
        {
            Guid fromUsersId = HttpContext.TakeUserIdFromAccessToken();

            await _workReportComplaintsService.CreateAsync(workReportComplaintCreateRequest.Title, workReportComplaintCreateRequest.Description,
                workReportComplaintCreateRequest.WorksId, fromUsersId, workReportComplaintCreateRequest.ImageNames);

            return Created();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.WorkReportComplaints.GET)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid workReportComplaintId)
        {
            WorkReportComplaint workReportComplaint = await _workReportComplaintsService.GetAsync(workReportComplaintId);

            return Ok(workReportComplaint);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.WorkReportComplaints.CONFIRM)]
        public async Task<IActionResult> ConfirmAsync([FromRoute] Guid workReportComplaintId)
        {
            await _workReportComplaintsService.ConfirmAsync(workReportComplaintId);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.WorkReportComplaints.REJECT)]
        public async Task<IActionResult> RejectAsync([FromRoute] Guid workReportComplaintId)
        {
            await _workReportComplaintsService.RejectAsync(workReportComplaintId);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpGet(ApiRoutes.WorkReportComplaints.GET_FROM_QUEUE)]
        public async Task<IActionResult> GetFromQueueAsync()
        {
            (WorkReportComplaint workReportComplaint, Guid workReportResultsId) = await _workReportComplaintsService.GetFromQueueAsync();

            if (workReportComplaint.FromUser == null)
            {
                throw new NullValueServerException(40, "from user", null);
            }

            if (workReportComplaint.ImageNames == null)
            {
                throw new NullValueServerException(41, "image names", null);
            }

            WorkReportComplaintGetFromQueueResponse workReportComplaintGetFromQueueResponse = new WorkReportComplaintGetFromQueueResponse
            {
                Id = workReportComplaint.Id,
                Title = workReportComplaint.Title.Value,
                Description = workReportComplaint.Description.Value,
                FromUserEmail = workReportComplaint.FromUser.Email.Value,
                FromUserNickname = workReportComplaint.FromUser.Nickname.Value,
                WorksId = workReportComplaint.WorksId,
                StartedDatime = workReportComplaint.StartedDatime,
                ImageNames = workReportComplaint.ImageNames.Select(i => i.Value).ToList(),
                WorkReportResultsId = workReportResultsId
            };

            return Ok(workReportComplaintGetFromQueueResponse);
        }
    }
}
