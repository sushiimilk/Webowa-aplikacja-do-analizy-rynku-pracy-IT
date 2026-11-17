# offers/components – komponenty ofert

Komponenty React używane tylko przez funkcję „oferty”. Nazwa pliku = nazwa komponentu, PascalCase: `OffersList.tsx`.

Komponent pobiera dane przez funkcje z `../api/` i hook `useAsync` z `@/lib/use-async`, a potem je wyświetla. Zawsze obsłuż stany: ładowanie, błąd, brak danych, dane.

Style funkcji są w `offers.css` (importuje go `OffersList.tsx`).
