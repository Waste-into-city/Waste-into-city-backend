using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Application;
using WasteIntoCity.Application.Contracts.V1.Requests;
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

        [HttpPost(ApiRoutes.WorkApplications.CREATE)]
        public async Task<IActionResult> CreateAsync([FromBody] WorkApplicationCreateRequest workApplicationCreateRequest)
        {
            await _workApplicationService.CreateAsync(workApplicationCreateRequest.Id, workApplicationCreateRequest.Title,
                workApplicationCreateRequest.Description, workApplicationCreateRequest.StartedDatetime, workApplicationCreateRequest.WorkComplexityId,
                workApplicationCreateRequest.Lat, workApplicationCreateRequest.Lng);

            return Created();
        }
    }
}
