# UI and functionality inventory

## Source and scope

This inventory is derived from `tenant-management.html` (a 48,776-byte, single-file prototype). `docs/MASTER_SPEC.md` was not present when this analysis was made. No local image, JavaScript, stylesheet, or document assets accompany the template. Google Fonts loads **Playfair Display** and **DM Sans** externally.

## Global shell

| Area | UI / behavior |
|---|---|
| Sidebar | Brand, Tenant Management subtitle, overview navigation badges, property filters. |
| Main area | One visible page at a time: tenants, renewals, payments, or vacating. |
| Overlay detail drawer | Tenant header and Overview, Payments, Documents tabs. Dismissible by close button or backdrop. |
| Add/edit modal | Tenant, lease, emergency-contact, and notes fields; dismissible by cancel or backdrop. |
| Visual system | Desktop two-column shell (260px sidebar), warm neutral palette, gold primary action, status/payout colours, responsive rules in embedded CSS. |

## Screens and functions

| Screen | Content | Template behavior |
|---|---|---|
| All Tenants | Four aggregate cards, overdue banner, status filters, search, tenant rows | Filter/search happen in browser memory; row/View opens detail; Edit opens modal. |
| Lease Renewals | Leases ending in 1–180 days, sorted earliest first | View works locally; Send Notice is an alert only. |
| Payment Status | March collection cards and one row per tenant | Calculated from the first seeded payment; Send Reminder is an alert only. |
| Vacating Soon | Vacating tenants and deposit | Currently no seeded tenant is vacating; Initiate Refund has no handler. |
| Detail: Overview | Lease progress, contact, finance, emergency contact, notes | Read-only display plus local edit/reminder. |
| Detail: Payments | Six-month history, payment stats | Send Receipt is an alert only. |
| Detail: Documents | Document metadata, download/upload controls | Both actions are alerts only. |
| Tenant form | Name, phone, email, occupation, property, rent, dates, deposit, due day, emergency contact, notes | Adds/edits volatile in-memory data; no validation, persistence, or server call. |

## Present data model shown

Four seed tenants span three named properties. Each has one current lease, rent/deposit, due day, emergency contact, notes, up to six payment entries, and document metadata. UI statuses are `active`, `overdue`, `expiring`, and `vacating`; payment statuses are `paid`, `late`, and `pending`.

## Accessibility and UX gaps

- Clickable `div`/`span` controls lack keyboard semantics and accessible names.
- Modal/drawer lack focus trap, Escape handling, focus restoration, and dialog ARIA semantics.
- Colour is an important status indicator; semantic text remains but contrast must be verified.
- Native validation is largely bypassed; fields are neither required nor constrained.
- No loading, error, authorization, empty-data, pagination, or mobile-navigation behavior is specified beyond prototype empty states.
