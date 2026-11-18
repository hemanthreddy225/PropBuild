# PostgreSQL database design

## Conventions

Use `uuid` primary keys, `timestamptz` audit columns, `created_at`, `updated_at`, `created_by`, and soft delete only where retention policy requires it. Monetary values are `bigint` minor units plus `char(3) currency`; never use floating point. Use `date` for lease and charge periods.

## Core tables

| Table | Key columns | Notes |
|---|---|---|
| organizations | id, name, timezone, currency | Tenant-isolation root. |
| users | id, email, display_name, auth_subject, status | Unique normalized email. External identity subject unique. |
| memberships | organization_id, user_id, role, status | Unique organization/user; role: owner, manager, accountant, viewer. |
| properties | id, organization_id, name, address fields, status | Unique name per organization when active. |
| units | id, property_id, label, unit_type, status | Supports current flat/villa and future multi-unit properties. |
| tenants | id, organization_id, legal_name, email, phone, occupation, status | Person profile; do not embed mutable lease facts. |
| tenant_contacts | id, tenant_id, kind, name, phone, email | `emergency` contact can be one or many. |
| leases | id, organization_id, unit_id, tenant_id, start_date, end_date, rent_minor, deposit_minor, currency, due_day, status, version | A tenant can have sequential leases; active dates must not overlap on a unit. |
| lease_notes | id, lease_id, body, visibility | Author and time stamped. |
| rent_charges | id, lease_id, period_start, due_date, amount_minor, status | Unique `(lease_id, period_start, charge_type)`. |
| payments | id, organization_id, received_on, amount_minor, currency, method, reference, status | A payment can pay more than one charge. |
| payment_allocations | payment_id, rent_charge_id, amount_minor | Sum cannot exceed payment or charge balance. |
| deposits | id, lease_id, amount_minor, received_on, held_minor, status | Track received, deductions, refunded—not merely a displayed number. |
| deposit_transactions | id, deposit_id, kind, amount_minor, occurred_on, reference | Kinds: receive, deduction, refund, adjustment. |
| move_out_notices | id, lease_id, notice_date, move_out_date, status | Drives vacating view and workflow. |
| documents | id, organization_id, tenant_id, lease_id, storage_key, original_name, media_type, byte_size, checksum, status | Private storage reference, not binary DB storage. |
| notifications | id, organization_id, type, recipient, payload, status, idempotency_key | Delivery attempts and provider identifiers. |
| audit_events | id, organization_id, actor_user_id, entity_type, entity_id, action, before_json, after_json, occurred_at | Append-only audit trail. |
| outbox_events | id, event_type, payload, occurred_at, processed_at | Reliable async dispatch. |

## Essential constraints and indexes

- `leases.due_day BETWEEN 1 AND 28`; `end_date >= start_date`; monetary values `>= 0`.
- Prevent overlapping active/pending lease date ranges per `unit_id` using an exclusion constraint on `daterange(start_date, end_date, '[]')`.
- Index organization-scoped lists: `tenants(organization_id, legal_name)`, `leases(organization_id, status, end_date)`, `rent_charges(lease_id, due_date, status)`, `payments(organization_id, received_on)`.
- Enforce row-level security by `organization_id` or keep all access behind a service role with mandatory scoped repository methods; choose and document one approach before implementation.
