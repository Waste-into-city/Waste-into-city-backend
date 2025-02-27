using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1;

namespace WasteIntoCity.Api.Controllers.V1
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet(ApiRoutes.Users.GET)]
        public IActionResult Get(Guid userId)
        {
            return Ok();
        }
    }
}
