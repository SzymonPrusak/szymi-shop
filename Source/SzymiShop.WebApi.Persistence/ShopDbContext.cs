using Microsoft.EntityFrameworkCore;

namespace SzymiShop.WebApi.Persistence
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {

        }

        internal DbSet<User.User> Users => Set<User.User>();
    }
}
