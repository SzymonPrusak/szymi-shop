using System.ComponentModel.DataAnnotations;

namespace SzymiShop.WebApi.Persistence.User
{
    internal class User : Entity
    {
        [Required]
        public required string Login { get; set; }
        [Required]
        public required string PasswordHash { get; set; }
        [Required]
        public required string PasswordSalt { get; set; }
    }
}
