# Application/Cv – analiza CV (do zrobienia)

Tu trafią przypadki użycia dla CV, np.:

- `ExtractSkills/` – wyciągnięcie umiejętności z tekstu CV (na podstawie słownika `Skills`)
- `MatchCvToOffer/` – procentowe dopasowanie CV do wybranej oferty i lista brakujących umiejętności

Jak dodać: patrz README w `src/RynekIT.Application`, sekcja „Jak dodać nowy przypadek użycia”.

## Normalizacja umiejętności (ważne dla `ExtractSkills`)

CV i oferty muszą być normalizowane identycznie, inaczej dopasowanie po `SkillId` nie zadziała (np. `Node.js` w CV i `node` w ofercie). `ExtractSkills` ma używać `IAppDbContext.SkillAliases` i tej samej reguły co ETL ofert (`stg.usp_ProcessRawOffers`):

1. `lower` i `trim` nazwy,
2. obcięcie do 100 znaków,
3. jeśli wynik jest aliasem w `SkillAliases`, zamiana na nazwę kanoniczną skilla docelowego.

Opis przepływu i aliasów: [`docs/schemat-bazy.md`](../../../docs/schemat-bazy.md#słownik-technologii-i-aliasy).
