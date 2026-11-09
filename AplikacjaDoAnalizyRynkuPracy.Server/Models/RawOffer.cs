namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

// Tabela robocza (stg.RawOffers): scraper w Pythonie robi tu tylko INSERT.
// Dopasować kolumny do pól, które zwraca parse_offer w fetch_remoteok.py.
public class RawOffer
{
    public long Id { get; set; }
    public string Source { get; set; } = null!;
    public string SourceOfferId { get; set; } = null!;
    public string SourceUrl { get; set; } = null!;
    public string RawJson { get; set; } = null!;        // pełna surowa odpowiedź API
    public DateTime FetchedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }          // NULL = jeszcze nie przeniesiona do Offers
}
