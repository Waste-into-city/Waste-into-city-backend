using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
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
        public async Task<IActionResult> Create([FromBody] WorkReportCreateRequest workReportResultCreate)
        {
            Guid fromParticipantId = HttpContext.TakeUserIdFromAccessToken();

            await _workReportResultsService.CreateAsync(fromParticipantId, workReportResultCreate.Title, workReportResultCreate.Description,
                workReportResultCreate.WorkComplexityTypesId, workReportResultCreate.WorkStatusTypesId);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.WorkReportResults.GET)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            WorkReportResult workReportResult = await _workReportResultsService.GetAsync(id);

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
                Description = workReportResult.Title.Value,
                WorkComplexityTypesId = (int)workReportResult.WorkComplexityTypesId,
                WorkStatusTypesId = (int)workReportResult.WorkStatusTypesId,
            };

            return Ok(workReportResultGetResponse);
        }
    }
}
