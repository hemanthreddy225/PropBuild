# Entity relationship diagram

The proposed relational model supports the template and avoids assuming that one property equals one unit.

```mermaid
erDiagram
  ORGANIZATIONS ||--o{ MEMBERSHIPS : has
  USERS ||--o{ MEMBERSHIPS : joins
  ORGANIZATIONS ||--o{ PROPERTIES : owns
  PROPERTIES ||--o{ UNITS : contains
  ORGANIZATIONS ||--o{ TENANTS : manages
  TENANTS ||--o{ TENANT_CONTACTS : has
  TENANTS ||--o{ LEASES : signs
  UNITS ||--o{ LEASES : is_leased_by
  LEASES ||--o{ LEASE_NOTES : includes
  LEASES ||--o{ RENT_CHARGES : creates
  PAYMENTS ||--o{ PAYMENT_ALLOCATIONS : splits
  RENT_CHARGES ||--o{ PAYMENT_ALLOCATIONS : receives
  LEASES ||--o{ DEPOSITS : secures
  DEPOSITS ||--o{ DEPOSIT_TRANSACTIONS : records
  LEASES ||--o{ MOVE_OUT_NOTICES : has
  TENANTS ||--o{ DOCUMENTS : owns
  LEASES ||--o{ DOCUMENTS : relates_to
  ORGANIZATIONS ||--o{ NOTIFICATIONS : sends
  ORGANIZATIONS ||--o{ AUDIT_EVENTS : records
```

`payment_allocations` resolves the many-to-many relationship between payments and rent charges. A document may reference a tenant, a lease, or both; constrain it to reference at least one.
