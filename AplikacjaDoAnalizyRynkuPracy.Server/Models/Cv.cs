namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

public class Cv
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public string FileName { get; set; } = null!;
    public DateTime UploadedAt { get; set; }

    public ICollection<CvSkill> CvSkills { get; set; } = new List<CvSkill>();
}

public class CvSkill
{
    public int CvId { get; set; }
    public Cv Cv { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
}
