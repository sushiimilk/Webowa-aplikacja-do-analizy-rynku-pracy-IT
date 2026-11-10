# Domain/Users – dane użytkownika

| Plik | Co opisuje |
|---|---|
| `Cv.cs` | CV wgrane przez użytkownika |
| `CvSkill.cs` | Umiejętności wyciągnięte z CV |
| `FavoriteOffer.cs` | Oferta zapisana przez użytkownika jako ulubiona |

**Uwaga:** tu nie ma klasy „User”. Konto użytkownika (login, hasło) obsługuje ASP.NET Identity w `Infrastructure/Identity`, bo to szczegół techniczny. Encje w Domain znają tylko `UserId` – identyfikator użytkownika jako tekst.
