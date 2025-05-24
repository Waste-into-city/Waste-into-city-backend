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
using static WasteIntoCity.Api.Contracts.V1.Responses.WorkGetByIdResponse;

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
            Work work = await _workService.GetByIdAsync(id);

            if (work.Coordinates is null)
            {
                throw new NullValueServerException(12, "coordinates", null);
            }

            if (work.Participants is null)
            {
                throw new NullValueServerException(30, "participants", null);
            }

            if (work.TrashTypesIds is null)
            {
                throw new NullValueServerException(31, "participants", null);
            }

            if (work.ImageNames is null)
            {
                throw new NullValueServerException(32, "participants", null);
            }

            if (work.WorkStatusForClient is null)
            {
                throw new NullValueServerException(48, "participants", null);
            }

            List<WorkGetByIdResponseParticipant> participants = work.Participants.Select(p => new WorkGetByIdResponseParticipant
            {
                Id = p.Id,
                Nickname = p.Nickname.Value,
                AvatarImageName = p.AvatarImageName != null ? p.AvatarImageName.Value : null
            }).ToList();

            List<int> trashTypesIds = work.TrashTypesIds.Select(t => (int)t).ToList();

            List<string> imageNames = work.ImageNames.Select(i => i.Value).ToList();

            WorkGetByIdResponse workGetByIdResponse = new WorkGetByIdResponse
            {
                Id = work.Id,
                Title = work.Title.Value,
                Description = work.Description.Value,
                Participants = participants,
                ImageNames = imageNames,
                TrashTypesIds = trashTypesIds,
                StartedDatetime = work.StartedDatetime,
                FinishDatetime = work.FinishDatetime,
                WorkComplexityTypesId = (int)work.WorkComplexityTypesId,
                WorkStatusTypesId = (int)work.WorkStatusForClient,
                Lat = work.Coordinates.Lat,
                Lng = work.Coordinates.Lng,
            };

            return Ok(workGetByIdResponse);
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Works.GET_ALL_LOOKUP)]
        public async Task<IActionResult> GetAllLookup()
        {
            List<Work> works = await _workService.GetAllLookup();

            List<WorksGetAllLookupResponse> items = works.Select(w =>
            {
                if (w.Coordinates == null)
                {
                    throw new NullValueServerException(34, "coordinates", null);
                }

                return new WorksGetAllLookupResponse
                {
                    Id = w.Id,
                    Lat = w.Coordinates.Lat,
                    Lng = w.Coordinates.Lng
                };
            }).ToList();

            return Ok(items);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.User)}")]
        [HttpGet(ApiRoutes.Works.GET_ALL_OWN_TAKE_PART_IN)]
        public async Task<IActionResult> GetAllOwnTakePartInAsync([FromQuery] int skipItems, [FromQuery] int size)
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            (List<Work> works, int total) = await _workService.GetAllOwnTakePartIn(userId, skipItems, size);

            List<WorkGetAllOwnTakePartInResponse> items = new List<WorkGetAllOwnTakePartInResponse>(
                works.Select(work =>
                {
                    if (work.Coordinates is null)
                    {
                        throw new NullValueServerException(13, nameof(work.Coordinates), null);
                    }

                    if (work.TrashTypesIds is null)
                    {
                        throw new NullValueServerException(47, "trash types ids", null);
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
                        TrashTypesIds = work.TrashTypesIds.Select(t => (int)t).ToList()
                    };
                }).ToList()
            );

            BySkipItemsResponse<WorkGetAllOwnTakePartInResponse> response = new BySkipItemsResponse<WorkGetAllOwnTakePartInResponse>
            {
                SkippedItems = skipItems + size,
                Size = size,
                Items = items,
                Total = total
            };

            return Ok(response);
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.User)}")]
        [HttpPut(ApiRoutes.Works.TAKE_PART_IN_FIRST)]
        public async Task<IActionResult> TakePartInFirstSelfAsync([FromRoute] Guid id, [FromBody] WorkTakePartInFirstSelfRequest workTakePartInFirstSelfRequest)
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            await _workService.TakePartInFirstSelfAsync(id, userId, workTakePartInFirstSelfRequest.StartedDatetime);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.User)}")]
        [HttpGet(ApiRoutes.Works.TAKE_PART_IN)]
        public async Task<IActionResult> TakePartInSelfAsync([FromRoute] Guid id)
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            await _workService.TakePartInSelfAsync(id, userId);

            return Ok();
        }

        [Authorize(Roles = $"{nameof(RoleEnum.Admin)},{nameof(RoleEnum.Moderator)},{nameof(RoleEnum.User)}")]
        [HttpGet(ApiRoutes.Works.LEAVE_FROM_PARTICIPATION)]
        public async Task<IActionResult> LeaveFromParticipationSelfAsync([FromRoute] Guid id)
        {
            Guid userId = HttpContext.TakeUserIdFromAccessToken();

            await _workService.LeaveFromParticipationSelfAsync(id, userId);

            return Ok();
        }
    }
}
