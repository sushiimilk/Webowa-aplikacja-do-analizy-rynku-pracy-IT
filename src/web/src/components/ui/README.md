# components/ui – podstawowe klocki interfejsu

Najmniejsze komponenty wielokrotnego użytku. Nie wiedzą nic o ofertach, CV ani backendzie – dostają dane przez propsy i tylko je rysują.

| Plik | Co to |
|---|---|
| `StatusMessage.tsx` | Komunikat „Ładowanie...”, „Brak danych” (`variant="info"`) albo błąd (`variant="error"`) |
| `ui.css` | Style komponentów z tego folderu |

Przyciski i pola formularzy nie mają komponentów – używamy zwykłych `<button className="button">`, `<input>`, `<select>` ze stylami z `app/index.css`.
