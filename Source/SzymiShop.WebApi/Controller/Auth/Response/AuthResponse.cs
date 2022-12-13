namespace SzymiShop.WebApi.Controller.Auth.Response
{
    public class AuthResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
