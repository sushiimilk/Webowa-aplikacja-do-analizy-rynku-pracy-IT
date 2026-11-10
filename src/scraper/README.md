# scraper – pobieranie ofert (Python)

Osobny program w Pythonie. Pobiera oferty z serwisów źródłowych (API, strony WWW) i zapisuje je surowe do tabeli `stg.RawOffers` w bazie. Dalszym przetwarzaniem zajmuje się backend.

## Struktura (tzw. *src layout* – standard w Pythonie)

```
scraper/
├── pyproject.toml            ← zależności i ustawienia narzędzi (zamiast requirements.txt)
├── src/rynekit_scraper/      ← kod programu (pakiet Pythona)
│   ├── main.py               ← punkt startowy
│   ├── models.py             ← RawOffer – jak wygląda pobrana oferta
│   └── sources/              ← jeden plik = jedno źródło ofert
└── tests/                    ← testy (pytest)
```

## Pierwsze uruchomienie (Windows)

```bash
cd src/scraper
python -m venv .venv
.venv\Scripts\activate
pip install -e ".[dev]"
```

`-e` instaluje pakiet „w trybie edycji” – zmiany w kodzie działają od razu. `[dev]` dodaje narzędzia: pytest i ruff.

## Komendy

| Komenda | Co robi |
|---|---|
| `rynekit-scraper` | Uruchamia scraper |
| `pytest` | Uruchamia testy |
| `ruff check .` | Sprawdza styl i błędy (jak ESLint dla Pythona) |
| `ruff check . --fix` | Poprawia automatycznie, co się da |
