# Infrastructure/Persistence – baza danych

| Element | Co to |
|---|---|
| `AppDbContext.cs` | Główna klasa Entity Framework. Każdy `DbSet<X>` to jedna tabela. Implementuje `IAppDbContext` z Application |
| `Configurations/` | Konfiguracja tabel: długości kolumn, indeksy, klucze. Jedna encja = jeden plik |
| `Migrations/` | Historia zmian schematu bazy. **Generowane automatycznie** |
| `Staging/` | Tabele robocze, do których scraper wrzuca surowe dane |

`AppDbContext` sam wczytuje wszystkie konfiguracje z folderu `Configurations/` (`ApplyConfigurationsFromAssembly`), więc po dodaniu nowej konfiguracji nie trzeba nic dopisywać ręcznie.

Schemat bazy z diagramem: `docs/schemat-bazy.md`.
