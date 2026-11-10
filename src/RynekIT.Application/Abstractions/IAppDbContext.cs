using Microsoft.EntityFrameworkCore;
using RynekIT.Domain.Offers;
using RynekIT.Domain.Skills;
using RynekIT.Domain.Users;

namespace RynekIT.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<Company> Companies { get; }
    DbSet<Offer> Offers { get; }
    DbSet<Skill> Skills { get; }
    DbSet<OfferSkill> OfferSkills { get; }
    DbSet<FavoriteOffer> FavoriteOffers { get; }
    DbSet<Cv> Cvs { get; }
    DbSet<CvSkill> CvSkills { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
