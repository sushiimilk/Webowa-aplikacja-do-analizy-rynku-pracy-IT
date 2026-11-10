# src – kod aplikacji

Tu jest cały kod, który trafia do działającej aplikacji. Testy są osobno, w folderze `tests/` w katalogu głównym.

| Folder | Co to jest | Język |
|---|---|---|
| `RynekIT.Domain` | Encje biznesowe: oferta, firma, umiejętność, CV | C# |
| `RynekIT.Application` | Przypadki użycia: „wyszukaj oferty”, „policz dopasowanie CV” | C# |
| `RynekIT.Infrastructure` | Baza danych, logowanie użytkowników, inne „techniczne” rzeczy | C# |
| `RynekIT.Api` | Punkt wejścia aplikacji: adresy HTTP (`/api/...`) | C# |
| `web` | Frontend, czyli to, co widzi użytkownik w przeglądarce | TypeScript + React |
| `scraper` | Program pobierający oferty z internetu do bazy | Python |

## Jak to się łączy (Clean Architecture)

Backend jest podzielony na 4 warstwy, ułożone jak cebula:

```
        Api  ──────────►  Application  ──────────►  Domain
         │                    ▲
         └──►  Infrastructure ┘
```

Strzałka znaczy „korzysta z” / „wie o”. Najważniejsza zasada: **strzałki idą tylko do środka**.

- `Domain` nie wie o niczym innym. To czysty opis świata: co to jest oferta, co to jest firma.
- `Application` wie o `Domain`. Opisuje, *co* aplikacja robi, ale nie *jak* technicznie (nie wie, że baza to SQL Server).
- `Infrastructure` dostarcza szczegóły techniczne, np. prawdziwe połączenie z bazą.
- `Api` skleja wszystko razem i wystawia adresy HTTP.

Dlaczego tak? Bo możesz zmienić bazę danych albo framework webowy i nie ruszasz logiki biznesowej. Testy logiki nie potrzebują prawdziwej bazy.

Tych zasad pilnują automatyczne testy w `tests/RynekIT.ArchitectureTests`. Jeśli je złamiesz, CI zrobi się czerwone.
