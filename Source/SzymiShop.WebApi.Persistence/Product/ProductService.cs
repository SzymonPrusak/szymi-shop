using Microsoft.EntityFrameworkCore;
using SzymiShop.WebApi.Business.Model.Product;

namespace SzymiShop.WebApi.Persistence.Product
{
    internal class ProductService : IProductService
    {
        private readonly ShopDbContext _dbContext;

        public ProductService(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<ProductOverview>> ReadProductOverviews(CancellationToken token = default)
        {
            var products = await _dbContext.Products
                .IgnoreAutoIncludes()
                .Include(p => p.Images.Where(i => i.Order == -1))
                .Include(p => p.Seller)
                .ToListAsync(token);

            var res = new List<ProductOverview>();
            foreach (var product in products)
            {
                var iconEnt = product.Images.FirstOrDefault();
                if (iconEnt == null)
                    continue;
                var icon = new Business.Model.Image.Image(iconEnt);
                var overview = new ProductOverview(product, icon);
                res.Add(overview);
            }

            return res;
        }

        public async Task<ProductDetails?> FindProductDetails(Guid id, CancellationToken token = default)
        {
            var ent = await _dbContext.Products
                .Where(p => p.Id == id)
                .Include(p => p.Images.Where(i => i.Order > 0))
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(token);
            if (ent == null)
                return null;

            var images = ent.Images
                .Select(i => new Business.Model.Product.ProductImage(i))
                .ToList();
            return new ProductDetails(ent, ent.Description, images);
        }
    }
}
