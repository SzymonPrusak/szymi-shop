namespace SzymiShop.WebApi.Controller.Auth.Payload
{
    public class RefreshTokenPayload
    {
        public required Guid Id { get; set; }
        public required string Signature { get; set; }
    }
}
