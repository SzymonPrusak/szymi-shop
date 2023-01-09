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
            return CreateOverviewModel(products);
        }

        public async Task<IEnumerable<ProductOverview>> FindProductOverviewsByUser(Guid userId, CancellationToken token = default)
        {
            var products = await _dbContext.Products
                .IgnoreAutoIncludes()
                .Include(p => p.Images.Where(i => i.Order == -1))
                .Include(p => p.Seller)
                .Where(p => p.SellerId == userId)
                .ToListAsync(token);
            return CreateOverviewModel(products);
        }

        private IEnumerable<ProductOverview> CreateOverviewModel(IEnumerable<Product> products)
        {
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
                .IgnoreAutoIncludes()
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


        public async Task CreateUpdate(ProductDetails product)
        {
            if (!product.Images.Any(i => i.Order == -1))
                throw new ArgumentException("product does not contain icon image");

            var ent = await _dbContext.Products
                .Where(p => p.Id == product.Id)
                .Include(p => p.Images)
                .FirstOrDefaultAsync();

            if (ent == null)
                AddNewProduct(product);
            else
                UpdateProduct(product, ent);
            await _dbContext.SaveChangesAsync();
        }

        private void AddNewProduct(ProductDetails product)
        {
            var ent = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                SellerId = product.Seller.Id,
                Seller = null!,
                Price = product.Price.Value,
                Description = product.Description,
                Images = AddNewImages(product)
            };
            _dbContext.Products.Add(ent);
        }

        private void UpdateProduct(ProductDetails product, Product ent)
        {
            ent.Name = product.Name;
            ent.SellerId = product.Seller.Id;
            ent.Price = product.Price.Value;
            ent.Description = product.Description;

            var oldImages = ent.Images;
            var removedImages = new List<ProductImage>();

            ent.Images = AddNewImages(product);
            foreach (var oldImage in oldImages)
            {
                var image = product.Images
                    .FirstOrDefault(i => i.Content == null && i.Id == oldImage.ImageId);
                if (image == null)
                {
                    removedImages.Add(oldImage);
                    continue;
                }

                oldImage.Order = image.Order;
                ent.Images.Add(oldImage);
            }

            _dbContext.Images.RemoveRange(removedImages.Select(i => i.Image!));
        }

        private IList<ProductImage> AddNewImages(ProductDetails product)
        {
            var images = product.Images
                .Where(i => i.Content != null)
                .Select(i =>
                {
                    var img = new Image.Image()
                    {
                        Id = Guid.NewGuid(),
                        Content = i.Content!
                    };
                    return new ProductImage()
                    {
                        Order = i.Order,
                        Image = img,
                        ImageId = img.Id,
                        ProductId = product.Id
                    };
                })
                .ToList();
            _dbContext.Images.AddRange(images.Select(i => i.Image!));

            return images;
        }
    }
}
