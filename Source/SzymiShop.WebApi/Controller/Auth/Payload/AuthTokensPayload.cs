namespace SzymiShop.WebApi.Controller.Auth.Payload
{
    public class AuthTokensPayload
    {
        public required string AccessToken { get; set; }
        public required RefreshTokenPayload RefreshToken { get; set; }
    }
}
