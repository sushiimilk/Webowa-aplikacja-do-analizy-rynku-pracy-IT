using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RynekIT.Application.Abstractions;
using RynekIT.Domain.Offers;
using RynekIT.Domain.Skills;
using RynekIT.Domain.Users;
using RynekIT.Infrastructure.Identity;
using RynekIT.Infrastructure.Persistence.Staging;

namespace RynekIT.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser>(options), IAppDbContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<SkillAlias> SkillAliases => Set<SkillAlias>();
    public DbSet<OfferSkill> OfferSkills => Set<OfferSkill>();
    public DbSet<FavoriteOffer> FavoriteOffers => Set<FavoriteOffer>();
    public DbSet<Cv> Cvs => Set<Cv>();
    public DbSet<CvSkill> CvSkills => Set<CvSkill>();
    public DbSet<RawOffer> RawOffers => Set<RawOffer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
