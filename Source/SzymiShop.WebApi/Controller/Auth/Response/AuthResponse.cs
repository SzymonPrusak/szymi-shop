using SzymiShop.WebApi.Controller.Auth.Payload;

namespace SzymiShop.WebApi.Controller.Auth.Response
{
    public class AuthResponse
    {
        public required UserPayload User { get; set; }
        public required AuthTokensPayload AuthTokens { get; set; } 
    }
}
