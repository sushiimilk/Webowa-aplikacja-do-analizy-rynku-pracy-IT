using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RynekIT.Domain.Offers;

namespace RynekIT.Infrastructure.Persistence.Configurations;

internal sealed class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Offers_SalaryRange",
                "[SalaryMin] IS NULL OR [SalaryMax] IS NULL OR [SalaryMin] <= [SalaryMax]");
            t.HasCheckConstraint("CK_Offers_SalaryNormalizedRange",
                "[SalaryMinNormalized] IS NULL OR [SalaryMaxNormalized] IS NULL OR [SalaryMinNormalized] <= [SalaryMaxNormalized]");
        });

        builder.Property(x => x.Source).HasMaxLength(50);
        builder.Property(x => x.SourceOfferId).HasMaxLength(100);
        builder.Property(x => x.SourceUrl).HasMaxLength(500);
        builder.Property(x => x.Title).HasMaxLength(300);
        builder.Property(x => x.Location).HasMaxLength(200);

        builder.Property(x => x.WorkMode).HasConversion<string>().HasMaxLength(20);
        builder.Property(x => x.SalaryPeriod).HasConversion<string>().HasMaxLength(10);

        builder.Property(x => x.SalaryCurrency).HasMaxLength(3).IsFixedLength();
        builder.Property(x => x.SalaryMin).HasPrecision(12, 2);
        builder.Property(x => x.SalaryMax).HasPrecision(12, 2);
        builder.Property(x => x.SalaryMinNormalized).HasPrecision(12, 2);
        builder.Property(x => x.SalaryMaxNormalized).HasPrecision(12, 2);

        builder.Property(x => x.FetchedAt).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(x => new { x.Source, x.SourceOfferId }).IsUnique();

        builder.HasOne(x => x.Company)
            .WithMany(c => c.Offers)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
