# Checklista mot inlämningsuppgiften

Den här checklistan ska bockas av under arbetet.

## G - Godkänt
### GitHub och struktur
- [ ] Publikt GitHub-repo finns
- [ ] README finns i repo-roten
- [ ] `main` har branch protection eller tydlig instruktion för att aktivera den
- [ ] Minst 5 meningsfulla commits finns
- [ ] Projektet har 4 lager: API, Application, Domain, Infrastructure

### Modeller och CRUD
- [ ] Minst 2 entiteter finns
- [ ] Full CRUD finns för `DvdMovie`
- [ ] Relation finns mellan entiteterna

### CQRS + MediatR
- [ ] Commands och Queries är separerade i Application
- [ ] Handlers ligger i rätt mappar
- [ ] Alla controllers använder MediatR

### Repository Pattern + EF Core
- [ ] Repository-interface finns i Domain eller Application
- [ ] Implementation finns i Infrastructure
- [ ] `DbContext` är kopplad till SQL Server
- [ ] Migrationer har skapats
- [ ] Databasen kan skapas via migrations

### Dokumentation
- [ ] Swagger eller Scalar fungerar
- [ ] Endpoints går att testa
- [ ] README förklarar projektet

## VG - Väl godkänt
- [ ] DTOs används konsekvent
- [ ] Entiteter exponeras aldrig direkt från API
- [ ] AutoMapper-profiler finns i Application
- [ ] Pipeline Behavior finns
- [ ] FluentValidation används
- [ ] JWT-login-endpoint finns
- [ ] JWT-token returneras vid login
- [ ] Skyddade endpoints kräver giltig token
- [ ] Minst 2 roller finns: `Admin`, `User`
- [ ] Roller används i authorization

## Muntlig förklarbarhet
Studenten ska lätt kunna förklara:
- [ ] vad varje lager gör
- [ ] hur en GET-request går genom systemet
- [ ] skillnaden mellan Command och Query
- [ ] varför MediatR används
- [ ] varför repository-mönstret används
- [ ] varför DTOs används
- [ ] hur JWT + roller fungerar

## Förslag på demo-flöde
1. Visa solutionens 4 projekt
2. Visa en entity i Domain
3. Visa en Query + Handler i Application
4. Visa repository implementation i Infrastructure
5. Visa controller i API som använder MediatR
6. Visa Swagger
7. Login och få JWT-token
8. Testa skyddad endpoint med Admin/User

## Uppgiftsstöd
Skolans uppgift kräver bland annat:
- Clean Architecture i 4 lager
- CQRS med MediatR
- Repository Pattern
- SQL Server via EF Core
- branch protection på main
- minst 2 modeller och relation
- full CRUD
- dokumentation
- för VG även DTOs, AutoMapper, pipeline behavior, JWT och RBAC
