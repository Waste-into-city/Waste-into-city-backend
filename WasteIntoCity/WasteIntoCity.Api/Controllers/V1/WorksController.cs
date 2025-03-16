using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Api.Controllers.V1
{
    public class WorksController : ControllerBase
    {
        private readonly IWorksService _workService;

        public WorksController(IWorksService workService)
        {
            _workService = workService;
        }

        [Authorize(Roles = $"{nameof(RoleType.Admin)},{nameof(RoleType.Moderator)}")]
        [HttpPost(ApiRoutes.Works.CREATE)]
        public async Task<IActionResult> CreateAsync([FromBody] WorkCreateRequest workCreateRequest)
        {
            await _workService.CreateAsync(workCreateRequest.Title, workCreateRequest.Description, workCreateRequest.StartedDateTime,
                workCreateRequest.FinishDatetime, workCreateRequest.WorkComplexityId, workCreateRequest.WorkStatusesId);

            return Created();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Works.GET_ALL)]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Work> works = await _workService.GetAll();

            WorkGetAllResponse workGetAllResponse = new WorkGetAllResponse
            {
                Works = works
            };

            return Ok(workGetAllResponse);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Works.GET_BY_ID)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            Work work = await _workService.GetById(id);

            WorkGetByIdResponse workGetByIdResponse = new WorkGetByIdResponse
            {
                Work = work
            };

            return Ok(workGetByIdResponse);
        }

        [Authorize(Roles = $"{nameof(RoleType.Admin)},{nameof(RoleType.Moderator)},{nameof(RoleType.User)}")]
        [HttpGet(ApiRoutes.Works.GET_ALL_OWN_TAKE_PART_IN)]
        public async Task<IActionResult> GetAllOwnTakePartInAsync()
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            List<Work> works = await _workService.GetAllOwnTakePartIn(userId);

            WorkGetAllOwnTakePartInResponse workGetAllOwnTakePartInResponse = new WorkGetAllOwnTakePartInResponse
            {
                Works = works
            };

            return Ok(workGetAllOwnTakePartInResponse);
        }

        [Authorize(Roles = $"{nameof(RoleType.Admin)},{nameof(RoleType.Moderator)}")]
        [HttpPut(ApiRoutes.Works.UPDATE)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] WorkUpdateRequest workUpdateRequest)
        {
            await _workService.UpdateAsync(id, workUpdateRequest.Title, workUpdateRequest.Description, workUpdateRequest.StartedDateTime,
                workUpdateRequest.FinishDatetime, workUpdateRequest.WorkComplexityId, workUpdateRequest.WorkStatusesId);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleType.Admin)},{nameof(RoleType.Moderator)},{nameof(RoleType.User)}")]
        [HttpPut(ApiRoutes.Works.UPDATE_WORK_STATUS)]
        public async Task<IActionResult> UpdateWorkStatusAsync([FromRoute] Guid id, [FromBody] WorkUpdateStatusRequest workUpdateStatusRequest)
        {
            await _workService.UpdateWorkStatusAsync(id, workUpdateStatusRequest.WorkStatusesId);

            return Ok();
        }
    }
}
