using BusinessLogicLayer.Contracts;
using Common.Models.Inbound;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControllerLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> User([FromForm] RegisterUser user)
        {
            return Ok(await _userService.CreateUser(user));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginUser user)
        {
            return Ok(await _userService.Login(user.Username, user.Password));
        }

        [HttpPost]
        [Route("facebooklogin")]
        public async Task<ActionResult> FacebookLogin(FacebookLoginUser user)
        {
            return Ok(await _userService.FacebookLogin(user));
        }
    }
}
