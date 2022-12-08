using Microsoft.EntityFrameworkCore;

namespace SzymiShop.WebApi.Persistence.Image
{
    internal class ImageService : IImageService
    {
        private readonly ShopDbContext _dbContext;

        public ImageService(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> LoadContent(Business.Model.Image.Image i, CancellationToken token = default)
        {
            var ent = await _dbContext.Images
                .FirstOrDefaultAsync(img => i.Id == img.Id, token);
            if (ent == null)
                return false;

            i.Content = ent.Content;
            return true;
        }
    }
}
