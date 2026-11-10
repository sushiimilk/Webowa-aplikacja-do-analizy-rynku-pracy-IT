# web/src/lib – narzędzia

Kod pomocniczy bez wyglądu, używany przez całą aplikację.

| Plik | Co to |
|---|---|
| `api-client.ts` | `apiGet<T>(path, params)` – jedno miejsce, które woła backend. Dokleja `/api/`, buduje parametry zapytania, rzuca `ApiError`, gdy backend zwróci błąd |
| `use-async.ts` | Hook `useAsync` – pobiera dane i zwraca stan: `loading` / `error` / `success` |

Jeśli kiedyś trzeba będzie dodać np. token logowania do każdego zapytania – zmienia się tylko `api-client.ts`.
