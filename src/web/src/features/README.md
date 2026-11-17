# web/src/features – funkcje aplikacji

Każda funkcja aplikacji ma swój folder. W środku zawsze ten sam układ:

```
offers/
├── api/          ← funkcje wołające backend, np. getOffers()
├── components/   ← komponenty React tej funkcji, np. <OffersList />
└── types.ts      ← typy danych (jak wygląda oferta w JSON-ie)
```

Opcjonalnie: `hooks/` (własne hooki), `utils/` (funkcje pomocnicze).

| Folder | Funkcja |
|---|---|
| `offers/` | Lista i wyszukiwarka ofert (backend: `GET /api/offers`) |
| `analytics/` | Wskaźniki rynku i popularność technologii (**na mockach**) |
| `cv/` | Wgranie CV i dopasowanie do ofert (**na mockach**) |
| `system-status/` | Wskaźnik, czy backend działa |

Planowane: `auth/` (logowanie), `favorites/` (ulubione).

## Kolejność pracy nad funkcją

```
types.ts → api/ → components/ → strona XPage.tsx → trasa w app/router.tsx
```

## Mocki

Gdy backend nie ma jeszcze endpointu, funkcja w `api/` zwraca dane z `api/mock-data.ts` przez `fakeRequest` z `@/lib/fake-request`. Kształt danych jest taki sam jak docelowy, więc komponenty nie wiedzą, że to mock. Po podłączeniu backendu zmienia się tylko wnętrze funkcji w `api/` (komentarz `TODO` mówi, na co), a `mock-data.ts` się usuwa.

## Zasady

- Funkcja **nie importuje** z innej funkcji (`offers` nie sięga do `cv`). Jeśli coś jest wspólne → `components/` albo `lib/`.
- Typy w `types.ts` muszą odpowiadać temu, co zwraca backend (rekordy w `RynekIT.Application`).
