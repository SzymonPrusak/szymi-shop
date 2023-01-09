using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SzymiShop.WebApi.Controller.Auth.Request
{
    public class LoginRequest
    {
        [Required]
        public required string Login { get; set; }

        [Required(AllowEmptyStrings = true)]
        public required string Password { get; set; }
    }
}
