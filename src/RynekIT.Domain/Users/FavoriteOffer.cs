using RynekIT.Domain.Offers;

namespace RynekIT.Domain.Users;

public class FavoriteOffer
{
    public string UserId { get; set; } = null!;

    public int OfferId { get; set; }
    public Offer Offer { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
