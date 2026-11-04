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

Wymagania: .NET 10 SDK, Node.js.

```bash
cd aplikacjadoanalizyrynkupracy.client && npm install
cd ../AplikacjaDoAnalizyRynkuPracy.Server && dotnet run --launch-profile https
```

Backend startuje frontend automatycznie (SPA proxy). Sprawdzenie API: `GET /api/health`.
Albo po prostu F5 w Visual Studio.

## Zespół

Szymon Rogowski, Grzegorz Olesiak, Wojciech Pruchnicki
