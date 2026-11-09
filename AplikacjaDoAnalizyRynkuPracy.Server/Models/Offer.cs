namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

public enum WorkMode { Remote, Hybrid, Onsite }
public enum SalaryPeriod { Hour, Month, Year }

public class Offer
{
    public int Id { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    // Pochodzenie oferty
    public string Source { get; set; } = null!;          // "remoteok", "himalayas", ...
    public string SourceOfferId { get; set; } = null!;   // ID oferty w serwisie źródłowym
    public string SourceUrl { get; set; } = null!;       // link do oryginału (wymagany przez warunki API Remote OK)

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public WorkMode? WorkMode { get; set; }

    // Wynagrodzenie dokładnie jak w ogłoszeniu. NULL = brak widełek (nigdy 0).
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string? SalaryCurrency { get; set; }          // ISO 4217, np. USD, PLN
    public SalaryPeriod? SalaryPeriod { get; set; }

    // Po normalizacji do jednej waluty i jednego okresu (docelowe jednostki ustalić z Wojtkiem).
    // Wypełniane przy przenoszeniu danych z stg.RawOffers.
    public decimal? SalaryMinNormalized { get; set; }
    public decimal? SalaryMaxNormalized { get; set; }

    public DateTime? PublishedAt { get; set; }
    public DateTime FetchedAt { get; set; }

    public ICollection<OfferSkill> OfferSkills { get; set; } = new List<OfferSkill>();
    public ICollection<FavoriteOffer> FavoritedBy { get; set; } = new List<FavoriteOffer>();
}
