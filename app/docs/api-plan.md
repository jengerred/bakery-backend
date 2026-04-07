# 🔌 Backend API — Architecture & Implementation Plan
Version 1.0 — C# / .NET 8 + PostgreSQL (Supabase)  
Bakery Business Suite

---

## ⭐ 1. Overview

The backend provides a unified, secure, scalable API for all modules of the Bakery Business Suite:

- POS (cashier‑facing)
- Online Ordering (customer‑facing)
- Manager Dashboard (admin‑facing)
- Customer Accounts & Loyalty
- Inventory System
- Employee System
- Order Processing
- Stripe Payments & Webhooks

The backend is implemented as a C# / .NET 8 REST API connected to a PostgreSQL database hosted on Supabase.

All business logic, authentication, and data integrity live in this backend.

---

## ⭐ 2. Tech Stack

### Backend
- C# / .NET 8 Web API
- Entity Framework Core 8
- ASP.NET Identity (customized) or custom JWT auth
- MediatR (optional, for clean CQRS patterns)
- FluentValidation (optional, for request validation)

### Database
- PostgreSQL (Supabase)
- EF Core Migrations
- Row‑level security (RLS) for customer data (optional)

### Auth
- JWT tokens
- Refresh tokens
- Role‑based permissions
- Customer login
- Employee login (PIN or password)

### Integrations
- Stripe Webhooks
- Email (SendGrid or Supabase SMTP)
- Optional: Twilio SMS for order updates

---

## ⭐ 3. Core Data Models

These models form the backbone of the bakery system.

### Customer
- Id  
- Name  
- Email  
- Phone  
- PasswordHash  
- LoyaltyPoints  
- CreatedAt  

### Employee
- Id  
- Name  
- Role (Cashier, Manager, Admin)  
- PinHash (for POS login)  
- PasswordHash (for dashboard login)  
- Active  
- CreatedAt  

### Product
- Id  
- Name  
- Price  
- Category  
- ImageUrl  
- Description  
- Active  

### Order
- Id  
- CustomerId (nullable for guest checkout)  
- EmployeeId (nullable for online orders)  
- Total  
- Method (Pickup / Shipping / In‑Person)  
- Status (Pending, Paid, Preparing, Ready, Completed)  
- CreatedAt  

### OrderItem
- Id  
- OrderId  
- ProductId  
- Quantity  
- PriceAtPurchase  

### Inventory
- ProductId  
- Stock  
- LowStockThreshold  

### LoyaltyTransaction
- Id  
- CustomerId  
- Points  
- Reason  
- CreatedAt  

### TillSession
- Id  
- EmployeeId  
- OpenedAt  
- ClosedAt  
- OpeningAmount  
- ClosingAmount  
- ExpectedAmount  
- Notes  

---

## ⭐ 4. API Endpoints

### Authentication

#### Customers
- POST /auth/customer/register  
- POST /auth/customer/login  
- POST /auth/customer/refresh  

#### Employees
- POST /auth/employee/login (PIN or password)  
- POST /auth/employee/refresh  

---

### Products
- GET /products  
- GET /products/{id}  
- POST /products (manager/admin)  
- PATCH /products/{id} (manager/admin)  
- DELETE /products/{id} (admin)  

---

### Orders

#### POS + Online Ordering
- POST /orders  
- GET /orders/{id}  
- GET /orders/customer/{customerId}  
- GET /orders/today (manager)  
- PATCH /orders/{id}/status  

#### Stripe
- POST /webhooks/stripe  

---

### Customers
- GET /customers/{id}  
- GET /customers/lookup?phone=xxx  
- POST /customers  
- PATCH /customers/{id}  

---

### Employees
- GET /employees (manager/admin)  
- POST /employees (admin)  
- PATCH /employees/{id}  

---

### Inventory
- GET /inventory  
- PATCH /inventory/update  
- GET /inventory/usage  
- GET /inventory/low-stock  

---

### Loyalty
- GET /loyalty/{customerId}  
- POST /loyalty/add  
- POST /loyalty/redeem  

---

### Till Sessions
- POST /till/open  
- POST /till/close  
- GET /till/today  
- GET /till/{id}  

---

## ⭐ 5. Authentication & Authorization

### JWT Tokens
- Access token (15–30 min)  
- Refresh token (7–30 days)  

### Roles
- Customer  
- Cashier  
- Manager  
- Admin  

### Permissions

| Role     | Can Place Orders | Can Manage Products | Can View Reports | Can Manage Employees |
|----------|------------------|---------------------|------------------|----------------------|
| Customer | ✔                | ✘                   | ✘                | ✘                    |
| Cashier  | ✔                | ✘                   | ✘                | ✘                    |
| Manager  | ✔                | ✔                   | ✔                | ✘                    |
| Admin    | ✔                | ✔                   | ✔                | ✔                    |

---

## ⭐ 6. Stripe Integration

### Online Ordering
- PaymentIntent created client‑side  
- Confirmed via Stripe Elements  
- Webhook updates order status  

### POS
- Card reader integration (future)  
- Manual card entry fallback  
- Cash payments tracked in TillSession  

---

## ⭐ 7. Deployment Plan

### Backend
Deploy C# API to:
- Azure App Service  
- Render  
- Fly.io  
- Railway  

### Database
- Supabase PostgreSQL  
- Automated backups  
- Row‑level security optional  

### Frontend
- Vercel (Next.js)  

---

## ⭐ 8. Future Enhancements

- Real‑time order updates (SignalR)  
- Kitchen Display System integration  
- SMS notifications  
- Multi‑store support  
- Advanced inventory forecasting  
- Payroll + scheduling module  

---

## ⭐ 9. Summary

This backend architecture is:

- Scalable  
- Secure  
- Enterprise‑grade  
- Perfect for a real bakery  
- Perfect for POS + online ordering  
- Perfect for tutorials  
- Perfect for long‑term growth  
