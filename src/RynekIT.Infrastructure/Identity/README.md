# Infrastructure/Identity – konta użytkowników

Logowanie e-mailem i hasłem obsługuje gotowy mechanizm Microsoftu: **ASP.NET Core Identity**. Sam tworzy tabele (`AspNetUsers`, `AspNetRoles`...), haszuje hasła i sprawdza logowanie. My nie przechowujemy haseł sami.

| Plik | Co to |
|---|---|
| `AppUser.cs` | Nasz użytkownik. Dziedziczy po `IdentityUser` (ma już e-mail, hash hasła itp.) |

Jeśli użytkownik ma mieć dodatkowe pole (np. imię), dodaj je w `AppUser` i zrób migrację.

Dlaczego nie w Domain? Bo `IdentityUser` pochodzi z biblioteki ASP.NET, a Domain nie może zależeć od frameworków. Encje w Domain trzymają tylko `UserId`.
