# Entity Framework Core Partition POC

## Setup

Configure connection string for _Endpoints_ project:

```pwsh
dotnet user-secrets set "ConnectionStrings:PartitionPocContext" "..."  --project .\src\EntityFrameworkCorePartitionPoC.Endpoints\
```

_Note_: For local development use `Server=(localdb)\MSSQLLocalDB;Database=PartitionPoC;Trusted_Connection=True;MultipleActiveResultSets=true`.

Update database:

```pwsh
dotnet ef database update --project .\src\EntityFrameworkCorePartitionPoC.Infrastructure\ --startup-project .\src\EntityFrameworkCorePartitionPoC.Endpoints\
```

## Test

Tests are written using [https://testcontainers.com/](Testcontainers). Make sure [https://www.docker.com/](Docker) is running before executing:

```pwsh
dotnet test
```

## Develop

Add code, then create a migration and apply it:

```pwsh
dotnet ef migrations add MyNewMigration --project .\src\EntityFrameworkCorePartitionPoC.Infrastructure\ --startup-project .\src\EntityFrameworkCorePartitionPoC.Endpoints\

dotnet ef database update --project .\src\EntityFrameworkCorePartitionPoC.Infrastructure\ --startup-project .\src\EntityFrameworkCorePartitionPoC.Endpoints\
```
