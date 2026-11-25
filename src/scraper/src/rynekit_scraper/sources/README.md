# sources – źródła ofert

Każde źródło (Remote OK, Adzuna, Himalayas...) to osobny plik z klasą dziedziczącą po `OfferSource` z `base.py`.

| Plik | Źródło |
|---|---|
| `remoteok.py` | [Remote OK](https://remoteok.com) – publiczne API `https://remoteok.com/api` (~100 najnowszych ofert) |
| `solidjobs.py` | [SOLID.Jobs](https://solid.jobs/api-ofert-pracy) – publiczne API `https://solid.jobs/public-api/offers/IT` (~1400 ofert IT z widełkami i umiejętnościami; po 500 na stronę) |

## Jak dodać źródło

```python
from collections.abc import Iterable

from rynekit_scraper.models import RawOffer
from rynekit_scraper.sources.base import OfferSource


class RemoteOkSource(OfferSource):
    name = "remoteok"

    def fetch(self) -> Iterable[RawOffer]:
        ...  # pobierz dane z API i dla każdej oferty: yield RawOffer(...)
```

1. Plik w tym folderze (wzór: `remoteok.py`).
2. Zaimplementuj `fetch()` – ma zwracać (`yield`) obiekty `RawOffer`.
3. Dodaj źródło do listy `sources` w `main()` w `main.py` – to jedyna zmiana poza nowym plikiem.
4. Dodaj test w `tests/` – najlepiej na zapisanej przykładowej odpowiedzi API, żeby test nie łączył się z internetem.

Zasady:

- `name` jest częścią klucza w bazie (`Source`) i filtrem w ETL – po pierwszym uruchomieniu nie wolno go zmieniać.
- `source_offer_id` musi być stały dla tej samej oferty przy każdym pobraniu.
- `raw_json` to cała oferta bez obróbki – czyszczenie danych robi ETL.

Przed dodaniem źródła sprawdź jego regulamin, limity zapytań i wymagania atrybucji.

## Warunki źródeł

Obowiązują wszędzie, gdzie pokazujemy oferty (frontend, eksporty):

**Remote OK** – przy każdej ofercie link do oryginału bez `rel="nofollow"` i podanie „Remote OK” jako źródła; nie wolno używać ich logo.

**SOLID.Jobs** – API jest publiczne i pozwala pokazywać oferty we własnej aplikacji ([FAQ](https://solid.jobs/api-ofert-pracy)); zgodę potwierdzili też mailowo 24.11.2026 w odpowiedzi na naszą prośbę, więc obowiązuje to, co w niej obiecaliśmy:

- przy każdej ofercie nazwa „SOLID.Jobs” i link z pola `url` – kandydat aplikuje u nich,
- nie używamy logo SOLID.Jobs; logo firm (`companyLogoUrl`) też pomijamy (regulamin daje do nich prawo tylko SOLID),
- nie przekazujemy ofert dalej (inne serwisy, Google Jobs / dane strukturalne `JobPosting`, LinkedIn),
- nie zapisujemy danych osobowych rekruterów; z opisu pokazujemy krótki fragment,
- projekt niekomercyjny (bez reklam i opłat), na prośbę SOLID usuwamy dane i przestajemy pobierać,
- pokazujemy tylko aktualne oferty (`validTo`).

Szczegóły techniczne SOLID.Jobs:

- każde zapytanie wymaga parametru `campaign` – u nas `projektinzynierski-uken-rynekit` (stała w `main.py`). SOLID dokleja go do linków ofert, żeby widzieć ruch od nas – **nie zmieniać**, tak jak `name`,
- nagłówek `X-Api-Version: 1.0` przypina wersję API (bez niego dostajemy najnowszą, która może zmienić format),
- limit 300 zapytań/min na IP; pobieramy 3 strony z 1 s przerwy. Błędu `429` nie ponawiamy – przy 3 zapytaniach dziennie praktycznie nie wystąpi, a gdyby wystąpił, źródło zostanie zalogowane jako nieudane i pobierze się przy następnym uruchomieniu,
- API pomija pola z wartością `null` (np. `secondarySalary`, `companyLogoUrl`) – czytaj je przez `item.get(...)`.
