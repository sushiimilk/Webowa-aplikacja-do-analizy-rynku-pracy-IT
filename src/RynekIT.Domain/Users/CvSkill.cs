using RynekIT.Domain.Skills;

namespace RynekIT.Domain.Users;

public class CvSkill
{
    public int CvId { get; set; }
    public Cv Cv { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
}
