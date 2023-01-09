using System.ComponentModel.DataAnnotations;

namespace SzymiShop.WebApi.Controller.Auth.Payload
{
    public class AuthTokensPayload
    {
        [Required]
        public required string AccessToken { get; set; }
        public required RefreshTokenPayload RefreshToken { get; set; }
    }
}
