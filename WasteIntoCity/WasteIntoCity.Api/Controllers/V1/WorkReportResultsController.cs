using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Api.Controllers.V1
{
    public class WorkReportResultsController : ControllerBase
    {
        private readonly IWorkReportResultsService _workReportResultsService;

        public WorkReportResultsController(IWorkReportResultsService workReportResultsService)
        {
            _workReportResultsService = workReportResultsService;
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.User)}")]
        [HttpPost(ApiRoutes.WorkReportResults.CREATE)]
        public async Task<IActionResult> CreateAsync([FromBody] WorkReportCreateRequest workReportResultCreateRequest)
        {
            Guid fromParticipantId = HttpContext.TakeUserIdFromAccessToken();

            await _workReportResultsService.CreateAsync(fromParticipantId, workReportResultCreateRequest.Title,
                workReportResultCreateRequest.Description, workReportResultCreateRequest.WorkComplexityTypesId,
                workReportResultCreateRequest.WorkStatusTypesId, workReportResultCreateRequest.WorksId);

            return Created();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.WorkReportResults.GET)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            (WorkReportResult workReportResult, Work work) = await _workReportResultsService.GetAsync(id);

            if (workReportResult.FromParticipant == null)
            {
                throw new NullValueServerException(26, "from participant", null);
            }

            WorkReportResultGetResponse workReportResultGetResponse = new WorkReportResultGetResponse
            {
                FromParticipantId = workReportResult.FromParticipantId,
                FromParticipantEmail = workReportResult.FromParticipant.Email.Value,
                FromParticipantNickname = workReportResult.FromParticipant.Nickname.Value,
                Title = workReportResult.Title.Value,
                Description = workReportResult.Description.Value,
                WorkComplexityTypesId = (int)workReportResult.WorkComplexityTypesId,
                WorkStatusTypesId = (int)EnumOperationsExtension.TakeWorkStatusForClientEnum(work.StartedDatetime,
                    work.FinishDatetime, work.WorkStatusTypesId)
            };

            return Ok(workReportResultGetResponse);
        }
    }
}
