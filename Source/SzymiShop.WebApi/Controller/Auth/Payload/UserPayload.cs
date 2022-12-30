namespace SzymiShop.WebApi.Controller.Auth.Payload
{
    public class UserPayload
    {
        public required Guid Id { get; set; }
        public required string Login { get; set; }
    }
}
