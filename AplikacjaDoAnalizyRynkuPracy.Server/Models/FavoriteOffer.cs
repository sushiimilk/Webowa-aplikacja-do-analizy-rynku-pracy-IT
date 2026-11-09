namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

public class FavoriteOffer
{
    public string UserId { get; set; } = null!;
    public AppUser User { get; set; } = null!;

    public int OfferId { get; set; }
    public Offer Offer { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
