# features/system-status – czy backend działa

Najprostsza możliwa funkcja – dobra do zrozumienia przepływu frontend → backend.

| Plik | Co robi |
|---|---|
| `api/get-health.ts` | Woła `GET /api/health` |
| `components/BackendStatus.tsx` | Pokazuje „ok” albo „brak połączenia z API” |

Jeśli widzisz „brak połączenia z API” – backend nie działa albo nie łączy się z bazą.
