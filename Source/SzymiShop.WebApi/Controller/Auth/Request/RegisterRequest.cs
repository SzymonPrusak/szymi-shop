using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SzymiShop.WebApi.Business.Model.User;

namespace SzymiShop.WebApi.Controller.Auth.Request
{
    public class RegisterRequest
    {
        [Required]
        [MinLength(User.MinLoginLength)]
        [MaxLength(User.MaxLoginLength)]
        public required string Login { get; set; }

        [Required]
        [MinLength(User.MinPasswordLength)]
        [MaxLength(User.MaxPasswordLength)]
        public required string Password { get; set; }
    }
}
