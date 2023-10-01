using AnySocialNetwork.Requests;
using AnySocialNetwork.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnySocialNetwork.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> CreateAsync([FromBody]CreateUserRequest createUserRequest)
        {
            var result = await _userServices.CreateAsync(createUserRequest);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("getAll")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<dynamic>> GetAllAsync()
        {
            var result = await _userServices.GetAllAsync();
            if (!result.Any()) return NoContent();
            return result;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]LoginUserRequest loginUserRequest)
        {
            var result = await _userServices.Authenticate(loginUserRequest);
            if (result == null) return BadRequest();

            return result;
        }
    }
}