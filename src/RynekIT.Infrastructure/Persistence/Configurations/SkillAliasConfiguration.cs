using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RynekIT.Domain.Skills;

namespace RynekIT.Infrastructure.Persistence.Configurations;

internal sealed class SkillAliasConfiguration : IEntityTypeConfiguration<SkillAlias>
{
    public void Configure(EntityTypeBuilder<SkillAlias> builder)
    {
        builder.Property(x => x.Alias).HasMaxLength(100);
        builder.HasIndex(x => x.Alias).IsUnique();

        builder.HasOne(x => x.Skill)
            .WithMany(x => x.Aliases)
            .HasForeignKey(x => x.SkillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
