using System.ComponentModel.DataAnnotations;

namespace SzymiShop.WebApi.Controller.Auth.Payload
{
    public class RefreshTokenPayload
    {
        public required Guid Id { get; set; }
        [Required]
        public required string Signature { get; set; }
    }
}
