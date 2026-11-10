using Microsoft.AspNetCore.Identity;

namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

public class AppUser : IdentityUser
{
    public ICollection<FavoriteOffer> FavoriteOffers { get; set; } = new List<FavoriteOffer>();
    public ICollection<Cv> Cvs { get; set; } = new List<Cv>();
}
