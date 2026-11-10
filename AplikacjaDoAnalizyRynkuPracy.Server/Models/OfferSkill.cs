namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

public class OfferSkill
{
    public int OfferId { get; set; }
    public Offer Offer { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
}
