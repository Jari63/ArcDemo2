# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
dotnet build                                    # Build solution
dotnet run --project src/ArcDemo2.AspireHost   # Run with Aspire (recommended - auto-provisions SQL Server container)
dotnet run --project src/ArcDemo2.Web           # Run web project only (requires LocalDB)
dotnet test                                     # Run tests

# EF Core migrations (run from repo root)
dotnet ef migrations add <Name> -p src/ArcDemo2.Web -s src/ArcDemo2.Web
dotnet ef database update -c AppDbContext -p src/ArcDemo2.Web -s src/ArcDemo2.Web
```

When running with Aspire, the app is available at https://localhost:57379. The API docs are at `/scalar`.

## Architecture

**Minimal Clean Architecture** — a single-project Vertical Slice Architecture (VSA). All code lives in `src/ArcDemo2.Web`.

Three .NET projects:
- `ArcDemo2.Web` — main application (all features, domain, infrastructure)
- `ArcDemo2.AspireHost` — Aspire orchestration host (wires up SQL Server container, Papercut SMTP)
- `ArcDemo2.ServiceDefaults` — shared OpenTelemetry/service discovery config

### Key Libraries

| Library | Role |
|---|---|
| **FastEndpoints** | API endpoints using REPR pattern — one endpoint class per operation |
| **Ardalis.Specification** | Repository queries via the Specification pattern |
| **Ardalis.Result** | Return type for operations (avoids exceptions for control flow) |
| **Ardalis.GuardClauses** | Input validation guards |
| **Vogen** | Strongly-typed Value Objects via source generation |
| **Mediator.Abstractions** | Optional in-process messaging for cross-cutting concerns |
| **Serilog** | Structured logging |
| **Entity Framework Core** | ORM — SQL Server in Aspire, LocalDB for standalone run |

### Project Layout (`src/ArcDemo2.Web/`)

```
Domain/                 # Aggregates and value objects
  CartAggregate/
  OrderAggregate/
  ProductAggregate/
  GuestUserAggregate/
Infrastructure/
  Data/
    AppDbContext.cs
    Config/             # EF Core entity type configurations
    Migrations/
    Queries/            # Read-side query services
    SeedData.cs
  Email/               # IEmailSender + MimeKit/Fake implementations
Endpoints/             # FastEndpoints classes (one per API operation)
  Cart/
  Order/
  Product/
Configurations/        # Service registration extensions (ServiceConfigs, LoggerConfigs, etc.)
Program.cs
```

### Adding a New Vertical Slice

1. Add domain entity to `Domain/<Feature>Aggregate/`
2. Add EF config to `Infrastructure/Data/Config/`
3. Add migration: `dotnet ef migrations add Add<Feature> -p src/ArcDemo2.Web -s src/ArcDemo2.Web`
4. Add FastEndpoints endpoint(s) to `Endpoints/<Feature>/`

### Azure Deployment

Uses Azure Developer CLI (`azd`). Infrastructure is defined in `infra/` (Bicep). Deploys to Azure Container Apps.

```bash
azd provision   # Provision infrastructure
azd deploy      # Deploy application
```

CI/CD runs via `.github/workflows/azure-dev.yml` — deploys `develop` → development env, `main` → production.
