# ArsenalApi — C# ASP.NET Core 8 Web API

Batman/Wayne Enterprises-themed inventory management REST API. Backend for the [Arsenal Manager](../arsenal_manager) Flutter app.

---

## Features

- Full CRUD for gear items (`/api/gear`)
- Divisions list (`/api/divisions`)
- Dashboard stats aggregation (`/api/dashboard/stats`)
- Seeded with 18 Batman-themed gear items across 6 divisions
- Swagger UI at `/swagger`

---

## Tech Stack

| Package | Version | Purpose |
|---|---|---|
| ASP.NET Core | 8.0 | Web framework |
| Entity Framework Core | 8.0.4 | ORM |
| Npgsql.EntityFrameworkCore.PostgreSQL | 8.0.4 | PostgreSQL provider |
| Microsoft.EntityFrameworkCore.Design | 8.0.4 | EF CLI tooling |
| Swashbuckle.AspNetCore | 6.6.2 | Swagger/OpenAPI |

---

## Project Structure

```
ArsenalApi/
├── Program.cs                   # App entry, DI registration, CORS, Swagger, routing
├── appsettings.json             # Non-secret defaults (connection string comes from User Secrets / env)
├── Models/
│   ├── Division.cs              # Division entity
│   └── GearItem.cs              # GearItem entity
├── DTOs/
│   ├── GearItemDto.cs           # Response DTO (DivisionName flattened)
│   ├── CreateGearItemDto.cs     # Create request body
│   └── UpdateGearItemDto.cs     # Update request body
├── Data/
│   └── ArsenalDbContext.cs      # EF DbContext + seed data
├── Controllers/
│   ├── GearController.cs        # Full CRUD — /api/gear
│   ├── DivisionsController.cs   # GET /api/divisions
│   └── DashboardController.cs   # GET /api/dashboard/stats
└── Migrations/
    └── ...                      # EF Core migration files
```

---

## Data Model

### Division
| Field | Type |
|---|---|
| Id | int (PK) |
| Name | string |

### GearItem
| Field | Type |
|---|---|
| Id | int (PK) |
| Name | string |
| DivisionId | int (FK) |
| Quantity | int |
| Notes | string? |
| CreatedAt | DateTime (UTC) |
| UpdatedAt | DateTime (UTC) |

---

## Seed Data

### Divisions
| Id | Name |
|---|---|
| 1 | Gadgets |
| 2 | Vehicles |
| 3 | Weaponry |
| 4 | Surveillance |
| 5 | Medical |
| 6 | Tactical |

### Gear Items
| Name | Division | Qty | Status |
|---|---|---|---|
| Batarangs | Gadgets | 12 | Low |
| Grapple Gun | Gadgets | 7 | Low |
| Remote Batarang | Gadgets | 23 | In Stock |
| Batmobile | Vehicles | 2 | Critical |
| Batwing | Vehicles | 1 | Critical |
| Batcycle | Vehicles | 4 | Critical |
| Explosive Gel | Weaponry | 8 | Low |
| Smoke Bombs | Weaponry | 45 | In Stock |
| EMP Device | Weaponry | 3 | Critical |
| Bat-Tracker | Surveillance | 31 | In Stock |
| Micro Camera | Surveillance | 18 | In Stock |
| Sonar Device | Surveillance | 5 | Critical |
| Trauma Kit | Medical | 14 | Low |
| Antidote Vials | Medical | 6 | Low |
| Nanite Injector | Medical | 19 | In Stock |
| Batsuit | Tactical | 2 | Critical |
| Kevlar Cape | Tactical | 22 | In Stock |
| Wayne Tech Armor | Tactical | 9 | Low |

**Stock thresholds:** Critical ≤ 5 | Low 6–15 | In Stock > 15

---

## API Endpoints

### Gear — `/api/gear`

| Method | Route | Body | Description |
|---|---|---|---|
| GET | `/api/gear` | — | All gear items. Optional `?search=` query param |
| GET | `/api/gear/{id}` | — | Single gear item |
| POST | `/api/gear` | `CreateGearItemDto` | Create new gear item |
| PUT | `/api/gear/{id}` | `UpdateGearItemDto` | Update gear item (sets UpdatedAt) |
| DELETE | `/api/gear/{id}` | — | Delete gear item |

**CreateGearItemDto / UpdateGearItemDto:**
```json
{
  "name": "Batarang",
  "divisionId": 1,
  "quantity": 12,
  "notes": "Standard issue"
}
```

**GearItemDto (response):**
```json
{
  "id": 1,
  "name": "Batarangs",
  "divisionId": 1,
  "divisionName": "Gadgets",
  "quantity": 12,
  "notes": "Standard issue",
  "createdAt": "2024-01-01T00:00:00Z",
  "updatedAt": "2024-01-01T00:00:00Z"
}
```

### Divisions — `/api/divisions`

| Method | Route | Description |
|---|---|---|
| GET | `/api/divisions` | All divisions `[{ id, name }]` |

### Dashboard — `/api/dashboard`

| Method | Route | Description |
|---|---|---|
| GET | `/api/dashboard/stats` | Aggregate counts |

**Stats response:**
```json
{
  "totalGear": 18,
  "criticalCount": 6,
  "lowStockCount": 6,
  "inStockCount": 6
}
```

---

## Setup & Running

### Prerequisites
- .NET 8 SDK
- PostgreSQL running locally

### 1. Configure connection string (keep secrets out of Git)

Do **not** put passwords in `appsettings.json`. The project uses [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for local development (stored on your machine, outside the repo).

From the project directory:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=arsenal_db;Username=postgres;Password=YOUR_PASSWORD"
```

**PowerShell (production / CI):** set an environment variable instead (double underscores for nested keys):

```powershell
$env:ConnectionStrings__DefaultConnection = "Host=...;Password=..."
```

If a database password was ever committed to version control, rotate it in PostgreSQL and update your secret.

**Optional:** copy `appsettings.json` to `appsettings.Development.local.json`, add `ConnectionStrings` there, and keep that file untracked (it is listed in `.gitignore`). User Secrets are usually simpler.

### 2. Apply migrations (creates DB + seeds data)
```bash
dotnet ef database update
```

### 3. Run
```bash
dotnet run
```

API runs at `http://localhost:5000`. Swagger UI at `http://localhost:5000/swagger`.

### Regenerate migrations (if model changes)
```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

---

## CORS

Configured to allow all origins, methods, and headers for development. Restrict this before deploying to production.

---

## Test Checklist

- [ ] `dotnet run` → Swagger at `/swagger` shows all endpoints
- [ ] GET `/api/gear` returns 18 seeded items
- [ ] POST `/api/gear` via Swagger → item appears in GET response
- [ ] PUT `/api/gear/{id}` updates item and sets `updatedAt`
- [ ] DELETE `/api/gear/{id}` removes item
- [ ] GET `/api/dashboard/stats` returns correct counts
- [ ] `?search=bat` on GET `/api/gear` filters results
