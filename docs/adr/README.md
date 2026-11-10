# docs/adr – decyzje architektoniczne (ADR)

ADR (*Architecture Decision Record*) to krótka notatka o jednej ważnej decyzji technicznej: jaki był problem, co wybraliśmy, jakie są konsekwencje.

Po co? Za pół roku nikt nie pamięta, *dlaczego* wybraliśmy SQL Server zamiast PostgreSQL. Na obronie pracy też padnie takie pytanie.

## Jak dodać ADR

1. Skopiuj `0001-clean-architecture.md` jako kolejny numer, np. `0002-ms-sql-server.md`.
2. Wypełnij sekcje: Kontekst, Decyzja, Konsekwencje.
3. Dodaj w tym samym PR, co zmianę, której dotyczy.

ADR-ów nie usuwamy. Jeśli decyzja się zmieni – piszemy nowy ADR, a w starym zmieniamy status na „Zastąpiony przez 000X”.

| Nr | Decyzja | Status |
|---|---|---|
| 0001 | Clean Architecture z vertical slices | Przyjęta |
