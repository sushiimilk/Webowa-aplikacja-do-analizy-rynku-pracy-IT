# Api/Properties

`launchSettings.json` – profile uruchomieniowe używane przez Visual Studio i `dotnet run`:

- `https` – backend na https://localhost:7119 (+ http 5051). **Tego używamy na co dzień.**
- `http` – tylko http://localhost:5051.

Oba ustawiają środowisko `Development` (wczytywany jest `appsettings.Development.json` i user-secrets) i automatycznie uruchamiają frontend.

Ten plik dotyczy tylko uruchamiania lokalnego – na serwerze nie jest używany.
