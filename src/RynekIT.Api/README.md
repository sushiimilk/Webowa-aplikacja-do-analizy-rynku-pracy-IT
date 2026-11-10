# RynekIT.Api – punkt wejścia aplikacji

**Co tu jest:** projekt, który się uruchamia (F5 w Visual Studio). Wystawia adresy HTTP (`/api/...`), z których korzysta frontend, i serwuje sam frontend.

**Zasada: ta warstwa jest cienka.** Endpoint tylko odbiera żądanie, woła handler z Application i zwraca wynik. Żadnej logiki biznesowej ani zapytań do bazy tutaj.

## Pliki i foldery

| Element | Co to |
|---|---|
| `Program.cs` | Start aplikacji: rejestracja warstw (`AddApplication`, `AddInfrastructure`), obsługa błędów, mapowanie endpointów |
| `Endpoints/` | Definicje adresów HTTP, pogrupowane tematycznie |
| `Properties/launchSettings.json` | Profile uruchomieniowe (porty, środowisko Development) |
| `appsettings.json` | Ustawienia wspólne (produkcja) |
| `appsettings.Development.json` | Ustawienia lokalne: connection string do bazy w Dockerze, szczegółowe logi |
| `RynekIT.Api.http` | Gotowe zapytania do przetestowania API z Visual Studio / VS Code |

## Uruchomienie

```bash
dotnet run --project src/RynekIT.Api --launch-profile https
```

Backend sam uruchomi frontend. Aplikacja: https://localhost:52070. Opis API (OpenAPI): https://localhost:7119/openapi/v1.json.
