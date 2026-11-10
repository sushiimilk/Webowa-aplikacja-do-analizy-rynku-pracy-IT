using RynekIT.Domain.Users;

namespace RynekIT.Domain.Offers;

public class Offer
{
    public int Id { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public string Source { get; set; } = null!;
    public string SourceOfferId { get; set; } = null!;
    public string SourceUrl { get; set; } = null!;

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public WorkMode? WorkMode { get; set; }

    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string? SalaryCurrency { get; set; }
    public SalaryPeriod? SalaryPeriod { get; set; }

    public decimal? SalaryMinNormalized { get; set; }
    public decimal? SalaryMaxNormalized { get; set; }

    public DateTime? PublishedAt { get; set; }
    public DateTime FetchedAt { get; set; }

    public ICollection<OfferSkill> OfferSkills { get; set; } = new List<OfferSkill>();
    public ICollection<FavoriteOffer> FavoritedBy { get; set; } = new List<FavoriteOffer>();
}
