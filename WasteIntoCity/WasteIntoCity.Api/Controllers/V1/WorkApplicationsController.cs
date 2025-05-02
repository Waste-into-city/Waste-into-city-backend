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
                workApplicationCreateRequest.Lat, workApplicationCreateRequest.Lng, workApplicationCreateRequest.TrashTypeIds,
                workApplicationCreateRequest.ImageNames, userId);

            return Created();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.WorkApplications.REJECT)]
        public async Task<IActionResult> RejectAsync([FromRoute] Guid workApplicationsId)
        {
            await _workApplicationService.RejectAsync(workApplicationsId);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.WorkApplications.CONFIRM)]
        public async Task<IActionResult> ConfirmAsync([FromRoute] Guid workApplicationsId)
        {
            await _workApplicationService.ConfirmAsync(workApplicationsId);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.WorkApplications.GET_FROM_QUEUE)]
        public async Task<IActionResult> GetFromQueueAsync()
        {
            WorkApplication workApplication = await _workApplicationService.GetFromQueueAsync();

            if (workApplication.FromUser == null)
            {
                throw new NullValueServerException(35, "from user", null);
            }

            if (workApplication.TrashTypesIds == null)
            {
                throw new NullValueServerException(36, "trash types ids", null);
            }

            if (workApplication.ImageNames == null)
            {
                throw new NullValueServerException(36, "trash types ids", null);
            }

            if (workApplication.Coordinates == null)
            {
                throw new NullValueServerException(37, "trash types ids", null);
            }

            WorkApplicationGetFromQueueResponse workApplicationGetFromQueueResponse = new WorkApplicationGetFromQueueResponse
            {
                Id = workApplication.Id,
                Title = workApplication.Title.Value,
                Description = workApplication.Description.Value,
                FromUserNickname = workApplication.FromUser.Nickname.Value,
                FromUserEmail = workApplication.FromUser.Email.Value,
                StartedDatetime = workApplication.StartedDatetime,
                TrashTypesIds = workApplication.TrashTypesIds.Select(t => (int)t).ToList(),
                ImageNames = workApplication.ImageNames.Select(t => t.Value).ToList(),
                Lat = workApplication.Coordinates.Lat,
                Lng = workApplication.Coordinates.Lng
            };

            return Ok();
        }
    }
}
