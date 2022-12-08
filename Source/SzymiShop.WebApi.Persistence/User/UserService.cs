using Microsoft.EntityFrameworkCore;

namespace SzymiShop.WebApi.Persistence.User
{
    internal class UserService : IUserService
    {
        private readonly ShopDbContext _dbContext;

        public UserService(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Business.Model.User.User?> FindByLogin(string login, CancellationToken token = default)
        {
            var ent = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Login == login, token);
            if (ent == null)
                return null;

            return new Business.Model.User.User(ent);
        }

        public async Task Create(Business.Model.User.User user)
        {
            var ent = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            if (ent != null)
                throw new InvalidOperationException("user already exists");

            ent = new User()
            {
                Id = user.Id,
                Login = user.Login,
                PasswordHash = user.Password.PasswordHashB64,
                PasswordSalt = user.Password.Salt
            };
            _dbContext.Users.Add(ent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Business.Model.User.User user)
        {
            var ent = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            if (ent == null)
                throw new InvalidOperationException("user does not exist");

            ent.Login = user.Login;
            ent.PasswordHash = user.Password.PasswordHashB64;
            ent.PasswordSalt = user.Password.Salt;
            await _dbContext.SaveChangesAsync();
        }
    }
}
