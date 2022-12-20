using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using SzymiShop.WebApi.Util.Auth;

namespace SzymiShop.WebApi.Service.Crypto
{
    public class SignatureService : ISignatureService
    {
        private readonly IOptions<JwtConfig> _jwtConfig;

        public SignatureService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }


        public string Sign(string text)
        {
            using (var hmac = new HMACSHA512(_jwtConfig.Value.Key.Key))
            {
                var textBytes = Encoding.UTF8.GetBytes(text);
                var hashBytes = hmac.ComputeHash(textBytes);
                string hash = Convert.ToBase64String(hashBytes);
                return hash;
            }
        }
    }
}
