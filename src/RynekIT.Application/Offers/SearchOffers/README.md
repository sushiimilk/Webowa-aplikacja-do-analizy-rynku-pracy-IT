# SearchOffers – wyszukiwanie ofert (przykładowy przypadek użycia)

Wzorcowy przykład, jak wygląda przypadek użycia w tym projekcie.

| Plik | Rola |
|---|---|
| `SearchOffersQuery.cs` | **Wejście.** Parametry od użytkownika: fraza, tryb pracy, numer i rozmiar strony |
| `SearchOffersHandler.cs` | **Logika.** Buduje zapytanie do bazy, filtruje, sortuje (najnowsze pierwsze), dzieli na strony |
| `OfferListItem.cs` | **Wyjście.** Dane jednej oferty na liście – tylko to, czego potrzebuje frontend |

Przepływ:

```
GET /api/offers?phrase=python   (Api/Endpoints/OffersEndpoints.cs)
        │  tworzy SearchOffersQuery
        ▼
SearchOffersHandler.HandleAsync  (tutaj)
        │  pyta IAppDbContext o oferty
        ▼
PagedResult<OfferListItem>  →  JSON do przeglądarki
```

Dlaczego osobny `OfferListItem`, a nie po prostu `Offer`? Bo encja ma pola, których frontend nie potrzebuje (np. pełny opis, dane techniczne). Zwracamy tylko to, co trzeba.

Testy: `tests/RynekIT.Application.UnitTests/Offers/SearchOffersHandlerTests.cs`.
