# 🛠️ OSTech

> A Service Order Management System built to study modern .NET development.

![.NET](https://img.shields.io/badge/.NET-10-purple)
![C#](https://img.shields.io/badge/C%23-Language-blue)
![Entity%20Framework%20Core](https://img.shields.io/badge/EF%20Core-9-green)
![MySQL](https://img.shields.io/badge/MySQL-Database-orange)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

---

# 📖 About

OSTech is a long-term learning project created to simulate the evolution of a real enterprise application.

Instead of creating multiple disconnected projects, the same system is continuously improved while new technologies and architectural concepts are learned.

The project started as a simple Console Application using Entity Framework Core and will gradually evolve into a complete enterprise solution using:

- ASP.NET Core Web API
- Clean Architecture
- SOLID Principles
- TDD
- Docker
- DDD
- CQRS
- RabbitMQ
- Oracle Database

The goal is not only to build software, but also to document the learning process and architectural evolution throughout the project.

---

# 🎯 Objectives

- Learn modern C#
- Master Entity Framework Core
- Apply SOLID principles
- Build RESTful APIs
- Learn Clean Architecture
- Practice Test Driven Development (TDD)
- Containerize applications using Docker
- Apply Domain-Driven Design (DDD)
- Implement CQRS with MediatR
- Integrate asynchronous communication using RabbitMQ
- Work with Oracle Database

---

# 🏗️ Current Architecture

```
OSTech
│
├── OSTech.Console
│
├── OSTech.Domain
│
└── OSTech.EFCore
```

### Project Responsibilities

### OSTech.Console

Responsible for:

- Testing features
- Executing CRUD operations
- Running the application

---

### OSTech.Domain

Contains the business model.

Current entities:

- Technician
- WorkOrder

Also contains:

- Enums

---

### OSTech.EFCore

Responsible for data persistence.

Contains:

- DbContext
- Entity Configurations
- Fluent API
- Migrations
- Seed Data
- Database Configuration

---

# 🚀 Features

## Technician

- Create Technician
- Update Technician
- Delete Technician
- Search Technician
- List all Technicians

---

## Work Order

- Create Work Order
- Update Work Order
- Delete Work Order
- Search Work Order
- List all Work Orders

---

## Database

- Entity Framework Core
- Fluent API Mapping
- One-to-Many Relationship
- LINQ Queries
- Migrations
- Seed Data

---

# 🛠️ Technologies

Current stack:

- C#
- .NET 10
- Entity Framework Core
- LINQ
- MySQL
- Console Application
- Git
- GitHub

Future stack:

- ASP.NET Core Web API
- Swagger
- Dependency Injection
- Clean Architecture
- xUnit
- Moq
- Docker
- Docker Compose
- MediatR
- RabbitMQ
- Oracle Database

---

# 📂 Database Model

Current relationship:

```
Technician (1)
      │
      │
      │
      ▼
WorkOrder (N)
```

Each Technician can be responsible for multiple Work Orders.

Each Work Order belongs to one Technician.

---

# 📚 Learning Roadmap

## ✅ Stage 1 — Entity Framework Core

Completed

Topics studied:

- Entity Framework Core
- DbContext
- DbSet
- Fluent API
- Relationships
- LINQ
- CRUD
- Migrations
- Seed Data
- Change Tracker

---

## ✅ Stage 2 — SOLID

Completed

Topics studied:

- Object-Oriented Programming
- Encapsulation
- Interfaces
- Polymorphism
- Code Smells
- Clean Code
- SOLID Principles
- Guided Refactoring

---

## ⏳ Stage 3 — ASP.NET Core Web API

Planned

Topics:

- Controllers
- REST API
- Dependency Injection
- Swagger
- HTTP Methods
- Services
- DTOs

---

## ⏳ Stage 4 — Clean Architecture

Planned

---

## ⏳ Stage 5 — TDD

Planned

---

## ⏳ Stage 6 — Docker

Planned

---

## ⏳ Stage 7 — DDD + CQRS

Planned

---

## ⏳ Stage 8 — RabbitMQ

Planned

---

## ⏳ Stage 9 — Oracle SQL

Planned

---

# 📈 Project Evolution

The objective of this repository is to demonstrate the continuous evolution of the same application.

Each roadmap stage introduces new concepts while preserving and improving the existing codebase.

This approach simulates how enterprise software evolves over time.

---

# ▶️ Getting Started

## Clone the repository

```bash
git clone https://github.com/your-username/OSTech.git
```

## Restore packages

```bash
dotnet restore
```

## Update the database

```bash
dotnet ef database update --project OSTech.EFCore --startup-project OSTech.Console
```

## Run the project

```bash
dotnet run --project OSTech.Console
```

---

# 📖 What I've Learned

## Entity Framework Core

- DbContext
- DbSet
- Fluent API
- Relationships
- LINQ
- Migrations
- Seed Data
- Change Tracker

---

## SOLID

- SRP
- OCP
- LSP
- ISP
- DIP

---

## Git

- Branches
- Commits
- Repository organization

---

# 📌 Next Milestone

Implement the REST API using ASP.NET Core Web API.

---

# 🤝 Contributing

This repository is a personal learning project.

Suggestions and feedback are always welcome.

---

# 📄 License

This project is licensed under the MIT License.

---

# 👨‍💻 Author

**Hernandes**

Computer Science Student

Passionate about Software Engineering, Backend Development and Enterprise Architecture.

GitHub: **(your profile)**

LinkedIn: **(your profile)**