# RynekIT.Infrastructure – szczegóły techniczne

**Co tu jest:** wszystko, co „dotyka świata zewnętrznego”: baza danych (Entity Framework Core + SQL Server), konta użytkowników (ASP.NET Identity). W przyszłości np. wysyłka e-maili.

Implementuje interfejsy zdefiniowane w `Application/Abstractions`.

## Foldery

| Folder | Zawartość |
|---|---|
| `Persistence/` | Wszystko o bazie danych: `AppDbContext`, konfiguracje tabel, migracje |
| `Identity/` | Konta użytkowników (logowanie e-mail + hasło) |

`DependencyInjection.cs` – rejestruje usługi tej warstwy. Wywoływane w `Api/Program.cs` przez `AddInfrastructure(...)`. Tu też budowany jest connection string do bazy (hasło dokładane z user-secrets).

## Najczęstsze komendy (uruchamiaj z katalogu głównego repo)

```bash
dotnet ef migrations add NazwaZmiany --project src/RynekIT.Infrastructure --startup-project src/RynekIT.Api -o Persistence/Migrations
dotnet ef database update --project src/RynekIT.Infrastructure --startup-project src/RynekIT.Api
```
