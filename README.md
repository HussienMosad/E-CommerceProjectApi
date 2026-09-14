# E-Commerce API

A scalable **E-Commerce RESTful API** built with **ASP.NET Core 10**, designed with a layered architecture and focused on clean separation of concerns and maintainable backend development.

## Features

* **Authentication & Authorization**

  * User registration and login
  * JWT-based authentication
  * User profile and address management

* **Product Catalog**

  * Products, brands, and types
  * Pagination
  * Filtering by brand and type
  * Searching and sorting

* **Shopping Basket**

  * Create and update baskets
  * Redis-based basket storage and caching

* **Orders**

  * Create and retrieve orders
  * Delivery methods
  * Order total and shipping calculation

* **Payments**

  * Stripe Payment Intent integration
  * Payment validation
  * Stripe Webhook handling for payment status updates

## Architecture

The project follows a **layered architecture inspired by Clean Architecture principles**, separating business logic, domain entities, infrastructure, and API presentation.

```text
API / Controllers
       ↓
Services
       ↓
Domain
       ↓
Infrastructure
       ↓
SQL Server / Redis
```

## Technologies

* **C# / .NET 10**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **ASP.NET Core Identity**
* **JWT Authentication**
* **Redis**
* **Stripe**
* **AutoMapper**
* **Swagger / OpenAPI**

## Design Patterns & Concepts

* Repository Pattern
* Unit of Work
* Specification Pattern
* Dependency Injection
* DTOs
* Middleware
* Global Exception Handling
* Data Seeding
* Pagination, Filtering, Searching & Sorting
* Caching

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/HussienMosad/E-CommerceProjectApi.git
cd E-CommerceProjectApi
```

### 2. Configure the application

Set up the required:

* SQL Server connection strings
* Redis connection
* JWT configuration
* Stripe credentials

> **Security:** Do not commit real passwords, JWT secrets, Stripe keys, or other sensitive credentials to the repository.

### 3. Apply database migrations

```bash
dotnet ef database update
```

## Project Purpose

This project was built to practice and demonstrate **real-world backend development with ASP.NET Core**, including API architecture, authentication, database design, caching, payment integration, and common backend design patterns.

## Author

**Hussien Mosad**

GitHub:
https://github.com/HussienMosad
