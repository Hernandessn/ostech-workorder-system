# 🛠️ OSTech

> A Service Order Management System built to study modern .NET, Web API and React development.

[![CI/CD](https://github.com/Hernandessn/ostech-workorder-system/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/Hernandessn/ostech-workorder-system/actions/workflows/ci.yml)
![.NET](https://img.shields.io/badge/.NET-9-purple)
![C%23](https://img.shields.io/badge/C%23-Language-blue)
![Entity%20Framework%20Core](https://img.shields.io/badge/EF%20Core-9-green)
![ASP.NET%20Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-blue)
![React](https://img.shields.io/badge/React-Frontend-61DAFB)
![MySQL](https://img.shields.io/badge/MySQL-Database-orange)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

---

# 🔗 Live Demo

- **App:** https://ostech-system.netlify.app
- **API health check:** https://ostech-workorder-system.onrender.com/health

> ⚠️ Both the API and the database run on free-tier infrastructure (Render + Aiven). The API may take 30–60 seconds to respond on the first request after a period of inactivity, as the free instance spins down when idle. Subsequent requests are fast.

---

# 📖 About

OSTech is a long-term learning project created to simulate the evolution of a real-world service order management system.

The project was developed incrementally, using the same application to practice different technologies, architectural concepts and development approaches.

The system currently contains:

- ASP.NET Core Web API
- ASP.NET Core MVC client
- React frontend
- Entity Framework Core
- MySQL
- DTOs
- Repository Pattern
- Unit of Work
- AutoMapper
- API versioning
- RESTful communication
- CRUD operations
- Entity relationships
- Form validation
- Error handling
- Responsive frontend
- TanStack Query

The objective is not only to build a functional application, but also to document the evolution of the project while learning modern software engineering practices.

---

# 🎯 Objectives

The project was created to practice:

- Modern C# and .NET
- Entity Framework Core
- Object-Oriented Programming
- SOLID principles
- RESTful API development
- ASP.NET Core Web API
- DTOs and mapping
- Repository Pattern
- Unit of Work
- ASP.NET Core MVC
- React
- API consumption
- State and asynchronous data management
- Componentization
- Form validation
- Responsive interfaces
- Clean code and refactoring

The project will continue evolving as new technologies and architectural concepts are studied.

---

# 🏗️ Current Architecture

The project currently contains different applications consuming the same backend API.

```text
OSTech
├── OSTech.Domain
├── OSTech.Application
├── OSTech.EFCore            (project name: OSTech.Infrastructure)
├── OSTech.WebAPI
├── OSTech.WebMVC
├── OSTech.Console
├── OSTech.UnitTests         (project name: OSTech.Tests)
└── ostechreact
```

---

# 🧱 Backend

## OSTech.Domain

Contains the domain entities and business concepts.

Current entities:

- Category
- Customer
- Equipment
- Technician
- WorkOrder

The domain also contains:

- Enums
- Entity relationships
- Domain properties and behavior

---

## OSTech.EFCore

Responsible for database persistence using Entity Framework Core.

Contains:

- DbContext
- Entity configurations
- Fluent API
- Migrations
- Seed data
- Database configuration
- Entity relationships
- LINQ queries

---

## OSTech.Infrastructure

Contains infrastructure-level implementations used by the application.

Current concepts include:

- Repository Pattern
- Unit of Work
- Persistence-related abstractions and implementations

---

## OSTech.WebAPI

REST API responsible for exposing the application's functionality.

The API provides endpoints for:

- Categories
- Customers
- Equipment
- Technicians
- Work Orders

The API uses:

- ASP.NET Core
- Controllers
- DTOs
- AutoMapper
- Dependency Injection
- Repository Pattern
- Unit of Work
- API Versioning
- Entity Framework Core

---

# 🚀 API Features

## Category

- Create
- Read
- Update
- Delete

---

## Customer

- Create
- Read
- Update
- Delete

---

## Equipment

- Create
- Read
- Update
- Delete

---

## Technician

- Create
- Read
- Update
- Delete

Additional information:

- Specialty
- Contact
- Availability
- Hiring Date

---

## Work Order

- Create
- Read
- Update
- Delete

Work Orders contain relationships with:

- Customer
- Technician
- Category
- Equipment

---

# 🔗 Entity Relationships

The main relationship of the application is the Work Order.

```text
Customer ───────────┐
                    │
Technician ─────────┤
                    │
Category ───────────┤──► WorkOrder
                    │
Equipment ──────────┘
```

A Work Order references:

```text
CustomerId
TechnicianId
CategoryId
EquipmentId
```

These relationships are used both by the API and by the frontend applications.

---

# 🌐 ASP.NET Core MVC

The project also contains an ASP.NET Core MVC client that consumes the Web API.

The MVC application implements CRUD interfaces for:

- Categories
- Customers
- Equipment
- Technicians
- Work Orders

The MVC client uses:

- ASP.NET Core MVC
- Razor Views
- HttpClientFactory
- Services
- ViewModels
- Dependency Injection
- REST API consumption

The application follows a separation between:

```text
Controller
    ↓
Service
    ↓
HttpClient
    ↓
Web API
```

This allowed the project to practice API consumption from a server-side MVC application before moving to React.

---

# ⚛️ React Frontend

The project also contains a React frontend that consumes the same ASP.NET Core Web API.

The React application was built after completing the ASP.NET Core portion of the project.

It currently provides CRUD interfaces for:

- Categories
- Customers
- Equipment
- Technicians
- Work Orders

---

# ⚛️ React Features

The React application includes:

- React Router
- Axios
- TanStack Query
- React Toastify
- Phosphor Icons
- Tailwind CSS
- Reusable components
- Custom hooks
- Form validation
- Loading states
- Empty states
- Error states
- Toast notifications
- Responsive layouts
- Custom modals

---

# 🔄 Data Management

TanStack Query is responsible for asynchronous server state.

The application uses:

- `useQuery`
- `useMutation`
- Query invalidation
- Cache
- Automatic refetching
- Mutation loading states

The frontend follows the pattern:

```text
React Component
       ↓
TanStack Query
       ↓
Service
       ↓
Axios
       ↓
ASP.NET Core Web API
       ↓
Entity Framework Core
       ↓
MySQL
```

---

# 🧩 React Componentization

The React application was refactored to reduce duplication and improve maintainability.

Reusable components include:

- `Container`
- `Header`
- `Loading`
- `EmptyState`
- `ErrorState`
- `Modal`
- `CreateButton`
- `ActionsButtons`

Entity-specific responsibilities are also separated into components such as:

```text
CreateCategory
EditCategory
DeleteCategory

CreateCustomer
EditCustomer
DeleteCustomer

CreateEquipment
EditEquipment
DeleteEquipment

CreateTechnician
EditTechnician
DeleteTechnician

CreateWorkOrder
EditWorkOrder
DeleteWorkOrder
```

---

# 🪝 Custom Hooks

The React application uses custom hooks to centralize repeated logic.

### `useModals`

Responsible for managing modal state:

```text
isCreateOpen
isEditOpen
isDeleteOpen
```

and their respective open/close functions.

### `useRequestState`

Used for state that remains manually controlled by the application, particularly validation errors.

Loading and mutation states are primarily handled by TanStack Query.

---

# ✅ Validation

Each entity has its own validation logic.

```text
validations/
├── categoryValidation.js
├── customerValidation.js
├── equipmentValidation.js
├── technicianValidation.js
└── workOrderValidation.js
```

Validation occurs before requests are sent to the API.

For example:

```javascript
const validationErrors = validateWorkOrder(workOrder);

if (Object.keys(validationErrors).length > 0) {
    setErrors(validationErrors);
    return;
}
```

Validation errors are displayed directly beside the corresponding fields.

---

# ⚠️ Error Handling

API errors are handled through a centralized utility:

```javascript
getApiErrorMessage(error)
```

The resulting message is displayed using React Toastify:

```javascript
toast.error(getApiErrorMessage(error));
```

This allows the frontend to display more meaningful feedback instead of generic error messages.

---

# 🔔 User Feedback

React Toastify is used to provide feedback for:

- Successful creation
- Successful updates
- Successful deletion
- API errors
- Validation-related operations

---

# 📱 Responsive Design

The React frontend was reviewed for multiple screen sizes.

The interface was tested on:

- Mobile
- Tablet
- Notebook
- Desktop

Special attention was given to layouts starting at approximately 360px wide.

---

# 📊 Dashboard

The React application includes a dashboard displaying operational information such as:

- Total Customers
- Total Technicians
- Total Equipment
- Total Categories
- Total Work Orders
- Work Orders by status

---

# 🛠️ Technologies

## Backend

- C#
- .NET 9
- ASP.NET Core
- Entity Framework Core
- LINQ
- MySQL
- AutoMapper
- REST API
- API Versioning
- Repository Pattern
- Unit of Work
- Dependency Injection

## MVC Client

- ASP.NET Core MVC
- Razor
- HttpClientFactory
- ViewModels
- Services

## Frontend

- React
- React Router
- TanStack Query
- Axios
- Tailwind CSS
- React Toastify
- Phosphor Icons

## Development

- Visual Studio
- Visual Studio Code
- Git
- GitHub

---

# 📂 Project Structure

```text
OSTech
├── OSTech.Domain
├── OSTech.Application
├── OSTech.EFCore            (project name: OSTech.Infrastructure)
├── OSTech.WebAPI
├── OSTech.WebMVC
├── OSTech.Console
├── OSTech.UnitTests         (project name: OSTech.Tests)
└── ostechreact
```

---

# 📚 Learning Roadmap

## ✅ Stage 1 — Entity Framework Core

Completed.

Topics studied:

- DbContext
- DbSet
- Fluent API
- Entity relationships
- LINQ
- CRUD
- Migrations
- Seed Data
- Change Tracker

---

## ✅ Stage 2 — SOLID Principles

Completed.

Topics studied:

- Object-Oriented Programming
- Encapsulation
- Interfaces
- Polymorphism
- Code Smells
- Clean Code
- SOLID Principles
- Refactoring

---

## ✅ Stage 3 — ASP.NET Core Web API

Completed.

Topics studied:

- REST API
- Controllers
- HTTP methods
- DTOs
- Dependency Injection
- Repository Pattern
- Unit of Work
- AutoMapper
- API Versioning
- CRUD
- Entity relationships
- Error handling

---

## ✅ Stage 4 — ASP.NET Core MVC

Completed.

Topics studied:

- MVC
- Razor Views
- ViewModels
- HttpClientFactory
- Services
- API consumption
- CRUD interfaces
- Dependency Injection

---

## ✅ Stage 5 — React

Completed.

Topics studied:

- Components
- Props
- State
- Hooks
- React Router
- Axios
- TanStack Query
- Mutations
- Query invalidation
- Componentization
- Custom hooks
- Form validation
- Error handling
- Responsive design

---

## ✅ Stage 6 — Domain-Driven Design

Completed.

Topics studied:

- Ubiquitous Language
- Entities vs. Anemic Models
- Value Objects
- Invariants and Guard Clauses
- Aggregates and Aggregate Roots
- Domain Services
- Domain Events (evaluated, deliberately not applied)
- Bounded Contexts
- Application Services
- Isolated domain unit testing


---

## ✅ Stage 7 — CQRS + MediatR

Completed.

Topics studied:

- Command Query Responsibility Segregation
- MediatR (IRequest, IRequestHandler)
- Read-model projections bypassing the rich domain model
- Application-layer orchestration via Command Handlers
- Conscious decision not to expand the pattern to every Aggregate

---

## ✅ Stage 8 — Docker

Completed.

Topics studied: multi-stage builds, Docker Compose, named volumes, healthchecks, isolated networks, image size optimization (Alpine), versioned tags, production override.

---

## 🚧 Stage 9 — CI/CD with GitHub Actions

In progress: CI and Docker image builds are working, deploys are manual, and documentation is being finalized.

---

# 🧭 Domain-Driven Design

After completing the initial architectural stages, the domain layer was revisited and evolved using Domain-Driven Design principles, applied selectively based on real needs identified in the domain — not as a checklist of patterns.

Key outcomes:

- **Entities** (`WorkOrder`, `Technician`, `Customer`, `Category`, `Equipment`) enforce their own invariants through guarded constructors and behavior methods, rejecting invalid state instead of relying on external validation.
- **Value Objects** (`EmailAddress`, `Document`) encapsulate format validation, normalization, and value-based equality, replacing primitive `string` fields where real business rules existed.
- **Aggregates**: each entity was evaluated against its own lifecycle and consistency boundary. The domain currently consists of five independent Aggregates, with no forced grouping.
- **Domain Service** (`CategoryDomainService`) isolates a rule that spans multiple Aggregates (blocking Category deletion when open Work Orders reference it), keeping it out of any single Entity.
- **Domain-specific exceptions** (`InvalidWorkOrderStatusException`, `CategoryDeletionBlockedException`) differentiate state-violation errors from generic domain validation errors.
- **Application Service** (`WorkOrderApplicationService`) extracts use-case orchestration (fetching dependencies, creating the entity, persisting) out of the Controller, which now only translates HTTP and delegates to the domain.

Deliberate decisions **not** to apply certain patterns were also part of the process — for example, Domain Events and multiple Bounded Contexts were evaluated and postponed, since no real need for them exists in the current domain.

A full sprint-by-sprint report of this process is available on request / in project notes.

--- 

# 🚧 Next Steps

- Validate Render rollback and Aiven restore
- Deterministic database for controller tests
- Registry publishing (postponed, see `TODO.md`)
- Automate migrations safely
- Fix or replace AutoMapper 12.0.1 (`NU1903`)

---

# 🧭 CQRS + MediatR

Building on the domain model established during the DDD phase, the `WorkOrder` Aggregate's use cases (Create, Update, Delete, Start, Complete, Cancel, and the two read queries) were migrated to a CQRS approach using MediatR.

Key outcomes:

- **Commands** (`CreateWorkOrderCommand`, `UpdateWorkOrderCommand`, `StartWorkOrderCommand`, etc.) encapsulate write intent and are handled by dedicated Handlers that orchestrate the domain — the business rules themselves remain inside the `WorkOrder` entity, unchanged.
- **Queries** (`GetWorkOrderByIdQuery`, `GetWorkOrdersQuery`) project directly from the database into DTOs via EF Core, bypassing the rich domain model entirely — read paths no longer pay the cost of materializing an Entity they won't mutate.
- **Domain-specific exceptions** were refined further: a new `NotFoundDomainException` was introduced to distinguish "resource not found" (404) from other domain rule violations (400), applied consistently across every `WorkOrder` Handler.
- The previously built `WorkOrderApplicationService` (see the DDD section) was fully absorbed into the CQRS Handlers rather than kept alongside them, avoiding duplicated orchestration logic.

As with the DDD phase, the pattern was **not** applied to the remaining Aggregates (`Technician`, `Customer`, `Category`, `Equipment`) — the WorkOrder migration already demonstrated the pattern end to end, and expanding it further was judged to add repetition without new learning value at this stage.

A full sprint-by-sprint report of this process is available on request / in project notes.

---

# 📈 Project Evolution

The main objective of OSTech is to demonstrate the evolution of the same application over time.

The project started as a simple Entity Framework Core Console Application and evolved into a system with:

```text
Console Application
        ↓
Entity Framework Core
        ↓
SOLID + Refactoring
        ↓
ASP.NET Core Web API
        ↓
ASP.NET Core MVC
        ↓
React
        ↓
Future architectural improvements
```

Instead of creating isolated projects for every technology, OSTech uses the same domain and application as a continuous learning environment.

This makes it possible to observe how the architecture changes as new requirements and technologies are introduced.

---

# ▶️ Getting Started

## Prerequisites

- .NET SDK **9.0.x** (the repository pins `9.0.319` in `global.json`)
- Node.js and npm
- MySQL 8

Configure the connection string with User Secrets (single quotes avoid shell interpolation of `$` in passwords):

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" 'Server=localhost;Database=OSTechDatabase;User=root;Password=<your-password>' --project OSTech.WebAPI
```

## Backend

Clone the repository:

```bash
git clone https://github.com/Hernandessn/ostech-workorder-system.git
```

Restore the .NET dependencies:

```bash
dotnet restore
```

Update the database:

```bash
dotnet ef database update \
    --project OSTech.EFCore \
    --startup-project OSTech.WebAPI
```

Run the API:

```bash
dotnet run --project OSTech.WebAPI
```

---

## React

Navigate to the React project:

```bash
cd ostechreact
```

Create your local environment file from the example (`.env` is not versioned):

```bash
cp .env.example .env
```

Then set `REACT_APP_API_URL` to the URL of your local API.

Install dependencies:

```bash
npm install
```

Start the development server:

```bash
npm start
```

Make sure the ASP.NET Core API is running before starting the frontend.

---

# 🐳 Running with Docker

The project can also be run entirely with Docker Compose, spinning up the React frontend, the ASP.NET Core Web API, and MySQL together.

## Prerequisites

- Docker Desktop installed and running

## Setup

Clone the repository:

```bash
git clone https://github.com/Hernandessn/ostech-workorder-system.git
cd ostech-workorder-system
```

Create a `.env` file in the project root with the following variables:

```env
MYSQL_ROOT_PASSWORD=your_password_here
MYSQL_DATABASE=OSTechDatabase
REACT_APP_API_URL=http://localhost:8000/api/v1
```

Start the containers:

```bash
docker-compose up --build
```

This will build and start three services:

- **mysql** — MySQL 8.0 database (exposed on port 3306)
- **api** — ASP.NET Core Web API (exposed on port 8000)
- **frontend** — React app served via Nginx (exposed on port 3000)

The `api` service waits for the MySQL healthcheck (`depends_on: condition: service_healthy`). `mysql` and `api` share a dedicated network, and `frontend` is kept outside it. Images are tagged `1.0.0` and the API image uses an Alpine base.

## Applying migrations

The database starts empty on first run. Migrations need to be applied manually, once, after the containers are up:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project OSTech.EFCore --startup-project OSTech.WebAPI
```

This step assumes your local `OSTech.WebAPI` User Secrets point to `Server=localhost;Database=OSTechDatabase;User=root;Password=<same as MYSQL_ROOT_PASSWORD>` — the same port MySQL exposes to the host.

## Accessing the application

- Frontend: http://localhost:3000
- API (Swagger): http://localhost:8000/swagger

---

## Simulating production

By default, `docker-compose up` runs the API in `Development` mode (Swagger enabled). To test a configuration closer to production, use the `docker-compose.prod.yml` override, which switches the API to `Production` mode (Swagger disabled):

```bash
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up --build
```

---

# 🔄 CI/CD and Deployment

## Pipeline

Workflow: `.github/workflows/ci.yml`. It runs on pushes to `master`, on pull requests targeting `master`, and manually (`workflow_dispatch`). Permissions are limited to `contents: read`. It has no deploy step and never touches production credentials.

| Job | What it does |
|---|---|
| `api` | Restores, builds the solution, starts a temporary MySQL 8.4 service, applies migrations to that test database and runs the domain unit tests (`FullyQualifiedName~UnitTests.Domain`, 25 tests) |
| `frontend` | Node 24.21.0, `npm ci` and `npm run build`, with `REACT_APP_API_URL` taken from the `REACT_APP_API_URL_CI` Actions variable |
| `docker` | Builds the API and frontend images without pushing them (`push: false`) |

The `master` branch is protected: merges are blocked when the required checks fail.

Full details: [`docs/ci-cd.md`](docs/ci-cd.md).

### What CI does not cover

- Controller tests that depend on a database (excluded by the test filter).
- Full UI tests for the frontend.
- Database migrations on the production database.
- Publishing images to a registry.

## Deployment

| Component | Host | Publishing |
|---|---|---|
| Frontend | Netlify | Manual |
| API | Render (Docker) | Manual, auto-deploy disabled |
| Database | Aiven MySQL (free tier) | Migrations applied manually |

A change reaches production like this: pull request to `master` → CI checks pass → merge → manual deploy on Render and Netlify → check `GET /health` and the main frontend flows.

The API exposes `GET /health`, which returns `{"status":"Healthy"}`.

## Production configuration (names only)

- API (Render): `ASPNETCORE_ENVIRONMENT`, `ConnectionStrings__DefaultConnection`, `Cors__AllowedOrigins__0`, `JWT__*`, `MyRateLimit__*`, `AllowedHosts`.
- `ConnectionStrings__DefaultConnection` must use the `key=value;` format (`Server=...;Port=...;Database=...;User=...;Password=...;SslMode=Required;`). A URI such as `mysql://...` is not supported.
- Frontend (Netlify): `REACT_APP_API_URL`, embedded at build time. It is visible in the browser, so never put secrets in it.

## Known limitations

- Render rollback: the option was located in the dashboard but never executed.
- Aiven: backups with point-in-time recovery are available, but a restore has never been tested. `Fork service` is not available on the current plan.
- `/health` only confirms that the application responds; it does not check the database.
- Migrations are applied manually.
- The free-tier API may take 30–60 seconds to wake up.
- AutoMapper 12.0.1 triggers the `NU1903` security warning (tracked in `TODO.md`).

---

# 📖 What I've Learned

Throughout the development of OSTech, I have practiced:

### Backend

- C#
- Object-Oriented Programming
- Entity Framework Core
- LINQ
- REST APIs
- DTOs
- Repository Pattern
- Unit of Work
- Dependency Injection
- API Versioning

### Frontend

- ASP.NET Core MVC
- Razor
- HttpClientFactory
- React
- React Router
- Axios
- TanStack Query
- Componentization
- Custom Hooks
- Form Validation
- Error Handling
- Responsive Design

### Engineering

- Refactoring
- Separation of Responsibilities
- Reusable Components
- API Contract consistency
- Git and incremental development
- Architectural evolution

---

# 📌 Current Status

**Backend:** ✅ Completed current course scope

**ASP.NET Core Web API:** ✅ Completed

**ASP.NET Core MVC:** ✅ Completed

**React Frontend:** ✅ Completed

**Domain-Driven Design:** ✅ Completed

**CQRS + MediatR:** ✅ Completed

**Docker:** ✅ Completed

**CI/CD:** 🚧 In progress

**Current focus:** 🚧 Continuing the software engineering roadmap

---

# 🤝 Contributing

This is primarily a personal learning and portfolio project.

Suggestions, feedback and discussions are welcome.

---

# 📄 License

This project is licensed under the MIT License.

---

# 👨‍💻 Author

**Hernandes**

Focused on Software Engineering, Backend Development, .NET and Enterprise Architecture.

GitHub: **https://github.com/Hernandessn**

LinkedIn: **www.linkedin.com/in/hernandes-sales**