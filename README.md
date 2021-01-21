# .NetCoreApi

An API base for ASP.NET Core **3.1** project. :cake:

## Features
- Users and JWT token authorization
- Example REST CRUD-controller 
- Alternative GraphQL API
    - Subscriptions however **unAuthorized**
- EF Core In-memory DB with bogus seed objects
- Docker with PostgreSQL dev setup.
    - Including pgAdmin for browsing
- Integration test base
- Unit test base

## Code Structure

- API under [`API`](./API)
- GraphQL API under [`GraphQL`](./API/GraphQL)
- Data schema and business logic under [`FeatureLibrary`](./Libraries/FeatureLibrary)
- Common functionalities under [`CoreLibrary`](./Libraries/CoreLibrary)
- Unit and integration tests under [`Tests`](./Tests)

## How to spin me up
> ```docker-compose up```

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
