using Microsoft.AspNetCore.Mvc;
using WasteIntoCity.Api.Contracts.V1.Requests;
using WasteIntoCity.Api.Contracts.V1.Responses;
using WasteIntoCity.Application.Contracts.V1.Requests;
using WasteIntoCity.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WasteIntoCity.Application.Controllers.V1
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.REGISTER)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            await _identityService.RegisterAsync(request.Nickname, request.Email, request.Password);

            return Ok();
        }

        [HttpPost(ApiRoutes.Identity.LOGIN)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            (string accessTokenValue, string refreshTokenValue) = await _identityService.LoginAsync(request.Email, request.Password);

            UserLoginResponse userLoginResponse = new UserLoginResponse
            {
                AccessTokenValue = accessTokenValue,
                RefreshTokenValue = refreshTokenValue
            };

            return Ok(userLoginResponse);
        }




        //// GET api/<AuthController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<AuthController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<AuthController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<AuthController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
