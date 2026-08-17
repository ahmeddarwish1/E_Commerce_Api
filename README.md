# E-Commerce Backend API

A RESTful E-Commerce Web API built with **C#, ASP.NET Core, Entity Framework Core, and SQL Server**.

The project is designed using **Clean Architecture** to separate business logic, application services, infrastructure concerns, and API endpoints.

## Technologies

* C#
* .NET / ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* LINQ
* AutoMapper
* ASP.NET Core Identity
* Swagger / OpenAPI
* Dependency Injection

## Architecture

The solution is organized into four main layers:

* **E_Commerce_Domain** — Contains the core entities and domain contracts.
* **E_Commerce_Application** — Contains application services, DTOs, contracts, mappings, and business logic.
* **E_Commerce_Infrastructure** — Contains EF Core, repositories, Unit of Work, database configurations, migrations, and data seeding.
* **E_Commerce_Api** — Contains API controllers, middleware, configuration, and HTTP endpoints.

### Dependency Flow

```text
E_Commerce_Api
       ↓
E_Commerce_Application
       ↓
E_Commerce_Domain

E_Commerce_Infrastructure
       ↓
E_Commerce_Application
       ↓
E_Commerce_Domain
```

## Features

* RESTful Web API endpoints
* Product management
* Product, Brand, and Type data
* Entity Framework Core database integration
* SQL Server
* Generic Repository pattern
* Unit of Work pattern
* Dependency Injection
* DTOs
* AutoMapper
* ASP.NET Core Identity
* Database migrations
* Data seeding
* Swagger / OpenAPI documentation
* Clean Architecture

## Database

The application uses **Microsoft SQL Server** with **Entity Framework Core**.

Database functionality includes:

* Entity configurations
* Relationships
* EF Core migrations
* Database seeding

## API Documentation

The API can be tested and explored using **Swagger / OpenAPI**.

After running the application, open the Swagger interface provided by the ASP.NET Core application to view and test the available endpoints.

## Getting Started

### Prerequisites

* .NET SDK
* Microsoft SQL Server
* Visual Studio or another .NET-compatible IDE

### Setup

1. Clone the repository.

```bash
git clone https://github.com/ahmeddarwish1/E_Commerce_Api.git
```

2. Open the solution in Visual Studio.

3. Configure the SQL Server connection string in the application configuration.

4. Build the solution.

5. Run the application.

6. Open Swagger to explore and test the API endpoints.

## Project Structure

```text
E_Commerce_Api
│
├── E_Commerce_Domain
│
├── E_Commerce_Application
│
├── E_Commerce_Infrastructure
│
└── E_Commerce_Api
```

## Author

**Ahmed Darwish**

Junior .NET / ASP.NET Core Backend Developer

* GitHub: https://github.com/ahmeddarwish1
* LinkedIn: https://linkedin.com/in/ahmed-darwish-5g
