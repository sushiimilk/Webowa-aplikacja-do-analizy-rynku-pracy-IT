# features/offers – oferty pracy

| Plik | Co robi |
|---|---|
| `types.ts` | Typy: `OfferListItem` (jedna oferta na liście), `PagedResult` (wynik ze stronami) |
| `api/get-offers.ts` | `getOffers()` – woła `GET /api/offers` z filtrami |
| `components/OffersList.tsx` | Tabela ofert: ładowanie, błąd, brak wyników, lista |

Po stronie backendu odpowiada temu `Application/Offers/SearchOffers`.

To wzorcowa funkcja – nowe (np. `analytics/`) zakładaj, kopiując ten układ.
