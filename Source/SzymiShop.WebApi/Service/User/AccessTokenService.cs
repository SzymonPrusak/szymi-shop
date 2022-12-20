using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SzymiShop.WebApi.Util.Auth;

namespace SzymiShop.WebApi.Service.User
{
    public class AccessTokenService : IAccessTokenService
    {
        public const string IdClaimName = "Id";

        private readonly IOptionsMonitor<JwtConfig> _jwtConfig;

        public AccessTokenService(IOptionsMonitor<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }


        public string Generate(Guid userId)
        {
            var handler = new JwtSecurityTokenHandler();

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(IdClaimName, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = _jwtConfig.CurrentValue.SigningCredentials
            };

            var token = handler.CreateToken(tokenDesc);
            var tokenString = handler.WriteToken(token);

            return tokenString;
        }

        public Guid? Validate(string? token)
        {
            if (token == null)
                return null;

            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal cp;
            try
            {
                cp = handler.ValidateToken(token, _jwtConfig.CurrentValue.ValidationParameters, out _);
            }
            catch (SecurityTokenException)
            {
                return null;
            }

            var idClaim = cp.Claims.FirstOrDefault(c => c.Type == IdClaimName);
            if (idClaim == null)
                return null;

            return Guid.Parse(idClaim.Value);
        }
    }
}
