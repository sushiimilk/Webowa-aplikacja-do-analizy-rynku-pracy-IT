using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RynekIT.Domain.Offers;

namespace RynekIT.Infrastructure.Persistence.Configurations;

internal sealed class OfferSkillConfiguration : IEntityTypeConfiguration<OfferSkill>
{
    public void Configure(EntityTypeBuilder<OfferSkill> builder)
    {
        builder.HasKey(x => new { x.OfferId, x.SkillId });
    }
}
