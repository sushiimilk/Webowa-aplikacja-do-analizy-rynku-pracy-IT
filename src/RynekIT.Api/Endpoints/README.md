# Api/Endpoints – adresy HTTP

Każdy plik grupuje endpointy jednego tematu i jest *metodą rozszerzającą*, którą wywołuje `Program.cs`.

| Plik | Adresy |
|---|---|
| `HealthEndpoints.cs` | `GET /api/health` – czy backend działa |
| `OffersEndpoints.cs` | `GET /api/offers` – wyszukiwarka ofert |

## Jak dodać endpoint do istniejącej grupy

W `OffersEndpoints.cs`:

```csharp
group.MapGet("/{id:int}", GetOfferById).WithName("GetOfferById");
```

i prywatna metoda, która tylko woła handler z Application i zwraca `TypedResults.Ok(...)` albo `TypedResults.NotFound()`.

## Jak dodać nową grupę

1. Utwórz `AnalyticsEndpoints.cs` – statyczna klasa z metodą `MapAnalyticsEndpoints`.
2. W `Program.cs` dopisz `app.MapAnalyticsEndpoints();`.

Nazwa klasy musi kończyć się na `Endpoints`, a klasa musi być `static` – pilnują tego testy architektury.
