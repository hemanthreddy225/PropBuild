# Business rules, edge cases, and hardcoded prototype behavior

## Domain rules

1. An organization owns all properties, units, tenants, leases, financial records, documents, and notifications; cross-organization access is forbidden.
2. A unit may have at most one active or pending lease covering any calendar day. Past leases are retained.
3. Lease start must not exceed end; rent/deposit are non-negative integer minor units; due day is 1–28 to avoid invalid calendar dates.
4. An active lease generates one rent charge per billing period. A charge is overdue only after its due date/grace policy and if its outstanding balance is positive. Do not store `overdue` as the tenant's authoritative status.
5. Partial, late, reversed, and overpaid payments require immutable allocation/audit records. Payments must never be silently edited or deleted.
6. A lease expiry worklist uses a configurable window (the template presents 180 days); expired/terminated leases are excluded unless explicitly requested.
7. A vacating record requires notice date and intended move-out date. Deposit refunds require settlement of unpaid charges and logged deductions/approval.
8. Documents require authorization, malware scanning, allowed media type/size policy, private storage, and expiring download links.
9. Notifications are queued, idempotent, auditable, and only sent through verified communication channels with consent/policy handling.

## Important edge cases

- Time zones at midnight; define organization-local period generation and due-date evaluation.
- Leap years, varying months, lease starts/ends mid-month, proration, rent escalation, and renewal overlaps.
- Multiple occupants/co-tenants, tenant re-use across leases, shared contacts, duplicate phone/email, and missing email/phone.
- Payment received before/after due date, partial payment, reversal/chargeback, duplicate provider callback, and payment applied to the wrong charge.
- Concurrent lease edits, payment posting while a refund is calculated, and a unit becoming unavailable between form load and save.
- Document upload succeeds but metadata finalization fails; expired signed URLs; revoked membership.
- Never inject tenant-entered content into HTML without escaping/sanitizing.

## Template-only values, simulated actions, and placeholders

| Category | Observed value / behavior | Production treatment |
|---|---|---|
| Seed data | Four named people, three named Bengaluru properties, contacts, employment, documents, payments | Demonstration data only; do not migrate as real records. |
| Currency/locale | `₹`, `en-IN`, INR-style figures | Configure per organization; store ISO code. |
| Date examples | Payment display `Mar 2026`; lease dates 2023–2027 | Derive from data and organization clock. |
| Status | `active`, `overdue`, `expiring`, `vacating` stored on tenant | Derive workflow/reporting state from lease, charges, and notice records. |
| Thresholds | Renewal display 180 days; stats text says next 90 days | Resolve and configure a single policy. |
| Due days | Form limits choices to 1st, 5th, 10th, 15th | Use validated configurable due day (1–28). |
| IDs | Added tenant uses `Date.now()` | Server UUID only. |
| Defaults | Missing name becomes “New Tenant”; dates default to 2026-01-01/2027-01-01; rent/deposit 0 | Reject or require explicit user input according to policy. |
| Hardcoded property select | Three property names in form/sidebar | Load authorized properties/units from API. |
| Simulated actions | Send notice/reminder/receipt, upload, download use `alert()` | Replace with authenticated API + queued job/storage flow. |
| No-op | Initiate Refund button has no action | Implement deposit settlement/refund workflow. |
| Persistence | All state is in JavaScript memory; refresh loses edits | API/database persistence. |
| Asset placeholders | Emoji document/type icons; no file URLs | Use uploaded metadata/type icons and signed files. |

## Decisions needed from missing master specification

- Billing frequency, grace period, late fees, proration, escalation, and GST/tax/invoicing requirements.
- Supported payment methods/provider and reconciliation process.
- Notification channels, consent wording, and recipient rules.
- Document retention, required verification documents, storage region, and privacy obligations.
- Tenant portal, co-tenant/guarantor needs, approval flows, and exact role permissions.
