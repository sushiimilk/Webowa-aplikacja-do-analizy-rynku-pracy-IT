# features/analytics – statystyki rynku

Strona `/statystyki`. **Dane są na razie udawane (mock)** – backend nie ma jeszcze endpointów (patrz `Application/Analytics/README.md`).

| Plik | Co robi |
|---|---|
| `types.ts` | `MarketSummary` (liczba ofert, % zdalnych, mediana płac), `SkillDemand` (popularność technologii) |
| `api/get-market-summary.ts` | `getMarketSummary()` – docelowo `GET /api/analytics/summary` |
| `api/get-skill-demand.ts` | `getSkillDemand()` – docelowo `GET /api/analytics/skills` |
| `api/mock-data.ts` | Udawane dane. Do usunięcia po podłączeniu backendu |
| `components/AnalyticsPage.tsx` | Strona: dwie niezależne sekcje, każda z własnym ładowaniem i błędem |
| `components/SummaryTiles.tsx` | Kafelki ze wskaźnikami |
| `components/SkillDemandChart.tsx` | Wykres słupkowy w samym CSS |

Podłączenie backendu: w obu plikach `api/get-*.ts` zamień `fakeRequest(...)` na `apiGet(...)` z komentarza `TODO` i usuń `mock-data.ts`.
