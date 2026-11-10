# RynekIT.Domain – serce aplikacji

**Co tu jest:** klasy opisujące świat, którym zajmuje się aplikacja – *encje*. Oferta pracy, firma, umiejętność, CV, ulubiona oferta.

**Złota zasada:** ten projekt **nie zależy od niczego**. Żadnego Entity Framework, żadnego ASP.NET, żadnych bibliotek. Tylko czysty C#. Sprawdzają to testy architektury.

## Foldery

| Folder | Zawartość |
|---|---|
| `Offers/` | Oferta, firma, tryb pracy, okres wynagrodzenia, powiązanie oferta–umiejętność |
| `Skills/` | Słownik umiejętności (Python, SQL, Docker...) |
| `Users/` | Dane użytkownika: CV, jego umiejętności, ulubione oferty |

## Jak dodać nową encję

1. Wybierz folder, do którego pasuje tematycznie (albo załóż nowy, np. `Notifications/`).
2. Utwórz zwykłą klasę z właściwościami, np. `public string Name { get; set; } = null!;`
3. Namespace = ścieżka folderu, np. `RynekIT.Domain.Notifications`.
4. Dodaj `DbSet` w `IAppDbContext` (Application) i w `AppDbContext` (Infrastructure).
5. Dodaj konfigurację bazy w `Infrastructure/Persistence/Configurations/` i zrób migrację.

## Czego tu NIE wolno

- `using Microsoft.EntityFrameworkCore;` – konfiguracja bazy jest w Infrastructure.
- Atrybuty typu `[Key]`, `[MaxLength]` – to też szczegół bazy, nie biznesu.
- Klasy użytkownika z ASP.NET Identity – są w `Infrastructure/Identity`. Tutaj użytkownik to tylko jego `UserId`.
