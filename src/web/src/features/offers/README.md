# features/offers – oferty pracy

| Plik | Co robi |
|---|---|
| `types.ts` | Typy: `OfferListItem` (jedna oferta na liście), `OfferFilters` (fraza + tryb pracy), `PagedResult` (wynik ze stronami) |
| `api/get-offers.ts` | `getOffers()` – woła `GET /api/offers` z filtrami i numerem strony |
| `utils/work-mode.ts` | Polskie nazwy trybów pracy i odczyt trybu z adresu URL |
| `components/OffersPage.tsx` | Strona `/`. Trzyma filtry i numer strony w adresie URL (`?phrase=...&workMode=...&page=...`) |
| `components/OfferFiltersForm.tsx` | Formularz: fraza + tryb pracy, „Szukaj” i „Wyczyść” |
| `components/OffersList.tsx` | Pobiera oferty dla filtrów: ładowanie, błąd, brak wyników, siatka kart, paginacja |
| `components/OfferCard.tsx` | Karta jednej oferty |
| `components/OffersPagination.tsx` | Przyciski „Poprzednia / Następna” |

Po stronie backendu odpowiada temu `Application/Offers/SearchOffers`.

To wzorcowa funkcja – nowe zakładaj, kopiując ten układ.
