using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RynekIT.Infrastructure.Persistence.Staging;

namespace RynekIT.Infrastructure.Persistence.Configurations;

internal sealed class RawOfferConfiguration : IEntityTypeConfiguration<RawOffer>
{
    public void Configure(EntityTypeBuilder<RawOffer> builder)
    {
        builder.ToTable("RawOffers", "stg");

        builder.Property(x => x.Source).HasMaxLength(50);
        builder.Property(x => x.SourceOfferId).HasMaxLength(100);
        builder.Property(x => x.SourceUrl).HasMaxLength(500);
        builder.Property(x => x.FetchedAt).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(x => new { x.Source, x.SourceOfferId }).IsUnique();
        builder.HasIndex(x => x.ProcessedAt);
    }
}
