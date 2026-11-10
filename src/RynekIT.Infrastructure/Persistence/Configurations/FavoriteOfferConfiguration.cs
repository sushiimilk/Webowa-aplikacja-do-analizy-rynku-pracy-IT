using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RynekIT.Domain.Users;
using RynekIT.Infrastructure.Identity;

namespace RynekIT.Infrastructure.Persistence.Configurations;

internal sealed class FavoriteOfferConfiguration : IEntityTypeConfiguration<FavoriteOffer>
{
    public void Configure(EntityTypeBuilder<FavoriteOffer> builder)
    {
        builder.HasKey(x => new { x.UserId, x.OfferId });
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
