# RynekIT – analiza rynku pracy IT

Projekt inżynierski: aplikacja webowa, która pobiera oferty pracy IT z wybranych źródeł, udostępnia wyszukiwarkę, panel analityczny (KPI, popularność technologii) oraz dopasowanie CV do ofert.

| Warstwa | Technologia |
|---|---|
| Backend | ASP.NET Core 10 (Minimal API), Entity Framework Core, ASP.NET Core Identity |
| Baza danych | Microsoft SQL Server 2022 (lokalnie w Dockerze) |
| Frontend | React 19, TypeScript, Vite |
| Pobieranie danych | Python 3.12+ (requests, Scrapy, Playwright) |
| CI | GitHub Actions |

## Struktura repozytorium

```
.
├── src/
│   ├── RynekIT.Domain/            encje biznesowe – bez żadnych zależności
│   ├── RynekIT.Application/       przypadki użycia (jeden folder = jedna operacja)
│   ├── RynekIT.Infrastructure/    baza danych (EF Core), konta użytkowników (Identity)
│   ├── RynekIT.Api/               punkt wejścia: endpointy HTTP /api/...
│   ├── web/                       frontend React + TypeScript
│   └── scraper/                   pobieranie ofert (Python)
├── tests/
│   ├── RynekIT.Application.UnitTests/
│   └── RynekIT.ArchitectureTests/ pilnuje zasad Clean Architecture
├── docs/                          schemat bazy, decyzje architektoniczne (ADR)
├── .github/                       CI i szablon Pull Requesta
├── Directory.Build.props          wspólne ustawienia wszystkich projektów .NET
├── Directory.Packages.props       wersje wszystkich pakietów NuGet w jednym miejscu
├── global.json                    wymagana wersja .NET SDK
├── .editorconfig                  wspólny styl kodu
├── docker-compose.yml             SQL Server do pracy lokalnej
└── RynekIT.slnx                   solucja Visual Studio
```

Backend jest zbudowany według **Clean Architecture**: zależności idą tylko do środka (`Api → Application → Domain`, `Infrastructure → Application`). Szczegóły i uzasadnienie: [src/README.md](src/README.md) i [docs/adr/0001-clean-architecture.md](docs/adr/0001-clean-architecture.md).

**W każdym folderze jest plik README** z wyjaśnieniem, do czego służy folder i jak z niego korzystać.

## Uruchomienie lokalne

Wymagania: .NET 10 SDK, Node.js 22+, Docker Desktop (albo SQL Server LocalDB), Python 3.12+ (tylko do scrapera).

### 1. Baza danych

1. Skopiuj `.env.example` jako `.env` i ustaw własne hasło `MSSQL_SA_PASSWORD`: minimum 8 znaków i 3 z 4 kategorii (wielkie litery, małe litery, cyfry, symbole).
2. Uruchom SQL Server w kontenerze:
   ```bash
   docker compose up -d
   ```
   Kontener nasłuchuje tylko na `127.0.0.1`. Po kilkunastu sekundach `docker compose ps` powinien pokazać status `healthy`.
3. Przekaż backendowi to samo hasło przez user-secrets (hasło nie trafia do repozytorium):
   ```bash
   dotnet user-secrets set "Database:Password" "<hasło z .env>" --project src/RynekIT.Api
   ```
4. Utwórz schemat bazy:
   ```bash
   dotnet tool restore
   dotnet ef database update --project src/RynekIT.Infrastructure --startup-project src/RynekIT.Api
   ```

**Bez Dockera (LocalDB z Visual Studio):** pomiń punkty 1–3 i ustaw cały connection string:

```bash
dotnet user-secrets set "ConnectionStrings:Default" "Server=(localdb)\MSSQLLocalDB;Database=RynekIT;Trusted_Connection=True;TrustServerCertificate=True" --project src/RynekIT.Api
```

### 2. Aplikacja

```bash
cd src/web && npm install && cd ../..
dotnet run --project src/RynekIT.Api --launch-profile https
```

Albo F5 w Visual Studio (projekt startowy: `RynekIT.Api`). Backend sam uruchamia frontend.

- Aplikacja: https://localhost:52070
- API: https://localhost:7119/api/health
- Opis API (OpenAPI): https://localhost:7119/openapi/v1.json

### 3. Testy

```bash
dotnet test                                     # backend
cd src/web && npm run lint && npm run build     # frontend
cd src/scraper && pytest && ruff check .        # scraper
```

## Dokumentacja

- [Schemat bazy danych i kontrakt scrapera](docs/schemat-bazy.md)
- [Decyzje architektoniczne (ADR)](docs/adr/README.md)
- [Zasady pracy w zespole](CONTRIBUTING.md)

## Zespół

Szymon Rogowski, Grzegorz Olesiak, Wojciech Pruchnicki
