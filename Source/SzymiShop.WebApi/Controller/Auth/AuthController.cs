using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SzymiShop.WebApi.Business.Model.User;
using SzymiShop.WebApi.Controller.Auth.Payload;
using SzymiShop.WebApi.Controller.Auth.Request;
using SzymiShop.WebApi.Controller.Auth.Response;
using SzymiShop.WebApi.Persistence.User;
using SzymiShop.WebApi.Util.Auth;

namespace SzymiShop.WebApi.Controller.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptionsMonitor<JwtConfig> _jwtConfig;

        public AuthController(IUserService userService, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            _userService = userService;
            _jwtConfig = jwtConfig;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req, CancellationToken token)
        {
            var exUser = await _userService.FindByLogin(req.Login, token);
            if (exUser != null)
                return Conflict();

            var user = new User(req.Login, new HashedPassword(req.Password));
            await _userService.Create(user);

            return Ok(GenerateTokens(user));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req, CancellationToken token)
        {
            var user = await _userService.FindByLogin(req.Login, token);
            if (user == null)
                return Conflict();

            if (!user.Password.CheckPassword(req.Password))
                return Conflict();

            return Ok(GenerateTokens(user));
        }


        private AuthResponse GenerateTokens(User user)
        {
            return new AuthResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = null!
            };
        }

        private string GenerateAccessToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = _jwtConfig.CurrentValue.SigningCredentials
            };

            var token = handler.CreateToken(tokenDesc);
            var tokenString = handler.WriteToken(token);

            return tokenString;
        }

        private RefreshTokenPayload GenerateRefreshToken(User user)
        {
            return null!;
        }
    }
}
