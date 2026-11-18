using RynekIT.Domain.Offers;
using RynekIT.Domain.Users;

namespace RynekIT.Domain.Skills;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public SkillCategory Category { get; set; } = SkillCategory.Other;

    public ICollection<SkillAlias> Aliases { get; set; } = new List<SkillAlias>();
    public ICollection<OfferSkill> OfferSkills { get; set; } = new List<OfferSkill>();
    public ICollection<CvSkill> CvSkills { get; set; } = new List<CvSkill>();
}
