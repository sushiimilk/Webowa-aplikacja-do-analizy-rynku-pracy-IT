using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RynekIT.Domain.Users;

namespace RynekIT.Infrastructure.Persistence.Configurations;

internal sealed class CvSkillConfiguration : IEntityTypeConfiguration<CvSkill>
{
    public void Configure(EntityTypeBuilder<CvSkill> builder)
    {
        builder.HasKey(x => new { x.CvId, x.SkillId });
    }
}
