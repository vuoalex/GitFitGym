# GitFitGym 🏋️

A gym management console application built with .NET and Entity Framework Core.

Built as a school project during our Entity Framework course.

## Features

- Manage members, trainers, and memberships
- Create workouts with exercises
- Track membership plans and purchases
- View statistics (member count, salary summaries, etc.)

## Database Design

- **1-M:** Trainers → Members
- **M-M:** Members ↔ MembershipPlans (via Membership)
- **M-M:** Workouts ↔ Exercises (via WorkoutExercise)

## Tech Stack

- .NET 9
- Entity Framework Core
- PostgreSQL
- Code First with Migrations

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet)
- [PostgreSQL 17](https://www.postgresql.org/download/)
- [Docker](https://www.docker.com/products/docker-desktop/)
- [Entity Framework Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

## Setup

1. Clone the repository
```bash
git clone https://github.com/vuoalex/GitFitGym.git
cd GitFitGym
```

2. Start PostgreSQL with Docker
```bash
docker run --name gitfitgym -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=gitfitgym_db -p 5432:5432 -d postgres:17
```

3. Install Entity Framework Core CLI (if not already installed)
```bash
dotnet tool install --global dotnet-ef
```

4. Apply migrations and seed data
```bash
dotnet ef database update
```

5. Run the application
```bash
dotnet run
```

## Project Structure
```
GitFitGym/
├── Data/
│   ├── Entities/
│   ├── Repositories/
│   └── AppDbContext.cs
├── Domain/
│   ├── Interfaces/
│   ├── Models/
│   └── Gym.cs
├── Presentation/
└── Migrations/
```
