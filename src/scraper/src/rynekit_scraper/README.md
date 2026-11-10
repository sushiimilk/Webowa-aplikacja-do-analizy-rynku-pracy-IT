# rynekit_scraper – kod scrapera

| Plik / folder | Co to |
|---|---|
| `main.py` | Start programu: przechodzi po wszystkich źródłach i zbiera oferty |
| `models.py` | `RawOffer` – pobrana oferta: źródło, id w źródle, link, surowy JSON |
| `sources/` | Implementacje źródeł danych |
| `__init__.py` | Oznacza folder jako pakiet Pythona (może być pusty) |

Planowane: `db.py` – zapis do `stg.RawOffers` (upsert, patrz `docs/schemat-bazy.md`).
