# 👤 Customer System — Overview

This document outlines the customer‑facing and POS‑facing customer features.

---

## ⭐ Customer Features (Planned)

- Customer lookup (POS)
- Add new customer
- Customer profiles
- Loyalty points
- Saved payment methods
- Customer order history
- Email receipts

---

## ⭐ Customer Data Model (Draft)

```ts
type Customer = {
  id: string
  name: string
  phone: string
  email?: string
  loyaltyPoints?: number
}
