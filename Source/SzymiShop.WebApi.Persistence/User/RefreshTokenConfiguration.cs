using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SzymiShop.WebApi.Persistence.User
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasOne(typeof(User))
                .WithMany()
                .HasForeignKey(nameof(RefreshToken.UserId));
        }
    }
}
