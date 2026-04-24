# Softqu Backend Task - Category Management System

This repository contains a robust implementation of a hierarchical category management system, developed as a technical assessment. The project demonstrates advanced architectural patterns and best practices in modern .NET development.

## 📂 Project Structure
* **Softqu.Domain**: Entities, Value Objects, and Domain Interfaces.
* **Softqu.Application**: CQRS Handlers, DTOs, and Business Logic.
* **Softqu.Infrastructure**: Data Persistence (EF Core), Configurations, and Caching.
* **Softqu.Api**: Controllers and API Configuration (Scalar).

## 🏗 Architecture & Patterns
The project is built following **Clean Architecture** principles to ensure separation of concerns, testability, and maintainability:

* **Domain-Driven Design (DDD):** Use of Aggregate Roots, Entities, and Value Objects. Logic is encapsulated within the Domain layer.
* **CQRS Pattern:** Separation of Read and Write operations using **MediatR**.
* **Result Pattern:** Unified response handling using a custom `AppResult` to avoid exception-based flow control.
* **Repository Pattern:** Abstraction of data access logic.

## 🚀 Key Features
* **Hierarchical Structure:** Supports infinite nesting of categories (Parent/Child relationship).
* **Localization:** Multi-language support for category titles using a dedicated translation entity.
* **High Performance:**
    * **Distributed Caching:** Integration with Redis/DistributedCache for fast retrieval of category trees.
    * **Cache Invalidation:** Smart cache clearing on data updates to ensure consistency.
* **Soft Delete:** Categories are marked as deleted without being removed from the database, preserving data integrity.
* **Slug-based Routing:** SEO-friendly URLs via unique slugs.

## 🛠 Tech Stack
* **.NET 9 Core**
* **Entity Framework Core** (SQL Server)
* **MediatR** (CQRS)
* **StackExchange.Redis** (Caching)
* **FluentValidation** (Request Validation)
* **Manual Mapping** (DTO Transformations)

## 📝 API Documentation & Testing
The project integrates **Scalar**, a modern and interactive alternative to Swagger, providing a superior documentation interface.

* **Interactive API Console:** Test all endpoints directly from the browser.
* **Multiple Language Client Code:** Automatically generates code snippets for your API in various languages (JavaScript, Python, C#, etc.).
* **Access:** Once the project is running, you can access the documentation at:
  `https://localhost:{port}/scalar/v1`
  <img width="1881" height="792" alt="image" src="https://github.com/user-attachments/assets/65a43972-e0d1-419f-b49d-5c7f844d5da2" />


## ⚙️ Setup
1. Clone the repository.
2. Update the connection string in `appsettings.json`.
3. Run `dotnet ef database update` to apply migrations.
4. (Optional) Ensure Redis is running for caching features.
