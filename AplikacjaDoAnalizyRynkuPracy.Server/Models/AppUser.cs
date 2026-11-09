using Microsoft.AspNetCore.Identity;

namespace AplikacjaDoAnalizyRynkuPracy.Server.Models;

// Login, e-mail i hash hasła daje IdentityUser - nie trzymamy haseł samodzielnie
public class AppUser : IdentityUser
{
    public ICollection<FavoriteOffer> FavoriteOffers { get; set; } = new List<FavoriteOffer>();
    public ICollection<Cv> Cvs { get; set; } = new List<Cv>();
}
