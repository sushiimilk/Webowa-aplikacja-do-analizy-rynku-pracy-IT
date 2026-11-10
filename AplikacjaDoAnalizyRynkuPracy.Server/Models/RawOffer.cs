namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

public class RawOffer
{
    public long Id { get; set; }
    public string Source { get; set; } = null!;
    public string SourceOfferId { get; set; } = null!;
    public string SourceUrl { get; set; } = null!;
    public string RawJson { get; set; } = null!;
    public DateTime FetchedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}
