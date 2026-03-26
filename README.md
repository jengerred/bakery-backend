# 🧁 Bakery Business Suite
A Modular, Full‑Stack System for Running an Entire Bakery Operation
<br>

![Status](https://img.shields.io/badge/status-active-brightgreen)
![Next.js](https://img.shields.io/badge/Next.js-14-black)
![Stripe](https://img.shields.io/badge/Stripe-Integrated-blue)
![TypeScript](https://img.shields.io/badge/TypeScript-Strict-blue)
![License](https://img.shields.io/badge/license-MIT-lightgrey) <br>
<br>

# 📌 Repository Description
This repository is the master monorepo for the Bakery Business Suite — a scalable, modular system designed to run every part of a bakery business, including:

- POS (cashier‑facing)

- Manager Dashboard

- Inventory & Ingredient Tracking

- Employee System

- Scheduling & Payroll

- Customer Rewards

- Analytics & Reporting

- Online Ordering

- In‑Store Kiosk

- Kitchen Display System

Each module is designed to be independent, extensible, and production‑ready, following clean architecture principles. <br>
<br>

## 🧭 SDLC Phase & Progress Tracker
This project follows a full SDLC lifecycle. Below is the current status.

### SDLC Phase (Current)
**SDLC Phase 4 — Implementation (Iterative MVP Build)**
<br>

### Completed SDLC Work
🟢 Planning
Defined scope, goals, constraints, and bakery‑specific requirements.
<br>

🟢 Analysis
Identified functional requirements, workflows, and data needs.
<br>

🟢 Design
Created ERD, workflows, UI architecture, and module boundaries.
<br>

🟡 Implementation (Current)
Building POS MVP, Manager Dashboard, and shared architecture.
<br>

⚪ Testing (Upcoming)
Unit tests, integration tests, UI tests, payment flow tests.
<br>

⚪ Deployment (Upcoming)
Deploy to Vercel + backend services.
<br>

⚪ Maintenance (Future)
Add new modules, fix bugs, optimize performance. <br>
<br>
| Module | Status | Notes |
| --- | --- | --- |
| **POS System** | 🟢 Complete (Phase 1) | Fully functional, Stripe integrated |
| **Manager Dashboard** | 🟡 Phase 1 Complete | Summary cards, payment breakdown, recent orders |
| **Inventory System** | ⚪ Not Started | Ingredient deduction + real‑time tracking |
| **Employee System** | ⚪ Not Started | Login, roles, time tracking |
| **Scheduling & Payroll** | ⚪ Not Started | Manager‑facing |
| **Customer Rewards** | ⚪ Not Started | Loyalty points + history |
| **Online Ordering** | ⚪ Not Started | Customer‑facing website |
| **In‑Store Kiosk** | ⚪ Not Started | Touch‑friendly ordering |
| **Kitchen Display System** | ⚪ Not Started | Order queue for kitchen |
| **Cloud Backend** | ⚪ Not Started | C# + SQL Server + API layer | <br>
<br>

## 🛠️ Current Modules
### ✔ POS System (Standalone Repo)
👉 https://github.com/jengerred/bakery-pos <br>
<br>

**A fully functional cashier‑facing POS with:**

- Product grid

- Cart system

- Checkout modal

- Stripe payments

- Order history

- Receipt printing

- Payment method tagging

- Reader simulation

- Clean modular architecture <br>
<br>

### ✔ Manager Dashboard (Phase 1 UI Complete)
Located in /app/manager

**Includes:**

- Today’s sales summary

- Payment breakdown

- Recent orders

- Modular components

- Custom hook for metrics (useTodayOrders)
<br>

## 🧱 Architecture Overview
### High‑Level ASCII Architecture Diagram

```txt
                   ┌──────────────────────────────┐
                   │      Bakery Business Suite   │
                   └──────────────────────────────┘
                                │
        ┌───────────────────────┼───────────────────────┐  
        │                       │                       │   
┌──────────────┐      ┌────────────────┐       ┌──────────────────┐  
│   POS (UI)   │      │ Manager Dash   │       │  Online Ordering │  
│ Next.js/TS   │      │ Next.js/TS     │       │ Next.js/TS       │ 
└──────────────┘      └────────────────┘       └──────────────────┘ 
        │                       │                       |
        └──────────────┬────────┴──────────────┬────────┘
                       ▼                       ▼
              ┌────────────────┐      ┌──────────────────┐
              │  API Layer     │      │  Realtime Events │
              │   C# / .NET    │      │   Webhooks/WS    │
              └────────────────┘      └──────────────────┘
                       │
                       ▼
              ┌────────────────┐
              │   SQL Server   │
              │  (Core Data)   │
              └────────────────┘
                       │
                       ▼
              ┌────────────────┐
              │ MongoDB (UI)   │
              │ Sessions/Logs  │
              └────────────────┘
```
<br>
<br>

 ## 🚀 Roadmap <br>
#### Phase 1 — POS (Complete)
✅ Order entry

✅ Stripe payments

✅ Receipt printing

✅ Order history <br>
<br>

#### Phase 2 — Inventory System
⚪ Ingredient deduction

⚪ Real‑time inventory

⚪ Auto‑reorder logic

⚪ Ingredient usage analytics <br>
<br>

#### Phase 3 — Employee System
⚪ Login system

⚪ Hours tracking

⚪ Role permissions <br>
<br>

#### Phase 4 — Manager Dashboard Expansion
⚪ Sales charts

⚪ Inventory alerts

⚪ Employee performance

⚪ Scheduling

⚪ Payroll <br>
<br>

#### Phase 5 — Customer Features
⚪ Rewards

⚪ Order history

⚪ Messaging <br>
<br>
<br>
### 📸 Screenshots (Coming Soon)
POS UI

- Checkout modal

- Receipt

- Manager Dashboard <br>
<br>

### 🤝 Contributing
Contributions are welcome!
To contribute:

1. Fork the repository

2. Create a feature branch

3. Commit your changes

4. Open a pull request

Please follow clean architecture principles and modular design patterns. <br>
<br>

### 🧑‍💻 Tech Stack
- Next.js 14

- React

- TypeScript

- TailwindCSS

- Stripe Elements

- C# / .NET 8 (future backend)

- SQL Server (core business data)

- MongoDB Atlas (UI datastore) <br>
<br>

### 📝 License
MIT License <br>
<br>

### 👩‍💻 Author
Jennifer Gerred  
Full‑stack developer specializing in modular, scalable business systems.<br>
<br>