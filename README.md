# Vehicles list app
## Summary
Small .NET MVC learning project with the main focus on working out an integrated UI with Microsoft Identity for authorization.
It's intended to have all CRUD operations, and possibly in a near future will also include pagination.

## Dependencies
1. Microsoft.AspNet.Identity.Core
2. Microsoft.AspNetCore.Identity.EntityFrameworkCore
3. Microsoft.EntityFrameworkCore
4. Microsoft.EntityFrameworkCore.Tools
5. Npgsql.EntityFrameworkCore.PostgreSQL

## Run the project
1. The db definition is in the docker-compose file. The Webapp is commented our because I'm running locally. If you want to run the app in a container uncomment the docker-compose.yml and in appsettings.json change the ConnectionString for Docker
```bash
docker-compose up -d
```

2. Migrate the both contexts:
```bash
dotnet update database --context AppDbContext
dotnet update database --context AuthDbContext
# or in PM Console at Visual Studio
Update-Database -Context AppDbContext
Update-Database -Context AuthDbContext
```

3. Start the app with:
```bash
dotnet run
```