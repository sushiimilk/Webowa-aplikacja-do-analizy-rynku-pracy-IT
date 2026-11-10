# scraper/tests – testy scrapera

Testy w **pytest**. Plik musi zaczynać się od `test_`, funkcja testowa też od `test_`.

```bash
pytest          # wszystkie testy
pytest -k run   # tylko testy z "run" w nazwie
```

Zasada: testy **nie łączą się z internetem**. Zapisz przykładową odpowiedź API do pliku (np. `tests/data/remoteok.json`) i testuj parser na niej. Dzięki temu testy są szybkie i nie psują się, gdy serwis nie działa.
