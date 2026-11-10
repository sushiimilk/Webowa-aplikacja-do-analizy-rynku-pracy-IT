# Persistence/Staging – tabele robocze dla scrapera

Scraper (Python) nie zapisuje ofert od razu do tabeli `Offers`. Najpierw wrzuca **surowe dane** (cały JSON z API źródła) do tabeli `stg.RawOffers`. Dopiero później osobny proces je czyści, normalizuje i przenosi do `Offers`.

Dlaczego tak? Jeśli parser się pomyli, surowe dane nadal są w bazie i można je przetworzyć jeszcze raz.

| Plik | Co to |
|---|---|
| `RawOffer.cs` | Jeden wiersz surowych danych z serwisu źródłowego |

`ProcessedAt = null` oznacza „jeszcze nie przetworzone”.

Kontrakt dla scrapera (jak zapisywać, żeby nie było duplikatów): `docs/schemat-bazy.md`.

Ta klasa jest tutaj, a nie w Domain, bo to techniczny szczegół importu danych, a nie pojęcie biznesowe.
