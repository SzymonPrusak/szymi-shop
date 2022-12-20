using Microsoft.EntityFrameworkCore;
using SzymiShop.WebApi.Persistence;

namespace SzymiShop.WebApi.Util.Startup
{
    public static class DatabaseContextStartup
    {
        public static void AddPostgresDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopDbContext>(opts =>
            {
                string? cStr = configuration.GetConnectionString("Postgres");
                if (cStr == null)
                    throw new Exception();
                opts.UseNpgsql(cStr);
            });
        }
    }
}
