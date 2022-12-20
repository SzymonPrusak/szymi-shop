using SzymiShop.WebApi.Controller.Auth.Payload;

namespace SzymiShop.WebApi.Controller.Auth.Response
{
    public class AuthResponse
    {
        public required string AccessToken { get; set; }
        public required RefreshTokenPayload RefreshToken { get; set; }
    }
}
