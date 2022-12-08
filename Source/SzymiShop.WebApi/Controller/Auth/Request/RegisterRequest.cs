using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SzymiShop.WebApi.Business.Model.User;

namespace SzymiShop.WebApi.Controller.Auth.Request
{
    public class RegisterRequest
    {
        [MinLength(User.MinLoginLength)]
        [MaxLength(User.MaxLoginLength)]
        public required string Login { get; set; }

        [MinLength(User.MinPasswordLength)]
        [MaxLength(User.MaxPasswordLength)]
        public required string Password { get; set; }
    }
}
