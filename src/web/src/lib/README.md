# web/src/lib – narzędzia

Kod pomocniczy bez wyglądu, używany przez całą aplikację.

| Plik | Co to |
|---|---|
| `api-client.ts` | `apiGet<T>(path, params)` – jedno miejsce, które woła backend. Dokleja `/api/`, buduje parametry zapytania, rzuca `ApiError`, gdy backend zwróci błąd |
| `use-async.ts` | Hook `useAsync` – pobiera dane i zwraca stan: `loading` / `error` / `success` |
| `format.ts` | Formatowanie po polsku: `formatNumber` (1 243), `formatPercent` (62%), `formatMoney` (95 000 USD) |
| `fake-request.ts` | `fakeRequest(data, ms)` – udaje zapytanie do backendu (mocki w `features/*/api/mock-data.ts`) |

Jeśli kiedyś trzeba będzie dodać np. token logowania do każdego zapytania – zmienia się tylko `api-client.ts`.
