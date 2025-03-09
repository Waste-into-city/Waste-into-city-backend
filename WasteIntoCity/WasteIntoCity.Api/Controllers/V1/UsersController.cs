using Microsoft.AspNetCore.Mvc;

namespace WasteIntoCity.Application.Controllers.V1
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
