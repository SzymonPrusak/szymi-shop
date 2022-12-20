using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace SzymiShop.WebApi.Util.Auth
{
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IOptions<JwtConfig> _config;

        public ConfigureJwtBearerOptions(IOptions<JwtConfig> config)
        {
            _config = config;
        }


        public void Configure(JwtBearerOptions options)
        {
            options.SaveToken = true;
            options.TokenValidationParameters = _config.Value.ValidationParameters;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            Configure(options);
        }
    }
}
