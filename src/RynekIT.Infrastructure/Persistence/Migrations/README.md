# Persistence/Migrations – historia zmian bazy

Migracja to plik C# opisujący jedną zmianę schematu bazy, np. „dodaj tabelę”, „dodaj kolumnę”. Dzięki migracjom każdy w zespole ma identyczną bazę.

**Tych plików nie edytujemy ręcznie.** Generuje je narzędzie `dotnet ef`.

## Typowy scenariusz

1. Zmieniasz encję w `Domain` albo konfigurację w `Configurations/`.
2. Z katalogu głównego repo generujesz migrację (nazwa po angielsku, opisowa, np. `AddSkillCategory`):
   ```bash
   dotnet ef migrations add NazwaZmiany --project src/RynekIT.Infrastructure --startup-project src/RynekIT.Api -o Persistence/Migrations
   ```
3. Przeglądasz wygenerowany plik `..._NazwaZmiany.cs` – czy robi to, co miało.
4. Aplikujesz zmianę na swojej bazie:
   ```bash
   dotnet ef database update --project src/RynekIT.Infrastructure --startup-project src/RynekIT.Api
   ```
5. Commitujesz migrację razem ze zmianą w kodzie.

## Po pobraniu cudzych zmian (git pull)

Jeśli ktoś dodał migrację – uruchom tylko `database update` z punktu 4.

## Pomyłka?

Jeśli migracja **nie była jeszcze wypchnięta**: `dotnet ef migrations remove --project src/RynekIT.Infrastructure --startup-project src/RynekIT.Api`.
Jeśli była wypchnięta – nie usuwaj, zrób nową migrację, która cofa zmianę.
