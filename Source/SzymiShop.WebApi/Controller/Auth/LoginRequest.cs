using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SzymiShop.WebApi.Controller.Auth
{
    public class LoginRequest
    {
        [JsonProperty("login")]
        [Required]
        public string Login { get; set; } = null!;

        [JsonProperty("password")]
        [Required(AllowEmptyStrings = true)]
        public string Password { get; set; } = null!;
    }
}
