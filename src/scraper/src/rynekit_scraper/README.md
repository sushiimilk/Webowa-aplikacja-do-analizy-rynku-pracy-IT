# rynekit_scraper – kod scrapera

| Plik / folder | Co to |
|---|---|
| `main.py` | Start programu: `run(sources, save)` przechodzi po wszystkich źródłach i każdą ofertę przekazuje do `save`; `main()` łączy to z bazą |
| `models.py` | `RawOffer` – pobrana oferta: źródło, id w źródle, link, surowy JSON |
| `db.py` | `connect()` i `RawOfferWriter` – zapis do `stg.RawOffers` (upsert `MERGE`, patrz `docs/schemat-bazy.md`) |
| `sources/` | Implementacje źródeł danych |
| `__init__.py` | Oznacza folder jako pakiet Pythona (może być pusty) |
