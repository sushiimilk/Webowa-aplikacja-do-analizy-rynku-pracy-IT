# offers/api – komunikacja z backendem

Jedna funkcja = jeden endpoint backendu. Plik nazywamy od czynności: `get-offers.ts`, później np. `get-offer.ts`.

Funkcje korzystają ze wspólnego klienta `apiGet` z `@/lib/api-client` – nie piszemy `fetch` ręcznie w komponentach.
