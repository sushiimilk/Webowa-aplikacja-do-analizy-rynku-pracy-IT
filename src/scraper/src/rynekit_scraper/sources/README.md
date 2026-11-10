# sources – źródła ofert

Każde źródło (Remote OK, Adzuna, Himalayas...) to osobny plik z klasą dziedziczącą po `OfferSource` z `base.py`.

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

1. Plik `remoteok.py` w tym folderze.
2. Zaimplementuj `fetch()` – ma zwracać (`yield`) obiekty `RawOffer`.
3. Dodaj źródło do listy w `main.py`.
4. Dodaj test w `tests/` – najlepiej na zapisanej przykładowej odpowiedzi API, żeby test nie łączył się z internetem.

Przed dodaniem źródła sprawdź jego regulamin, limity zapytań i wymagania atrybucji.
