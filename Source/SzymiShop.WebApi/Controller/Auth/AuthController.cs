using Microsoft.AspNetCore.Mvc;
using SzymiShop.WebApi.Business.Model.User;
using SzymiShop.WebApi.Controller.Auth.Payload;
using SzymiShop.WebApi.Controller.Auth.Request;
using SzymiShop.WebApi.Controller.Auth.Response;
using SzymiShop.WebApi.Persistence.User;
using SzymiShop.WebApi.Service.User;

namespace SzymiShop.WebApi.Controller.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly Service.User.IRefreshTokenService _refreshTokenService;

        public AuthController(IUserService userService, IAccessTokenService accessTokenService,
            Service.User.IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }


        [HttpPost("Register")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req, CancellationToken token)
        {
            var exUser = await _userService.FindByLogin(req.Login, token);
            if (exUser != null)
                return Conflict();

            var user = new User(req.Login, new HashedPassword(req.Password));
            await _userService.Create(user);

            return Ok(await GenerateAuthResponse(user));
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Login([FromBody] LoginRequest req, CancellationToken token)
        {
            var user = await _userService.FindByLogin(req.Login, token);
            if (user == null)
                return Conflict();

            if (!user.Password.CheckPassword(req.Password))
                return Conflict();

            return Ok(await GenerateAuthResponse(user));
        }

        [HttpPost("Refresh")]
        [ProducesResponseType(typeof(AuthTokensPayload), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Refresh([FromBody] AuthTokensPayload tokens, CancellationToken token)
        {
            var userIdN = _accessTokenService.Validate(tokens.AccessToken);
            if (!userIdN.HasValue)
                return Unauthorized();
            var userId = userIdN.Value;

            var tokenEnt = await _refreshTokenService.Find(tokens.RefreshToken.Id, token);

            if (tokenEnt == null || tokenEnt.UserId != userId)
                return Conflict();
            if (!_refreshTokenService.Verify(tokenEnt, tokens.RefreshToken.Signature))
                return Conflict();

            await _refreshTokenService.Delete(tokenEnt);

            return Ok(await GenerateTokens(userId));
        }


        private async Task<AuthResponse> GenerateAuthResponse(User user)
        {
            return new AuthResponse
            {
                User = new UserPayload
                {
                    Id = user.Id,
                    Login = user.Login
                },
                AuthTokens = await GenerateTokens(user.Id)
            };
        }

        private async Task<AuthTokensPayload> GenerateTokens(Guid userId)
        {
            return new AuthTokensPayload
            {
                AccessToken = _accessTokenService.Generate(userId),
                RefreshToken = await GenerateRefreshToken(userId)
            };
        }

        private async Task<RefreshTokenPayload> GenerateRefreshToken(Guid userId)
        {
            var token = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };
            await _refreshTokenService.Create(token);

            return new RefreshTokenPayload
            {
                Id = token.Id,
                Signature = _refreshTokenService.Sign(token)
            };
        }
    }
}
