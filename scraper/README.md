# Scraper – pobieranie ofert pracy

Moduł w Pythonie odpowiedzialny za pobieranie ofert z zewnętrznych źródeł
(REST API, Scrapy, Playwright) i zapis do bazy MS SQL.

- `sources/` – osobny moduł dla każdego źródła (np. Adzuna, Himalayas, Remote OK, CBOP)

```bash
python -m venv .venv
.venv\Scripts\activate
pip install -r requirements.txt
```
