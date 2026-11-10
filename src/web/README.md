# web – frontend (React + TypeScript)

To, co widzi użytkownik w przeglądarce. Zbudowane w **React** z **TypeScriptem** (JavaScript z typami – błędy wychodzą przy pisaniu, a nie u użytkownika). Narzędzie do budowania: **Vite**.

## Struktura (wzorzec Bulletproof React)

```
src/
├── main.tsx        ← start aplikacji, podpina React do index.html
├── app/            ← „szkielet”: główny komponent, globalne style, (później) routing
├── features/       ← funkcje aplikacji, każda w swoim folderze
├── components/     ← wspólne komponenty UI używane przez wiele funkcji
└── lib/            ← narzędzia: klient API, hooki pomocnicze
```

Zasada: kod jednej funkcji (np. lista ofert) leży razem w `features/offers/`. Jeśli coś przyda się w kilku funkcjach – przenosimy do `components/` albo `lib/`.

Import przez alias `@/` = folder `src/`, np. `import { apiGet } from '@/lib/api-client';`.

## Komendy (w tym folderze)

| Komenda | Co robi |
|---|---|
| `npm install` | Instaluje zależności (raz, po sklonowaniu i po zmianach w `package.json`) |
| `npm run dev` | Serwer deweloperski na https://localhost:52070 (zwykle uruchamia go sam backend) |
| `npm run lint` | Sprawdza styl i typowe błędy (ESLint) |
| `npm run typecheck` | Sprawdza typy TypeScript |
| `npm run build` | Buduje wersję produkcyjną do `dist/` |

Zapytania do `/api/...` Vite przekazuje do backendu (.NET).
