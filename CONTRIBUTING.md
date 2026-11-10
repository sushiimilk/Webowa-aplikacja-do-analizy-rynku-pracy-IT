# Zasady pracy w zespole

## Gałęzie

`main` zawsze działa. Nikt nie pushuje bezpośrednio do `main` – wszystko przechodzi przez Pull Request.

Nowa praca = nowa gałąź od aktualnego `main`:

```bash
git switch main
git pull
git switch -c feature/wyszukiwarka-filtry
```

| Prefiks | Kiedy | Przykład |
|---|---|---|
| `feature/` | nowa funkcja | `feature/panel-kpi` |
| `fix/` | poprawka błędu | `fix/paginacja-ofert` |
| `refactor/` | zmiana struktury bez zmiany działania | `refactor/clean-architecture` |
| `docs/` | tylko dokumentacja | `docs/schemat-bazy` |
| `chore/` | konfiguracja, zależności, CI | `chore/aktualizacja-pakietow` |

Nazwy gałęzi: małe litery, słowa rozdzielone myślnikami, bez polskich znaków.

## Commity

Krótki opis **co** się zmienia, w trybie rozkazującym, do ~70 znaków:

```
Dodaj filtr trybu pracy w wyszukiwarce ofert
Popraw liczenie stron przy pustym wyniku
```

Jeden commit = jedna logiczna zmiana. Nie mieszaj refaktoru z nową funkcją.

## Pull Request

1. Przed otwarciem PR: `git pull origin main` (albo rebase) i rozwiąż konflikty u siebie.
2. Lokalnie przechodzą: `dotnet test`, `npm run lint`, `npm run build`.
3. Otwórz PR do `main` i wypełnij szablon.
4. Co najmniej **jedna osoba z zespołu** przegląda kod i zatwierdza.
5. CI musi być zielone.
6. Merge robi autor PR, po akceptacji.

PR-y mają być małe – łatwiej je przejrzeć. Lepiej trzy PR po 200 linii niż jeden na 1500.

## Gdzie dodać nowy kod

| Chcę dodać... | Miejsce |
|---|---|
| nową encję / pojęcie biznesowe | `src/RynekIT.Domain/<Obszar>/` |
| nową operację (np. „pobierz statystyki”) | `src/RynekIT.Application/<Obszar>/<Operacja>/` |
| konfigurację tabeli w bazie | `src/RynekIT.Infrastructure/Persistence/Configurations/` |
| nowy adres HTTP | `src/RynekIT.Api/Endpoints/` |
| nowy ekran / funkcję frontendu | `src/web/src/features/<funkcja>/` |
| nowe źródło ofert | `src/scraper/src/rynekit_scraper/sources/` |

Szczegóły w README danego folderu.

## Baza danych

- Każda zmiana modelu = migracja w tym samym PR (instrukcja: `src/RynekIT.Infrastructure/Persistence/Migrations/README.md`).
- Nie edytujemy ani nie usuwamy migracji, które są już w `main`.
- Po `git pull` z cudzą migracją: `dotnet ef database update --project src/RynekIT.Infrastructure --startup-project src/RynekIT.Api`.

## Pakiety

- NuGet: wersję dopisujemy **tylko** w `Directory.Packages.props`, w `.csproj` samo `<PackageReference Include="..." />`.
- npm: `npm install <pakiet>` w `src/web`, commitujemy też `package-lock.json`.
- Python: dopisujemy do `dependencies` w `src/scraper/pyproject.toml`.

## Sekrety

Hasła, klucze API i connection stringi z hasłem **nigdy** nie trafiają do repozytorium. Lokalnie: `dotnet user-secrets` i plik `.env` (jest w `.gitignore`).
