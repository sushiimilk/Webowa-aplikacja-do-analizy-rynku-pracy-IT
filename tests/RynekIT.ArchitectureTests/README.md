# RynekIT.ArchitectureTests – strażnik architektury

Te testy nie sprawdzają, czy kod działa, tylko czy jest **dobrze ułożony**. Używają biblioteki NetArchTest.

Sprawdzane zasady (`LayerDependencyTests.cs`):

- Domain nie zależy od Application, Infrastructure ani Api.
- Domain nie używa Entity Framework ani ASP.NET.
- Application nie zależy od Infrastructure ani Api.
- Infrastructure nie zależy od Api.
- Klasy `...Endpoints` w Api są statyczne i leżą w `RynekIT.Api.Endpoints`.

Jeśli test pada: komunikat podaje, które klasy łamią zasadę. Zwykle wystarczy przenieść kod do właściwej warstwy albo dodać interfejs w `Application/Abstractions`.
