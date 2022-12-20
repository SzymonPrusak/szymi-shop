using Microsoft.EntityFrameworkCore;

namespace SzymiShop.WebApi.Persistence.User
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ShopDbContext _dbContext;

        public RefreshTokenService(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<RefreshToken?> Find(Guid id, CancellationToken token = default)
        {
            var ent = await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Id == id, token);
            return ent;
        }

        public Task Create(RefreshToken token)
        {
            _dbContext.RefreshTokens.Add(token);
            return _dbContext.SaveChangesAsync();
        }

        public Task Delete(RefreshToken token)
        {
            _dbContext.RefreshTokens.Remove(token);
            return _dbContext.SaveChangesAsync();
        }
    }
}
