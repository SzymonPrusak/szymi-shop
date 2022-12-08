using Microsoft.AspNetCore.Mvc;
using SzymiShop.WebApi.Business.Model.User;
using SzymiShop.WebApi.Persistence.User;

namespace SzymiShop.WebApi.Controller.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterPayload req, CancellationToken token)
        {
            var exUser = await _userService.FindByLogin(req.Login, token);
            if (exUser != null)
                return Conflict();

            var user = new User(req.Login, new HashedPassword(req.Password));
            await _userService.Create(user);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginPayload req, CancellationToken token)
        {
            var user = await _userService.FindByLogin(req.Login, token);
            if (user == null)
                return Conflict();

            if (!user.Password.CheckPassword(req.Password))
                return Conflict();

            return Ok();
        }
    }
}
