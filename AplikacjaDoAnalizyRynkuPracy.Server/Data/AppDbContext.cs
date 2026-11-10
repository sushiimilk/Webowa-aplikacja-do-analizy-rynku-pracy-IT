using AplikacjaDoAnalizyRynkuPracy.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AplikacjaDoAnalizyRynkuPracy.Server.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<OfferSkill> OfferSkills => Set<OfferSkill>();
    public DbSet<FavoriteOffer> FavoriteOffers => Set<FavoriteOffer>();
    public DbSet<Cv> Cvs => Set<Cv>();
    public DbSet<CvSkill> CvSkills => Set<CvSkill>();
    public DbSet<RawOffer> RawOffers => Set<RawOffer>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Company>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(200);
            e.HasIndex(x => x.Name).IsUnique();
        });

        b.Entity<Offer>(e =>
        {
            e.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Offers_SalaryRange",
                    "[SalaryMin] IS NULL OR [SalaryMax] IS NULL OR [SalaryMin] <= [SalaryMax]");
                t.HasCheckConstraint("CK_Offers_SalaryNormalizedRange",
                    "[SalaryMinNormalized] IS NULL OR [SalaryMaxNormalized] IS NULL OR [SalaryMinNormalized] <= [SalaryMaxNormalized]");
            });

            e.Property(x => x.Source).HasMaxLength(50);
            e.Property(x => x.SourceOfferId).HasMaxLength(100);
            e.Property(x => x.SourceUrl).HasMaxLength(500);
            e.Property(x => x.Title).HasMaxLength(300);
            e.Property(x => x.Location).HasMaxLength(200);

            e.Property(x => x.WorkMode).HasConversion<string>().HasMaxLength(20);
            e.Property(x => x.SalaryPeriod).HasConversion<string>().HasMaxLength(10);

            e.Property(x => x.SalaryCurrency).HasMaxLength(3).IsFixedLength();
            e.Property(x => x.SalaryMin).HasPrecision(12, 2);
            e.Property(x => x.SalaryMax).HasPrecision(12, 2);
            e.Property(x => x.SalaryMinNormalized).HasPrecision(12, 2);
            e.Property(x => x.SalaryMaxNormalized).HasPrecision(12, 2);

            e.Property(x => x.FetchedAt).HasDefaultValueSql("SYSUTCDATETIME()");

            e.HasIndex(x => new { x.Source, x.SourceOfferId }).IsUnique();

            e.HasOne(x => x.Company)
             .WithMany(c => c.Offers)
             .HasForeignKey(x => x.CompanyId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<Skill>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
        });

        b.Entity<OfferSkill>().HasKey(x => new { x.OfferId, x.SkillId });
        b.Entity<CvSkill>().HasKey(x => new { x.CvId, x.SkillId });

        b.Entity<FavoriteOffer>(e =>
        {
            e.HasKey(x => new { x.UserId, x.OfferId });
            e.Property(x => x.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
        });

        b.Entity<Cv>(e =>
        {
            e.Property(x => x.FileName).HasMaxLength(260);
            e.Property(x => x.UploadedAt).HasDefaultValueSql("SYSUTCDATETIME()");
        });

        b.Entity<RawOffer>(e =>
        {
            e.ToTable("RawOffers", "stg");
            e.Property(x => x.Source).HasMaxLength(50);
            e.Property(x => x.SourceOfferId).HasMaxLength(100);
            e.Property(x => x.SourceUrl).HasMaxLength(500);
            e.Property(x => x.FetchedAt).HasDefaultValueSql("SYSUTCDATETIME()");
            e.HasIndex(x => new { x.Source, x.SourceOfferId }).IsUnique();
            e.HasIndex(x => x.ProcessedAt);
        });
    }
}
