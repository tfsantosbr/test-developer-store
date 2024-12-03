# Dev Store API

## Running API Locally

You will need [.NET CLI](https://dotnet.microsoft.com/en-us/download) to run this commands

```bash
dotnet restore
dotnet build
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

## Running Docker Infrastructure Dependencies

You will need [Docker Desktop](https://docs.docker.com/desktop/install/windows-install/) to run this commands

```bash
docker-compose up -d
```

## Migrations

You will need [.NET EF Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) to run this commands

```bash
# add migration
dotnet ef migrations add <MIGRATION_NAME> -p src/Ambev.DeveloperEvaluation.ORM -s src/Ambev.DeveloperEvaluation.WebApi -o DefaultContext

# remove migration
dotnet ef migrations remove -p src/Ambev.DeveloperEvaluation.ORM -c DefaultContext -s src/Ambev.DeveloperEvaluation.WebApi

# update database
dotnet ef database update -p src/Ambev.DeveloperEvaluation.ORM -c DefaultContext -s src/Ambev.DeveloperEvaluation.WebApi

# generate scripts for manual database update
dotnet ef migrations script -p src/Ambev.DeveloperEvaluation.ORM -c DefaultContext -s src/Ambev.DeveloperEvaluation.WebApi -o ./scripts/migrations.sql
```
