# Sandra Superprompt - Clean Architecture Web API (.NET 10, DVD Edition)

Du arbetar i en helt tom projektmapp och ska bygga en **inlämningsuppgift för skolan**: ett **ASP.NET Core Web API i .NET 10** som följer **Clean Architecture**, **CQRS**, **MediatR**, **Repository Pattern**, **Entity Framework Core** och **SQL Server**.

## Viktig kontext
Detta är en individuell skoluppgift. Kod får AI-assisteras, men studenten måste kunna:
- visa commit-historik som ser mänsklig och stegvis ut
- förklara koden muntligt efteråt
- lägga upp koden i publikt GitHub-repo

Därför gäller detta arbetssätt:
- Bygg **stegvis**, inte allt på en gång.
- Efter varje större delmoment: **commit + push**.
- Arbeta **endast i branch `development`**.
- `main` ska skyddas med branch protection och får **inte** få direkta pushar.
- Om något kräver merge till `main`, skapa instruktioner för PR-flöde, men fortsätt själv arbeta i `development`.
- Koden ska ha **generösa kommentarer på svenska**, särskilt i centrala filer.
- Lösningen ska vara **pedagogisk, ren och lätt att förklara**, inte överdesignad.

## Affärsdomän
Systemet ska hantera **DVD-filmer**, inte böcker.

Använd minst två entiteter med relation.
Förslag som ska användas:
- `DvdMovie`
- `Genre`

Relation:
- En `Genre` kan ha många `DvdMovie`
- Varje `DvdMovie` tillhör en `Genre`

Detta uppfyller kravet på minst 2 modeller och en 1-till-många-relation.

## Målbild för lösningen
Bygg en solution med dessa fyra projekt under `src/`:
- `DvdLibrary.API`
- `DvdLibrary.Application`
- `DvdLibrary.Domain`
- `DvdLibrary.Infrastructure`

Lägg även `README.md` i repo-roten.

## Tekniska krav som måste uppfyllas
### G-krav
- Publikt GitHub-repo med README
- Branch protection på `main` (inga direkta pushar, merge via PR)
- Minst 5 meningsfulla commits
- 4 lager: API, Application, Domain, Infrastructure
- Minst 2 entiteter/modeller
- Full CRUD för minst en modell (`DvdMovie`)
- Relation mellan entiteterna
- Commands och Queries separerade i Application
- MediatR används i alla controllers
- Repository-interface i Domain eller Application
- Repository-implementation i Infrastructure
- DbContext med SQL Server
- Migrations skapade och körda
- Swagger eller Scalar aktivt

### VG-krav som också ska implementeras
- DTOs används konsekvent, entiteter exponeras aldrig direkt
- AutoMapper-profiler i Application
- Minst ett MediatR Pipeline Behaviour, helst `ValidationBehavior`
- FluentValidation
- JWT-autentisering
- Login-endpoint som returnerar token
- Skyddade endpoints med `[Authorize]`
- RBAC med minst två roller: `Admin` och `User`

## Paketering och teknikval
Använd:
- .NET 10
- ASP.NET Core Web API
- Controllers (inte bara Minimal API)
- Entity Framework Core med SQL Server
- MediatR
- AutoMapper
- FluentValidation
- JWT Bearer Authentication
- Swagger

## Domänmodell - föreslagen miniminivå
### Genre
Fält:
- `Id`
- `Name`
- `Description`
- navigation collection till `DvdMovie`

### DvdMovie
Fält:
- `Id`
- `Title`
- `Director`
- `ReleaseYear`
- `DurationMinutes`
- `IsAvailable`
- `GenreId`
- navigation property till `Genre`

### User eller AppUser
För JWT/RBAC behövs användare. Håll det enkelt.
Skapa gärna en enkel användarmodell, t.ex. `AppUser`, för login-demo.
Fält kan vara:
- `Id`
- `Username`
- `PasswordHash`
- `Role`

Du behöver inte implementera full ASP.NET Identity om det gör lösningen onödigt tung för uppgiften. En enkel men begriplig JWT-lösning räcker om den är lätt att förklara.

## CRUD-krav
Minst `DvdMovie` ska ha full CRUD:
- `GET /api/dvdmovies`
- `GET /api/dvdmovies/{id}`
- `POST /api/dvdmovies`
- `PUT /api/dvdmovies/{id}`
- `DELETE /api/dvdmovies/{id}`

Lägg gärna även till enklare läs-endpoints för `Genre`.

## Strukturkrav i Application
Organisera Features tydligt, exempelvis:
- `Features/DvdMovies/Commands/CreateDvdMovie`
- `Features/DvdMovies/Commands/UpdateDvdMovie`
- `Features/DvdMovies/Commands/DeleteDvdMovie`
- `Features/DvdMovies/Queries/GetAllDvdMovies`
- `Features/DvdMovies/Queries/GetDvdMovieById`
- `Features/Genres/Queries/GetAllGenres`
- `Features/Auth/Commands/Login`

Varje feature ska vara uppdelad tydligt med:
- Command/Query
- Handler
- Validator där relevant
- DTOs där det passar

## Arkitekturprinciper som ska följas
- Controllers ska vara tunna
- MediatR används i alla controllers
- Application får inte känna till Infrastructure
- Domain ska vara ren och peka på inget yttre lager
- Infrastructure implementerar interfaces från Application/Domain
- DTOs returneras från API, inte entiteter
- Kommentarer på svenska i viktiga delar av koden

## GitHub- och branch-arbete
Du ska:
1. initiera git-repo
2. skapa GitHub-repo om det inte redan finns
3. pusha första commit
4. skapa branch `development`
5. växla till `development`
6. arbeta vidare endast där
7. konfigurera eller instruera branch protection på `main`

Om GitHub CLI finns, använd den gärna. Om branch protection inte går att sätta via CLI i miljön, skriv tydliga instruktioner i README eller separat notering för hur studenten snabbt gör det via GitHub UI.

## Commit-strategi
Du måste dela upp arbetet i meningsfulla commits. Sikta på fler än 5. Exempel:
1. `init solution, projects and git setup`
2. `add domain entities and relationships`
3. `add application contracts, dtos and mediatr features`
4. `implement infrastructure with dbcontext and repositories`
5. `wire dependency injection and controllers`
6. `add automapper and validation behavior`
7. `add jwt authentication and role-based authorization`
8. `add migrations, seed data and documentation`
9. `polish comments, swagger and readme`

Commita efter varje delmoment och pusha direkt.

## Arbetsordning du ska följa
### Steg 1 - Bootstrap
- skapa solution
- skapa `src/`
- skapa 4 projekt
- lägg till projekt i solution
- lägg till korrekta projektreferenser
- skapa git-repo
- skapa första commit
- skapa branch `development`
- pusha

### Steg 2 - Domain
- skapa entiteter
- definiera relationer
- skapa eventuella enums
- skapa ev. repository interfaces om du väljer Domain för dessa
- kommentera på svenska
- commit + push

### Steg 3 - Application
- skapa interfaces om de ska ligga här
- skapa DTOs
- skapa Commands och Queries
- skapa Handlers
- skapa Validators
- skapa AutoMapper-profiler
- skapa pipeline behavior
- kommentera på svenska
- commit + push

### Steg 4 - Infrastructure
- skapa `AppDbContext`
- konfigurera EF Core
- skapa repository-implementationer
- skapa JWT-tjänst om du placerar den här
- seed-data om lämpligt
- kommentera på svenska
- commit + push

### Steg 5 - API
- skapa controllers
- använd MediatR i alla controllers
- konfigurera Swagger
- konfigurera authentication/authorization
- lägg in appsettings för SQL Server och JWT
- kommentera på svenska
- commit + push

### Steg 6 - Databas
- skapa migrationer
- kör database update
- verifiera att databasen skapas
- commit + push

### Steg 7 - README och slutpolish
- skriv tydlig README
- förklara arkitekturen kortfattat
- lista endpoints
- beskriv hur JWT testas
- nämn branch protection och att arbete sker i `development`
- commit + push

## README ska innehålla
- projektbeskrivning
- teknikstack
- lagerstruktur
- instruktioner för att köra projektet
- migrations-kommandon
- hur man testar Swagger
- hur login/JWT fungerar
- roller (`Admin`, `User`)
- branch-strategi (`main` skyddad, arbete i `development`)

## Seed-data
Lägg gärna in seed-data för:
- några `Genre`
- några `DvdMovie`
- minst en `Admin` och en `User`

Login ska vara enkel att demonstrera.
Exempel på demo-konton kan dokumenteras i README.

## Kodkvalitet
Prioritera:
- begriplig kod
- små, tydliga filer
- rimliga namn
- pedagogik framför cleverness
- kommentarer på svenska där de hjälper studenten att förstå

Undvik:
- onödig enterprise-komplexitet
- att bygga 10 extra abstraktioner som inte behövs
- att exponera entiteter direkt från API
- att blanda SQL/EF-logik i controllers
- att hoppa över commit-steg

## Arbetsstil under körning
När du jobbar:
- skriv kort vad du tänker göra innan större steg
- skapa filer stegvis
- kör build ofta
- fixa fel direkt
- verifiera att projektet faktiskt startar
- verifiera att swagger visar endpoints
- verifiera att skyddade endpoints kräver JWT

## Viktigt om muntlig redovisning
Bygg lösningen så att studenten lätt kan förklara:
- varför 4 lager finns
- vad MediatR gör
- skillnaden mellan Command och Query
- varför repository-interface ligger i inre lager men implementation i Infrastructure
- varför DTO används
- hur JWT och roller fungerar

## Viktig slutleverans
Till sist ska repo innehålla:
- fungerande lösning
- migrations
- README
- meningsfull commit-historik
- branch `development` med allt arbete
- möjlighet att skapa PR mot `main`

## Ditt uppdrag nu
Utför arbetet från tom mapp till fungerande lösning enligt allt ovan.
Arbeta metodiskt, stegvis och commit-driven.
Kommentera centrala koddelar på svenska.
Optimera för att studenten ska kunna plugga på koden efteråt.
