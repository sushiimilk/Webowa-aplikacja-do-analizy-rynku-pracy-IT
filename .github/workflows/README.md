# .github/workflows – automatyczne sprawdzanie kodu (CI)

`ci.yml` uruchamia się na GitHubie przy każdym Pull Requeście i każdym pushu do `main`. Sprawdza trzy części projektu równolegle:

| Zadanie | Co robi |
|---|---|
| Backend (.NET) | build całej solucji + wszystkie testy |
| Frontend (React) | `npm ci`, lint, typy, build |
| Scraper (Python) | instalacja, `ruff`, `pytest` |

Wynik widać w PR jako zielony ✓ albo czerwony ✗. **Nie mergujemy PR z czerwonym CI.**

Jeśli CI pada: kliknij *Details* przy czerwonym zadaniu, znajdź pierwszy błąd w logu i odtwórz go lokalnie tą samą komendą.
