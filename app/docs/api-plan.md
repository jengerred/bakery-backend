# 🔌 Backend API — Plan

The backend will unify all modules under a single API.

---

## ⭐ Tech Stack

- C# (.NET 8)
- SQL Server
- Entity Framework Core
- REST API

---

## ⭐ Planned Endpoints

### Customers
- GET /customers
- GET /customers/{phone}
- POST /customers

### Orders
- POST /orders
- GET /orders/today
- GET /orders/{id}

### Inventory
- GET /inventory
- POST /inventory/update
- GET /inventory/usage

### Employees
- POST /login
- GET /employees
- POST /employees/clock-in

---

## ⭐ Authentication

- JWT (planned)
- Role‑based permissions
