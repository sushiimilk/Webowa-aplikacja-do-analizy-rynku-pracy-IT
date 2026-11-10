using Microsoft.EntityFrameworkCore;
using RynekIT.Application.Abstractions;
using RynekIT.Domain.Offers;
using RynekIT.Domain.Skills;
using RynekIT.Domain.Users;

namespace RynekIT.Application.UnitTests;

internal sealed class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<OfferSkill> OfferSkills => Set<OfferSkill>();
    public DbSet<FavoriteOffer> FavoriteOffers => Set<FavoriteOffer>();
    public DbSet<Cv> Cvs => Set<Cv>();
    public DbSet<CvSkill> CvSkills => Set<CvSkill>();

    public static TestDbContext Create()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TestDbContext(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OfferSkill>().HasKey(x => new { x.OfferId, x.SkillId });
        modelBuilder.Entity<CvSkill>().HasKey(x => new { x.CvId, x.SkillId });
        modelBuilder.Entity<FavoriteOffer>().HasKey(x => new { x.UserId, x.OfferId });
    }
}
