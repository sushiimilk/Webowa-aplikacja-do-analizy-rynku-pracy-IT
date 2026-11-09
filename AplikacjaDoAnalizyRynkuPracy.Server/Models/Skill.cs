namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

// Wspólny słownik umiejętności dla ofert i CV -> dopasowanie CV to JOIN po SkillId
public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<OfferSkill> OfferSkills { get; set; } = new List<OfferSkill>();
    public ICollection<CvSkill> CvSkills { get; set; } = new List<CvSkill>();
}
