# Webowa aplikacja do analizy rynku pracy IT

Projekt inżynierski: aplikacja pobierająca oferty pracy IT z wybranych źródeł,
z wyszukiwarką, panelem analitycznym (KPI, popularność technologii)
oraz dopasowaniem CV do ofert.

## Struktura repozytorium

```
AplikacjaDoAnalizyRynkuPracy.slnx          # rozwiązanie Visual Studio
AplikacjaDoAnalizyRynkuPracy.Server/       # backend – ASP.NET Core Web API (.NET 10)
├── Controllers/                           # endpointy REST (/api/...)
├── Data/                                  # DbContext EF Core, migracje
├── Models/                                # encje bazy (Offer, Company, Skill, User...)
├── Dtos/                                  # obiekty zwracane przez API
└── Services/                              # logika: wyszukiwanie, analityka, dopasowanie CV
aplikacjadoanalizyrynkupracy.client/       # frontend – React + Vite
└── src/
    ├── api/                               # komunikacja z backendem
    ├── components/                        # komponenty wielokrotnego użytku
    ├── pages/                             # widoki: wyszukiwarka, panel, CV, ulubione
    └── hooks/                             # własne hooki React
scraper/                                   # pobieranie ofert – Python (REST API, Scrapy, Playwright)
└── sources/                               # osobny moduł na każde źródło danych
docs/                                      # dokumentacja projektu
```

## Uruchomienie (dev)

Wymagania: .NET 10 SDK, Node.js, Docker Desktop (albo SQL Server LocalDB, patrz niżej).

### Baza danych

1. Skopiuj `.env.example` jako `.env` i ustaw własne hasło `MSSQL_SA_PASSWORD`: minimum 8 znaków i 3 z 4 kategorii (wielkie litery, małe litery, cyfry, symbole).
2. Uruchom SQL Server w kontenerze:
   ```bash
   docker compose up -d
   ```
   Kontener nasłuchuje tylko na `127.0.0.1`. Stan sprawdzisz przez `docker compose ps`; po kilkunastu sekundach powinien mieć status `healthy`.
3. Przekaż backendowi to samo hasło przez user-secrets. Hasło nie trafia do repozytorium:
   ```bash
   dotnet user-secrets set "Database:Password" "<hasło z .env>" --project AplikacjaDoAnalizyRynkuPracy.Server
   ```
4. Utwórz schemat bazy:
   ```bash
   dotnet tool restore
   dotnet ef database update --project AplikacjaDoAnalizyRynkuPracy.Server
   ```

Connection string bez hasła jest w `appsettings.Development.json`. Backend dokleja do niego hasło z `Database:Password`.

**Bez Dockera (LocalDB z Visual Studio):** nadpisz cały connection string w user-secrets i pomiń punkty 1–3:

```bash
dotnet user-secrets set "ConnectionStrings:Default" "Server=(localdb)\MSSQLLocalDB;Database=RynekIT;Trusted_Connection=True;TrustServerCertificate=True" --project AplikacjaDoAnalizyRynkuPracy.Server
```

**Nowa migracja po zmianie modelu:**

```bash
dotnet ef migrations add NazwaZmiany --project AplikacjaDoAnalizyRynkuPracy.Server -o Data/Migrations
```

### Aplikacja

```bash
cd aplikacjadoanalizyrynkupracy.client && npm install
cd ../AplikacjaDoAnalizyRynkuPracy.Server && dotnet run --launch-profile https
```

Backend startuje frontend automatycznie (SPA proxy). Sprawdzenie API: `GET /api/health`.
Albo po prostu F5 w Visual Studio.

W trybie Development w konsoli widać zapytania SQL wysyłane przez EF Core, razem z wartościami parametrów.

Schemat bazy i kontrakt scrapera: [docs/schemat-bazy.md](docs/schemat-bazy.md).

## Zespół

Szymon Rogowski, Grzegorz Olesiak, Wojciech Pruchnicki
