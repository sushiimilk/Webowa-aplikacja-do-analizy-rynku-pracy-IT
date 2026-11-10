# web/src/app – szkielet aplikacji

| Plik | Co to |
|---|---|
| `App.tsx` | Główny komponent – składa stronę z komponentów z `features/` |
| `app.css` | Style układu strony |
| `index.css` | Style globalne (czcionka, tło) |

Gdy dojdzie więcej stron, tu pojawi się routing (np. `router.tsx` z React Router) i ewentualnie wspólny layout (nawigacja).

`app/` może importować z `features/`, `components/` i `lib/`. Odwrotnie – nie.
