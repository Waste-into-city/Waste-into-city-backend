using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Extensions;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
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

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Works.GET_ALL)]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Work> works = await _workService.GetAll();

            List<WorkGetAllResponse> workGetAllOwnTakePartInResponse = new List<WorkGetAllResponse>(
                works.Select(work =>
                {
                    if (work.Coordinates is null)
                    {
                        throw new NullValueServerException(11, nameof(work.Coordinates), null);
                    }

                    return new WorkGetAllResponse
                    {
                        Id = work.Id,
                        Title = work.Title.Value,
                        Description = work.Description.Value,
                        StartedDatetime = work.StartedDatetime,
                        FinishDatetime = work.FinishDatetime,
                        WorkComplexityTypesId = (int)work.WorkComplexityTypesId,
                        WorkStatusTypesId = (int)work.WorkStatusTypesId,
                        Lat = work.Coordinates.Lat,
                        Lng = work.Coordinates.Lng,
                    };
                }).ToList()
            );

            return Ok(workGetAllOwnTakePartInResponse);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Works.GET_BY_ID)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            Work work = await _workService.GetById(id);

            if (work.Coordinates is null)
            {
                throw new NullValueServerException(12, nameof(work.Coordinates), null);
            }

            WorkGetByIdResponse workGetByIdResponse = new WorkGetByIdResponse
            {
                Id = work.Id,
                Title = work.Title.Value,
                Description = work.Description.Value,
                StartedDatetime = work.StartedDatetime,
                FinishDatetime = work.FinishDatetime,
                WorkComplexityTypesId = (int)work.WorkComplexityTypesId,
                WorkStatusTypesId = (int)work.WorkStatusTypesId,
                Lat = work.Coordinates.Lat,
                Lng = work.Coordinates.Lng,
            };

            return Ok(workGetByIdResponse);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.SuperAdmin)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.User)}")]
        [HttpGet(ApiRoutes.Works.GET_ALL_OWN_TAKE_PART_IN)]
        public async Task<IActionResult> GetAllOwnTakePartInAsync()
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            List<Work> works = await _workService.GetAllOwnTakePartIn(userId);

            List<WorkGetAllOwnTakePartInResponse> workGetAllOwnTakePartInResponse = new List<WorkGetAllOwnTakePartInResponse>(
                works.Select(work =>
                {
                    if (work.Coordinates is null)
                    {
                        throw new NullValueServerException(13, nameof(work.Coordinates), null);
                    }

                    return new WorkGetAllOwnTakePartInResponse
                    {
                        Id = work.Id,
                        Title = work.Title.Value,
                        Description = work.Description.Value,
                        StartedDatetime = work.StartedDatetime,
                        FinishDatetime = work.FinishDatetime,
                        WorkComplexityTypesId = (int)work.WorkComplexityTypesId,
                        WorkStatusTypesId = (int)work.WorkStatusTypesId,
                        Lat = work.Coordinates.Lat,
                        Lng = work.Coordinates.Lng,
                    };
                }).ToList()
            );

            return Ok(workGetAllOwnTakePartInResponse);
        }
    }
}
