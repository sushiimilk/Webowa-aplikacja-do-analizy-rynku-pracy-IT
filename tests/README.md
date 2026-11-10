# tests – testy backendu (.NET)

| Projekt | Co testuje |
|---|---|
| `RynekIT.Application.UnitTests` | Logikę przypadków użycia (handlery), na bazie w pamięci – bez SQL Servera |
| `RynekIT.ArchitectureTests` | Czy kod trzyma się zasad Clean Architecture (kto od kogo może zależeć) |

Planowane: `RynekIT.Api.IntegrationTests` – testy całego API na prawdziwym SQL Serverze w kontenerze (Testcontainers).

## Uruchamianie

```bash
dotnet test                       # wszystkie testy z katalogu głównego
```

albo w Visual Studio: *Test → Test Explorer*.

## Nazewnictwo testów

`Metoda_Warunek_OczekiwanyWynik` albo opisowo: `Phrase_FiltersByTitle`, `Paging_ReturnsRequestedPageAndTotalCount`. Ktoś czytający nazwę ma wiedzieć, co poszło nie tak, bez otwierania kodu.

Testy frontendu i scrapera są przy swoich projektach (`src/web`, `src/scraper/tests`).
