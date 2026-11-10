using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RynekIT.Domain.Users;
using RynekIT.Infrastructure.Identity;

namespace RynekIT.Infrastructure.Persistence.Configurations;

internal sealed class CvConfiguration : IEntityTypeConfiguration<Cv>
{
    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        builder.Property(x => x.FileName).HasMaxLength(260);
        builder.Property(x => x.UploadedAt).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
