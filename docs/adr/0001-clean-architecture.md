# 0001. Clean Architecture z vertical slices w warstwie Application

- **Status:** Przyjęta
- **Data:** 2026-11-10

## Kontekst

Aplikacja ma kilka niezależnych obszarów (oferty, analityka, CV, konta użytkowników), pobiera dane z zewnętrznych źródeł i będzie rozwijana równolegle przez trzy osoby. Potrzebujemy struktury, w której:

- logikę da się testować bez bazy danych,
- osoby nie wchodzą sobie w drogę w tych samych plikach,
- łatwo wyjaśnić i obronić podział kodu.

## Decyzja

Backend dzielimy na cztery projekty: `Domain`, `Application`, `Infrastructure`, `Api`, z zależnościami skierowanymi tylko do środka (Clean Architecture).

Wewnątrz `Application` kod grupujemy według przypadków użycia (vertical slices): jeden folder = jedna operacja (zapytanie, handler, wynik).

Handlery to zwykłe klasy rejestrowane w DI – bez biblioteki MediatR.

Zasady zależności sprawdzają automatyczne testy architektury (NetArchTest).

## Konsekwencje

- Więcej projektów i plików niż w prostym podejściu „wszystko w jednym projekcie”.
- Logika jest testowalna na bazie w pamięci.
- Zmiana technologii (np. bazy) dotyczy tylko `Infrastructure`.
- Każda nowa funkcja ma oczywiste miejsce w strukturze.
