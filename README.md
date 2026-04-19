# DvdLibrary API

Det här projektet är en ASP.NET Core Web API-lösning i .NET 10 för en fiktiv DVD-ehandel. Lösningen följer Clean Architecture med fyra lager, CQRS via MediatR, EF Core mot SQL Server, DTO:er, FluentValidation, AutoMapper och JWT-baserad autentisering med rollerna `Admin` och `User`.

## Teknikstack

- .NET 10
- ASP.NET Core Web API med controllers
- Clean Architecture
- CQRS + MediatR
- Entity Framework Core + SQL Server / LocalDB
- Repository Pattern
- AutoMapper
- FluentValidation
- JWT Bearer Authentication
- Swagger

## Lagerstruktur

- `src/DvdLibrary.API`
  Ansvarar för controllers, middleware-liknande felhantering, Swagger och autentiseringskonfiguration.
- `src/DvdLibrary.Application`
  Innehåller DTO:er, commands, queries, handlers, validators, AutoMapper-profiler och pipeline behavior.
- `src/DvdLibrary.Domain`
  Innehåller entiteter, enum och repository-kontrakt.
- `src/DvdLibrary.Infrastructure`
  Innehåller `AppDbContext`, repositories, JWT-tjänst, lösenordshantering, seed-data och migrationer.

## Domänmodell

- `Genre`
  En genre kan ha många DVD-filmer.
- `DvdMovie`
  Varje DVD-film tillhör en genre.
- `AppUser`
  Enkel användarmodell för att demonstrera login, JWT och rollbaserad åtkomst.

## Kör projektet

1. Säkerställ att lokal SQL Server på `localhost` är tillgänglig.
2. Återskapa paket:

```powershell
dotnet restore DvdLibrary.slnx
```

3. Bygg lösningen:

```powershell
dotnet build DvdLibrary.slnx
```

4. Kör API:t:

```powershell
dotnet run --project src/DvdLibrary.API/DvdLibrary.API.csproj
```

5. Öppna Swagger i webbläsaren:

- `https://localhost:xxxx/swagger`
- eller OpenAPI-dokumentet via `https://localhost:xxxx/openapi/v1.json`

## Databas och migrationer

Connection string finns i:

- [appsettings.json](/E:/Plugg/HA/07.%20WEBB/Vecka%204/Inlämningsuppgift/src/DvdLibrary.API/appsettings.json)
- [appsettings.Development.json](/E:/Plugg/HA/07.%20WEBB/Vecka%204/Inlämningsuppgift/src/DvdLibrary.API/appsettings.Development.json)

Skapa migration:

```powershell
dotnet ef migrations add InitialCreate --project src/DvdLibrary.Infrastructure/DvdLibrary.Infrastructure.csproj --startup-project src/DvdLibrary.API/DvdLibrary.API.csproj --output-dir Persistence/Migrations
```

Uppdatera databasen:

```powershell
dotnet ef database update --project src/DvdLibrary.Infrastructure/DvdLibrary.Infrastructure.csproj --startup-project src/DvdLibrary.API/DvdLibrary.API.csproj
```

Migrationerna ligger i [src/DvdLibrary.Infrastructure/Persistence/Migrations](/E:/Plugg/HA/07.%20WEBB/Vecka%204/Inlämningsuppgift/src/DvdLibrary.Infrastructure/Persistence/Migrations).

## Endpoints

### Auth

- `POST /api/auth/login`

### Genres

- `GET /api/genres`

### DvdMovies

- `GET /api/dvdmovies`
- `GET /api/dvdmovies/{id}`
- `POST /api/dvdmovies`
- `PUT /api/dvdmovies/{id}`
- `DELETE /api/dvdmovies/{id}`

## JWT och roller

Login returnerar en JWT-token som innehåller användarnamn och roll.

Demokonton:

- `admin` / `Admin123!`
- `user` / `User123!`

Roller:

- `Admin`
  Har åtkomst till att skapa, uppdatera och ta bort DVD-filmer.
- `User`
  Kan logga in och använda token för skyddade endpoints om fler sådana läggs till.

Så testar du i Swagger:

1. Kör `POST /api/auth/login`.
2. Kopiera token från svaret.
3. Klicka på `Authorize` i Swagger.
4. Klistra in `Bearer {token}`.
5. Testa t.ex. `POST`, `PUT` eller `DELETE` på `DvdMovies`.

## Arkitektur i korthet

- Controllers är tunna och skickar allt via MediatR.
- Commands ändrar data, queries hämtar data.
- Application känner inte till Infrastructure.
- Domain är rent och innehåller bara kärnmodellen samt kontrakt.
- Infrastructure implementerar repository-kontrakten och databasåtkomsten.
- API:t returnerar DTO:er, aldrig entiteter direkt.

## Branch-strategi

Arbetet sker i `development`. `main` ska skyddas och endast uppdateras via Pull Request.

### Rekommenderad branch protection i GitHub UI

1. Gå till repositoryts `Settings`.
2. Öppna `Branches`.
3. Klicka `Add branch protection rule`.
4. Ange branch-namn `main`.
5. Aktivera `Require a pull request before merging`.
6. Aktivera `Block force pushes`.
7. Aktivera gärna krav på uppdaterad branch före merge.

## Verifierat i den här miljön

- `dotnet build DvdLibrary.slnx`
- `dotnet ef migrations add InitialCreate ...`
- `dotnet ef database update ...`

## Känd notering

Nuvarande AutoMapper-version ger en NuGet-säkerhetsvarning i buildutskriften. Funktionaliteten fungerar, men paketversionen bör uppgraderas när en patchad version finns tillgänglig för vald .NET 10-kedja.
