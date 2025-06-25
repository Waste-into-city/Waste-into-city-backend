using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class WorkColleagueReportsController : ControllerBase
    {
        private readonly IWorkColleagueReportsService _workColleagueReportsService;

        public WorkColleagueReportsController(IWorkColleagueReportsService workColleagueReportsService)
        {
            _workColleagueReportsService = workColleagueReportsService;
        }

        [Authorize(Roles = $"{nameof(RoleEnum.User)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpPost(ApiRoutes.WorkColleagueReports.CREATE_MARKS)]
        public async Task<IActionResult> CreateMarksAsync([FromBody] WorkColleagueReportCreateMarksRequest workColleagueReportCreateMarksRequests)
        {
            Guid fromParticipantId = HttpContext.TakeUserIdFromAccessToken();

            await _workColleagueReportsService.CreateMarksAsync(workColleagueReportCreateMarksRequests.WorksId,
                fromParticipantId, workColleagueReportCreateMarksRequests.MarkColleaguePairStructs);

            return Created();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.User)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.Admin)}")]
        [HttpGet(ApiRoutes.WorkColleagueReports.GET_ALL_BY_WORKS_ID)]
        public async Task<IActionResult> GetAllByWorksIdAsync([FromRoute] Guid worksId)
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            List<WorkColleagueReport> workColleagueReports = await _workColleagueReportsService.GetAllByWorksIdAndUserIdAsync(
                userId, worksId);


            List<WorkColleagueReportGetAllByWorksIdResponse> response = workColleagueReports.Select(
                r =>
                {
                    if (r.AboutColleague == null)
                    {
                        throw new NullValueServerException(45, "about colleague", null);
                    }

                    return new WorkColleagueReportGetAllByWorksIdResponse
                    {
                        AboutColleagueNickname = r.AboutColleague.Nickname.Value,
                        AboutColleagueEmail = r.AboutColleague.Email.Value,
                        WorkMarkTypesId = (int)r.WorkMarkTypesId,
                        AvatarImageName = r.AboutColleague.AvatarImageName == null ? null
                            : r.AboutColleague.AvatarImageName.Value
                    };
                }
            ).ToList();

            return Ok(response);
        }
    }
}
