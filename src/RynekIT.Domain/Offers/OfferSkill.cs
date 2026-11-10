using RynekIT.Domain.Skills;

namespace RynekIT.Domain.Offers;

public class OfferSkill
{
    public int OfferId { get; set; }
    public Offer Offer { get; set; } = null!;

    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
}
