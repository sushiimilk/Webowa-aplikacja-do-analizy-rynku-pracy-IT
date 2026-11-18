namespace RynekIT.Domain.Skills;

public class SkillAlias
{
    public int Id { get; set; }
    public string Alias { get; set; } = null!;
    public int SkillId { get; set; }

    public Skill Skill { get; set; } = null!;
}
