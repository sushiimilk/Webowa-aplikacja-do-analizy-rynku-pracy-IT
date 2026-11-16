# Persistence/Sql – skrypty SQL używane przez migracje

Procedury składowane i inne długie skrypty, które migracje tworzą w bazie. Pliki są osadzone w assembly (`EmbeddedResource`) i ładowane przez `SqlScripts.Load(...)`.

## Zasady

- **Pliku wypchniętego do repo nie zmieniamy.** Zmiana procedury to nowy plik z kolejnym numerem wersji (`usp_X.v3.sql`) i nowa migracja: `Up()` ładuje nową wersję, `Down()` poprzednią.
- Plik zawiera dokładnie jedną instrukcję `CREATE OR ALTER ...`, bez `GO`.
- Nazwę pliku dodaj jako stałą w `SqlScripts`.

## Pliki

| Plik | Co robi | Migracja |
|---|---|---|
| `usp_ProcessRawOffers.v1.sql` | ETL `stg.RawOffers` → `Offers`/`Skills` (tagi bez aliasów) | `AddProcessRawOffersProcedure` (Down dla `AddSkillCategoryAndAliases`) |
| `usp_ProcessRawOffers.v2.sql` | ETL z zamianą aliasów na skill docelowy i scalaniem duplikatów | `AddSkillCategoryAndAliases` |
| `usp_MergeSkillAliases.sql` | Przepina oferty i CV z duplikatów (skill o nazwie będącej aliasem) na skill docelowy i usuwa duplikaty | `AddSkillCategoryAndAliases` |
