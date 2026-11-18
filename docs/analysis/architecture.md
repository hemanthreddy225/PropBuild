# Architecture and component breakdown

## Recommended boundaries

Use a browser client, a versioned REST API, PostgreSQL, private object storage for documents, and an asynchronous worker for notifications/receipts. The template is visual reference only—not a production component implementation.

```text
Web client -> API -> PostgreSQL
                  -> Object storage (private documents)
                  -> Job queue -> email/SMS provider
                  -> audit log
```

## Frontend components

| Component | Responsibility |
|---|---|
| App shell and navigation | Authenticated layout, route changes, tenant/property context. |
| Tenant list | Server-driven search/filter/sort/page, summary cards, overdue notice. |
| Tenant form | Client validation, create/update tenant and lease relationship. |
| Tenant detail | Profile, current/past leases, payments, documents, notes. |
| Renewals board | Date-window query, renewal workflow and notice action. |
| Payments board | Period summary, payment state, reminder and receipt action. |
| Vacating board | Notice, inspection, deposit-settlement/refund workflow. |
| Document manager | Upload, secure download, metadata and deletion authorization. |
| Shared UI | Currency/date formatting, status labels, accessible dialog/toast/error states. |

## Backend modules

| Module | Responsibility |
|---|---|
| Identity and access | Sessions/tokens, password/MFA policy, membership, role checks. |
| Organization and property | Organization isolation; properties and rentable units. |
| Tenant | Tenant profile, contacts, notes, duplicate detection. |
| Lease | Lifecycle, rent terms, due day, renewals, notices, occupancy constraints. |
| Billing and payments | Monthly charges, allocations, payment capture, receipts, overdue derivation. |
| Documents | Malware/type/size validation, private storage, signed URLs, retention. |
| Notifications | Idempotent notice/reminder/receipt jobs and delivery recording. |
| Reporting | Tenant, collection, overdue, expiry and vacancy summaries. |
| Audit | Immutable records of material mutation and sensitive-document access. |

## Cross-cutting requirements

- Enforce organization scope in every query; never trust a client organization ID.
- Store money in integer minor units with ISO currency; dates as `date`, timestamps in UTC.
- Use database transactions for lease/charge/payment/deposit changes.
- Publish events with an outbox table so notification delivery cannot be lost or duplicated.
- Use optimistic concurrency (`version`/ETag) on editable resources.
