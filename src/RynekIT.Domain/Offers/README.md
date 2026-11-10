# Domain/Offers – oferty pracy

| Plik | Co opisuje |
|---|---|
| `Offer.cs` | Jedna oferta pracy: tytuł, firma, lokalizacja, wynagrodzenie, skąd pochodzi |
| `Company.cs` | Firma publikująca oferty |
| `WorkMode.cs` | Tryb pracy: zdalnie / hybrydowo / stacjonarnie |
| `SalaryPeriod.cs` | Za jaki okres jest wynagrodzenie: godzina / miesiąc / rok |
| `OfferSkill.cs` | Tabela łącząca: która oferta wymaga której umiejętności |

**Ważne zasady** (szczegóły w `docs/schemat-bazy.md`):

- `SalaryMin` / `SalaryMax` puste (`null`) = ogłoszenie nie podaje wynagrodzenia. Nigdy nie wpisujemy `0`.
- `Source` + `SourceOfferId` razem identyfikują ofertę w serwisie źródłowym – dzięki temu nie mamy duplikatów.
- `SourceUrl` to link do oryginału. Musi być zawsze, bo wymagają tego warunki korzystania ze źródeł.
