# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run the app
dotnet run

# Build
dotnet build

# Apply pending migrations
dotnet ef database update

# Add a new migration
dotnet ef migrations add <MigrationName>

# Remove last migration (if not applied)
dotnet ef migrations remove
```

The app runs at `http://localhost:5257` (HTTP) or `https://localhost:7286` (HTTPS).
OpenAPI docs available at `/openapi/v1.json` in Development.

## Architecture

This is an **ASP.NET Core Web API** project targeting **.NET 10** with a minimal MVC structure:

- **`Program.cs`** — App entry point. Registers services (controllers, OpenAPI, EF Core) and configures middleware pipeline.
- **`Models/`** — EF Core entity classes with `[Key]`, `[Required]` data annotations.
- **`Data/AppDbContext.cs`** — EF Core `DbContext`. Add new `DbSet<T>` properties here when creating new entities.
- **`Controllers/`** — API controllers using `[ApiController]` + `[Route("[controller]")]`. Route is derived from controller class name.
- **`Migrations/`** — Auto-generated EF Core migration files. Do not edit manually.

**Database:** SQLite (`prof.db` in project root), connection string in `appsettings.json`.

## Conventions

- Namespace root is `Web` (matches project name in `Web.csproj`)
- Controllers inherit from `ControllerBase` (not `Controller` — no view support needed)
- Nullable reference types are enabled (`<Nullable>enable</Nullable>`)
