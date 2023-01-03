using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SzymiShop.WebApi.Util.Auth
{
    public class JwtConfig
    {
        private string _secret = null!;

        public required string Secret
        {
            get => _secret;
            set
            {
                _secret = value;
                Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(value));
                SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
                ValidationParameters = GenerateValidationParameters(true);
                ValidationParametersNoLifetime = GenerateValidationParameters(false);
            }
        }

        public SymmetricSecurityKey Key { get; private set; } = null!;
        public SigningCredentials SigningCredentials { get; private set; } = null!;
        public TokenValidationParameters ValidationParameters { get; private set; } = null!;
        public TokenValidationParameters ValidationParametersNoLifetime { get; private set; } = null!;


        private TokenValidationParameters GenerateValidationParameters(bool validateLifetime)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Key,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = validateLifetime,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
