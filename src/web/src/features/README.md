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
| `offers/` | Lista i wyszukiwarka ofert |
| `system-status/` | Wskaźnik, czy backend działa |

Planowane: `analytics/` (panel KPI i wykresy), `cv/` (dopasowanie CV), `auth/` (logowanie), `favorites/` (ulubione).

## Zasady

- Funkcja **nie importuje** z innej funkcji (`offers` nie sięga do `cv`). Jeśli coś jest wspólne → `components/` albo `lib/`.
- Typy w `types.ts` muszą odpowiadać temu, co zwraca backend (rekordy w `RynekIT.Application`).
