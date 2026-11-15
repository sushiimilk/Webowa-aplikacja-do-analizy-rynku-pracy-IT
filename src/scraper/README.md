# scraper – pobieranie ofert (Python)

Osobny program w Pythonie. Pobiera oferty z serwisów źródłowych (API, strony WWW) i zapisuje je surowe do tabeli `stg.RawOffers` w bazie. Dalszym przetwarzaniem zajmuje się backend.

## Struktura (tzw. *src layout* – standard w Pythonie)

```
scraper/
├── pyproject.toml            ← zależności i ustawienia narzędzi (zamiast requirements.txt)
├── src/rynekit_scraper/      ← kod programu (pakiet Pythona)
│   ├── main.py               ← punkt startowy: źródła → zapis do bazy
│   ├── models.py             ← RawOffer – jak wygląda pobrana oferta
│   ├── db.py                 ← połączenie z bazą i zapis do stg.RawOffers
│   └── sources/              ← jeden plik = jedno źródło ofert
└── tests/                    ← testy (pytest), dane testowe w tests/data/
```

## Pierwsze uruchomienie (Windows)

```bash
cd src/scraper
python -m venv .venv
.venv\Scripts\activate
pip install -e ".[dev]"
```

`-e` instaluje pakiet „w trybie edycji” – zmiany w kodzie działają od razu. `[dev]` dodaje narzędzia: pytest i ruff.

## Uruchomienie scrapera

Scraper zapisuje do bazy, więc przed `rynekit-scraper`:

1. Baza działa i ma migracje (z katalogu głównego repo: `docker compose up -d`, potem `dotnet ef database update` – patrz główny README). Scraper nie tworzy tabel.
2. W głównym `.env` jest `MSSQL_SA_PASSWORD` (i opcjonalnie `MSSQL_PORT`, `SCRAPER_USER_AGENT`) – scraper sam znajduje ten plik, szukając od bieżącego katalogu w górę.

```bash
rynekit-scraper
```

Wynik: `Pobrano ofert: 99 (nowe: …, zaktualizowane: …)`. Ponowne uruchomienie nie tworzy duplikatów – znane oferty są aktualizowane (`MERGE`, patrz `docs/schemat-bazy.md`). Gdy źródło nie odpowiada, błąd trafia do logu, a pozostałe źródła działają dalej.

Scraper łączy się z `127.0.0.1`, a nie `localhost` – na części komputerów `localhost` wybiera IPv6 (`::1`), na którym kontener nie nasłuchuje, i połączenie kończy się timeoutem.

Testy (`pytest`) nie potrzebują ani bazy, ani internetu.

## Komendy

| Komenda | Co robi |
|---|---|
| `rynekit-scraper` | Uruchamia scraper |
| `pytest` | Uruchamia testy |
| `ruff check .` | Sprawdza styl i błędy (jak ESLint dla Pythona) |
| `ruff check . --fix` | Poprawia automatycznie, co się da |
