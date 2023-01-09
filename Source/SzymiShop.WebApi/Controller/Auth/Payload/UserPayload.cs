using System.ComponentModel.DataAnnotations;

namespace SzymiShop.WebApi.Controller.Auth.Payload
{
    public class UserPayload
    {
        public required Guid Id { get; set; }
        [Required]
        public required string Login { get; set; }
    }
}
