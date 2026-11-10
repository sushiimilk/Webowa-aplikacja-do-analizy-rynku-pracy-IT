# RynekIT.Application – co aplikacja potrafi

**Co tu jest:** *przypadki użycia* (ang. use cases), czyli konkretne rzeczy, które aplikacja robi: „wyszukaj oferty”, „policz statystyki”, „dopasuj CV do oferty”.

Zależy tylko od `Domain`. **Nie wie**, że baza to SQL Server ani że frontend łączy się przez HTTP.

## Organizacja: jeden przypadek użycia = jeden folder

To podejście nazywa się *vertical slices* („pionowe plasterki”). Wszystko, co dotyczy jednej operacji, leży razem:

```
Offers/
└── SearchOffers/
    ├── SearchOffersQuery.cs     ← co przychodzi (parametry: fraza, filtr, strona)
    ├── SearchOffersHandler.cs   ← logika: co z tym zrobić
    └── OfferListItem.cs         ← co wychodzi (dane jednej oferty na liście)
```

## Foldery

| Folder | Zawartość |
|---|---|
| `Abstractions/` | Interfejsy, które Infrastructure musi zaimplementować (np. dostęp do bazy) |
| `Common/` | Drobne klasy wspólne dla wielu przypadków użycia (np. stronicowanie) |
| `Offers/` | Wyszukiwanie i szczegóły ofert |
| `Analytics/` | Statystyki i wykresy (do zrobienia) |
| `Cv/` | Analiza CV i dopasowanie do oferty (do zrobienia) |
| `Favorites/` | Ulubione oferty (do zrobienia) |

## Jak dodać nowy przypadek użycia (krok po kroku)

Przykład: „pobierz szczegóły oferty po id”.

1. Utwórz folder `Offers/GetOfferById/`.
2. Dodaj `GetOfferByIdQuery.cs` – rekord z parametrami: `public sealed record GetOfferByIdQuery(int Id);`
3. Dodaj klasę z wynikiem, np. `OfferDetails.cs`.
4. Dodaj `GetOfferByIdHandler.cs` z metodą `HandleAsync(query, cancellationToken)`. Handler dostaje `IAppDbContext` w konstruktorze.
5. Zarejestruj handler w `DependencyInjection.cs`: `services.AddScoped<GetOfferByIdHandler>();`
6. Dodaj endpoint w `RynekIT.Api/Endpoints/OffersEndpoints.cs`.
7. Dodaj testy w `tests/RynekIT.Application.UnitTests/Offers/`.

Najprościej: skopiuj `SearchOffers/` i przerób.
