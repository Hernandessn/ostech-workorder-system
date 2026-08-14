# 🛠️ OSTech

> A Service Order Management System built to study modern .NET, Web API and React development.

![.NET](https://img.shields.io/badge/.NET-10-purple)
![C%23](https://img.shields.io/badge/C%23-Language-blue)
![Entity%20Framework%20Core](https://img.shields.io/badge/EF%20Core-9-green)
![ASP.NET%20Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-blue)
![React](https://img.shields.io/badge/React-Frontend-61DAFB)
![MySQL](https://img.shields.io/badge/MySQL-Database-orange)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

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
│
├── OSTech.Domain
│
├── OSTech.EFCore
│
├── OSTech.Infrastructure
│
├── OSTech.WebAPI
│
├── OSTech.WebMVC
│
├── OSTech.Console
│
└── OSTech.React
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
- .NET 10
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
│
├── OSTech.Console
│
├── OSTech.Domain
│
├── OSTech.EFCore
│
├── OSTech.Infrastructure
│
├── OSTech.WebAPI
│
├── OSTech.WebMVC
│
└── OSTech.React
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

# 🚧 Next Steps

The OSTech will continue evolving as new software engineering concepts are studied.

Planned topics include:

- Clean Architecture
- Automated Testing / TDD
- Docker
- Domain-Driven Design
- CQRS
- MediatR
- RabbitMQ
- Oracle Database
- Microservices
- Authentication and Authorization

These technologies will be introduced gradually rather than being treated as completed features.

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

## Backend

Clone the repository:

```bash
git clone https://github.com/your-username/OSTech.git
```

Restore the .NET dependencies:

```bash
dotnet restore
```

Update the database:

```bash
dotnet ef database update \
    --project OSTech.EFCore \
    --startup-project OSTech.Console
```

Run the API:

```bash
dotnet run --project OSTech.WebAPI
```

---

## React

Navigate to the React project:

```bash
cd OSTech.React
```

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