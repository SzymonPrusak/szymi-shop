using Microsoft.AspNetCore.Authentication.JwtBearer;
using SzymiShop.WebApi.Util.Auth;

namespace SzymiShop.WebApi.Util.Startup
{
    public static class JwtAuthenticationStartup
    {
        public static void AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));

            services.AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer();

            services.ConfigureOptions<ConfigureJwtBearerOptions>();
        }
    }
}
