# REST API specification

Base path: `/api/v1`. All endpoints require an authenticated user and organization scope, except identity bootstrap/callback endpoints. JSON uses ISO 8601 dates, RFC 3339 timestamps, UUIDs, and `*_minor` integer money fields. List responses use `{ data, page: { cursor, has_more } }`; mutations return the representation and an ETag/version.

## Identity and organization

| Method / path | Purpose | Access |
|---|---|---|
| `GET /me` | Current user, memberships, effective permissions | Member |
| `GET /organizations` | User organizations | Authenticated |
| `POST /organizations` | Create organization | Authenticated |
| `GET /organizations/{orgId}/members` | List members | Owner/manager |
| `POST /organizations/{orgId}/members` | Invite member / assign role | Owner |
| `PATCH /organizations/{orgId}/members/{userId}` | Change role/status | Owner |

## Properties, units, and tenants

| Method / path | Purpose | Access |
|---|---|---|
| `GET,POST /properties` | List/create properties | Viewer / Manager |
| `GET,PATCH,DELETE /properties/{id}` | Read/update/archive property | Viewer / Manager |
| `GET,POST /properties/{id}/units` | List/create units | Viewer / Manager |
| `GET,PATCH /units/{id}` | Read/update unit | Viewer / Manager |
| `GET,POST /tenants` | Search/list (`q`, `status`, `property_id`, cursor) / create | Viewer / Manager |
| `GET,PATCH /tenants/{id}` | Profile read/update | Viewer / Manager |
| `GET,POST /tenants/{id}/contacts` | Contacts | Viewer / Manager |
| `GET,POST /tenants/{id}/documents` | List/create upload request | Viewer / Manager |
| `GET /documents/{id}/download` | Short-lived signed download URL | Viewer with scoped access |

## Leases, billing, and payments

| Method / path | Purpose | Access |
|---|---|---|
| `GET,POST /leases` | Query/create lease; filter status/end-date/unit/tenant | Viewer / Manager |
| `GET,PATCH /leases/{id}` | Read/update terms (requires `If-Match`) | Viewer / Manager |
| `POST /leases/{id}/activate` | Activate validated pending lease | Manager |
| `POST /leases/{id}/renewals` | Create renewal proposal/lease | Manager |
| `GET,POST /leases/{id}/notices` | List/create move-out or renewal notices | Viewer / Manager |
| `GET /rent-charges` | Charges by period/status/lease | Viewer |
| `POST /rent-charges/{id}/waive` | Record authorized adjustment | Manager/Accountant |
| `GET,POST /payments` | Query/record payment and allocations | Viewer / Accountant |
| `GET /payments/{id}` | Payment and allocations | Viewer |
| `POST /payments/{id}/receipt` | Queue/send receipt | Accountant/Manager |
| `GET,POST /leases/{id}/deposit-transactions` | View/record deposit receive/deduction/refund | Viewer / Accountant |

## Worklists and actions

| Method / path | Purpose |
|---|---|
| `GET /dashboard/tenant-summary` | Counts, monthly expected/collected/outstanding, deposits. |
| `GET /renewals?within_days=180` | Expiring active leases, ordered by end date. |
| `GET /vacating` | Leases with active move-out notices and settlement state. |
| `POST /leases/{id}/reminders` | Queue rent reminder, idempotent by type/period/channel. |
| `POST /leases/{id}/renewal-notices` | Queue renewal notice. |
| `POST /leases/{id}/documents/upload-url` | Validate metadata and issue short-lived upload URL. |

## Error contract

Use RFC 9457 problem details (`type`, `title`, `status`, `detail`, `instance`, `errors[]`). Return `400` invalid shape, `401` unauthenticated, `403` unauthorized, `404` not found/not in organization, `409` occupancy/concurrency/idempotency conflict, `422` business validation error, and `429` rate limited. Require idempotency keys for payment, notification, and refund mutations.
