# .NetCoreApi

An API base for ASP.NET Core **3.1** project. :cake:

## Features
- Users and JWT token authorization
- Example REST CRUD-controller 
- Alternative GraphQL API
    - Subscriptions yet **unAuthorized**
- EF Core In-memory DB with bogus seed objects
- Docker with PostgreSQL dev setup.
    - Including pgAdmin or browsing
- Integration test base
- Unit test base

## Code Structure

- API under **/API** folder
- GraphQl API under **A/PI/GraphQL**
- Data schema and business logic under **/Libraries/FeatureLibrary** folder
- Common functionalities under **/Libraries/CorereLibrary** folder
- Unit and integration tests under **/Tests**

## How to spin me up
`docker-compose up`  

## Configuration
```
"DatabaseConfiguration": {
  "UseInMemory": true, // Use InMemory DB provider
  "ConnectionString": "pSQLConnString" // Postgre connection string if not in inMemory mode
},
"JWTConfiguration": {
  "Secret": "Dev-Secret-356178he9j20kle" // Token secret for JWT
}
```
