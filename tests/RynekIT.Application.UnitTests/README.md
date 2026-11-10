# RynekIT.Application.UnitTests

Testy jednostkowe przypadków użycia z `RynekIT.Application`.

Układ folderów **odpowiada** układowi w Application: test `SearchOffersHandler` jest w `Offers/SearchOffersHandlerTests.cs`.

`TestDbContext.cs` – baza danych w pamięci RAM (EF Core InMemory), podstawiana pod `IAppDbContext`. Każdy test dostaje własną, pustą bazę, więc testy nie przeszkadzają sobie nawzajem.

## Schemat testu (Arrange – Act – Assert)

```csharp
[Fact]
public async Task WorkMode_FiltersByWorkMode()
{
    await using var db = await CreateDbWithOffersAsync();             // Arrange: przygotuj dane
    var result = await new SearchOffersHandler(db).HandleAsync(...);  // Act: wykonaj
    Assert.Single(result.Items);                                      // Assert: sprawdź
}
```

Uwaga: baza w pamięci nie sprawdza ograniczeń SQL (np. check constraints). To sprawdzą testy integracyjne.
