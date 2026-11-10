# Application/Abstractions – interfejsy do świata zewnętrznego

Application potrzebuje bazy danych, ale **nie może** zależeć od Infrastructure (strzałki idą tylko do środka!). Rozwiązanie: Application definiuje *interfejs* – „potrzebuję czegoś, co umie to i to” – a Infrastructure dostarcza *implementację*.

| Plik | Co to |
|---|---|
| `IAppDbContext.cs` | Dostęp do tabel bazy. Implementuje go `AppDbContext` w Infrastructure |

To się nazywa *Dependency Inversion* (odwrócenie zależności) – litera D w SOLID.

W testach podstawiamy pod ten interfejs bazę w pamięci (patrz `tests/RynekIT.Application.UnitTests/TestDbContext.cs`), więc testy nie potrzebują SQL Servera.

**Kiedy dodać nowy interfejs:** gdy przypadek użycia potrzebuje czegoś zewnętrznego, np. wysłania e-maila → `IEmailSender`. Interfejs tutaj, implementacja w Infrastructure.
