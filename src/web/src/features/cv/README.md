# features/cv – dopasowanie CV do ofert

Strona `/cv`. **Wynik analizy jest na razie udawany (mock)** – zawsze ten sam, niezależnie od pliku. Backend: patrz `Application/Cv/README.md`.

| Plik | Co robi |
|---|---|
| `types.ts` | `CvAnalysis` (rozpoznane umiejętności + dopasowane oferty), `CvOfferMatch` |
| `api/analyze-cv.ts` | `analyzeCv(file)` – docelowo wysyła plik `POST /api/cv/analyze` |
| `api/mock-data.ts` | Udawany wynik. Do usunięcia po podłączeniu backendu |
| `components/CvPage.tsx` | Strona. Stan analizy prowadzony ręcznie (`idle / loading / error / success`), bo startuje po kliknięciu, a nie po wejściu na stronę |
| `components/CvUploadForm.tsx` | Wybór pliku i walidacja (format, rozmiar) |
| `components/CvAnalysisResult.tsx` | Rozpoznane umiejętności i lista ofert z % dopasowania |
| `components/SkillTag.tsx` | Etykietka umiejętności (zwykła / „masz” / „brakuje”) |

`SkillTag` jest tu, bo na razie używa go tylko CV. Gdy będzie potrzebny w innej funkcji (np. na karcie oferty) – przenieś go do `components/ui/`.

Podłączenie backendu: dopisz `apiPost` do `lib/api-client.ts`, w `analyze-cv.ts` zamień mock na kod z komentarza `TODO`, usuń `mock-data.ts`.
