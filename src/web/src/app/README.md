# web/src/app – szkielet aplikacji

| Plik | Co to |
|---|---|
| `App.tsx` | Główny komponent – uruchamia router (`RouterProvider`) |
| `router.tsx` | Mapa adresów: który adres pokazuje którą stronę |
| `Layout.tsx` | Wspólna rama każdej strony: menu u góry, stopka ze statusem backendu, w środku `<Outlet />` na aktualną stronę |
| `NotFoundPage.tsx` | Strona 404 dla nieznanych adresów |
| `app.css` | Style ramy: menu, kontener treści, stopka |
| `index.css` | Style globalne: kolory (zmienne CSS, tryb jasny i ciemny), pola formularzy, przyciski `.button` |

## Strony

| Adres | Strona |
|---|---|
| `/` | `features/offers` – lista ofert z filtrami |
| `/statystyki` | `features/analytics` – wskaźniki i popularność technologii |
| `/cv` | `features/cv` – dopasowanie CV do ofert |

## Jak dodać nową stronę

1. Załóż funkcję w `features/` (np. `features/auth/components/LoginPage.tsx`).
2. Dopisz trasę w `router.tsx` w `children` layoutu, **przed** trasą `*`.
3. Jeśli strona ma być w menu – dopisz link do tablicy `links` w `Layout.tsx`.

`app/` może importować z `features/`, `components/` i `lib/`. Odwrotnie – nie.
