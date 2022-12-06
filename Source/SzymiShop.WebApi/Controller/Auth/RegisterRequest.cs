using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SzymiShop.WebApi.Business.Model.User;

namespace SzymiShop.WebApi.Controller.Auth
{
    public class RegisterRequest
    {
        [JsonProperty("login")]
        [Required]
        [MinLength(UserEntity.MinLoginLength)]
        [MaxLength(UserEntity.MaxLoginLength)]
        public string Login { get; set; } = null!;

        [JsonProperty("password")]
        [Required]
        [MinLength(UserEntity.MinPasswordLength)]
        [MaxLength(UserEntity.MaxPasswordLength)]
        public string Password { get; set; } = null!;
    }
}
