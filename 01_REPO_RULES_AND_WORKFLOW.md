# Repo-regler och arbetsflöde för Sandra

Detta dokument styr hur projektet ska byggas och versionshanteras.

## Grenstrategi
- `main` är skyddad och får inte användas för direktarbete.
- Allt arbete sker i `development`.
- Om något ska in i `main`, ska det ske via Pull Request.

## Branch protection
Målet är att följande ska gälla på `main`:
- inga direkta pushar
- PR krävs för merge
- gärna krav på uppdaterad branch innan merge

Om automatisering inte är möjlig i miljön:
- dokumentera exakt hur användaren sätter branch protection i GitHub UI
- fortsätt själv ändå att arbeta i `development`

## Commit-regler
Commits ska vara:
- små till medelstora
- meningsfulla
- skrivna på engelska eller svenska, men tydliga
- aldrig slarviga som `fix`, `asdf`, `misc`

## Minsta acceptabla commit-plan
1. init solution, projects and git setup
2. add domain entities and relationships
3. add application cqrs structure and dtos
4. implement infrastructure repositories and dbcontext
5. wire api controllers and dependency injection
6. add automapper and validation behavior
7. add jwt authentication and role-based authorization
8. add migrations and seed data
9. update readme and polish project

## Arbetsdisciplin
Efter varje större delmoment:
1. kör build
2. fixa ev. fel
3. commit
4. push

## Kvalitetskontroll efter varje steg
Kontrollera alltid:
- bygger solutionen?
- finns rimliga kommentarer på svenska i nyckelfiler?
- ligger filerna i rätt lager?
- har inget yttre lager läckt in i Domain/Application?
- följer controllers MediatR-spåret?

## Kommentarstil i koden
Kommentarer ska:
- vara på svenska
- förklara varför något finns
- vara extra tydliga i `Program.cs`, handlers, repository-implementationer, `DbContext`, auth-delar och pipeline behavior

Undvik kommentarer som bara upprepar exakt vad koden redan säger.
Bra kommentarer förklarar syftet.

## Viktig princip
Bygg inte flashigt. Bygg begripligt.
Det här projektet ska tåla att studenten får frågor om varje lager.
