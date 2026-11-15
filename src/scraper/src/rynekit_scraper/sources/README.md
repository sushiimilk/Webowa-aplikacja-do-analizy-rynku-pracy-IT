# sources – źródła ofert

Każde źródło (Remote OK, Adzuna, Himalayas...) to osobny plik z klasą dziedziczącą po `OfferSource` z `base.py`.

| Plik | Źródło |
|---|---|
| `remoteok.py` | [Remote OK](https://remoteok.com) – publiczne API `https://remoteok.com/api` (~100 najnowszych ofert) |

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

Przed dodaniem źródła sprawdź jego regulamin, limity zapytań i wymagania atrybucji. Remote OK wymaga przy każdej wyświetlanej ofercie linku do oryginału bez `rel="nofollow"` i podania „Remote OK” jako źródła; nie wolno używać ich logo.
