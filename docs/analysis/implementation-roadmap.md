# Implementation roadmap and dependencies

## Preconditions

`docs/MASTER_SPEC.md` is missing. Resolve the open product/policy decisions in `business-rules.md` before implementation; they affect schema, permissions, billing, document storage, and integrations.

## Phased plan

| Phase | Deliverable | Depends on |
|---|---|---|
| 0. Product baseline | Approved master specification, roles, policies, acceptance criteria, nonfunctional requirements | User/product approval |
| 1. Foundation | Repository setup, CI, configuration/secrets management, migrations, observability, authentication skeleton | Phase 0 |
| 2. Data and access | PostgreSQL schema, organization scoping, RLS/service access strategy, audit events, seed fixtures | Phase 1 |
| 3. Core API | Properties/units, tenants/contacts, leases/notes; validation, pagination, OpenAPI tests | Phase 2 |
| 4. Billing | Charge schedule, payment/allocation ledger, overdue calculations, receipts, dashboards | Phase 2 and billing policy |
| 5. Workflows | Renewal, move-out, deposit settlement/refund, notification outbox/worker | Phases 3–4 and provider decisions |
| 6. Documents | Private object storage, upload/download flow, malware scanning, retention/audit | Phase 2 and storage/security policy |
| 7. Frontend | Accessible components and pages matching approved template intent; loading/error/empty/permission states | APIs in Phases 3–6 |
| 8. Quality and release | Unit/integration/e2e/security/accessibility/performance tests; backups, migration runbook, deployment | All prior phases |

## Critical dependency order

```text
master specification -> data/access model -> core API -> billing/workflows -> UI
                                  \-> document/security platform -----/
```

## Acceptance checkpoints

- Phase 0: product owner approves unresolved rules and data classifications.
- Phase 2: tenant-isolation, overlapping-lease, audit, and migration tests pass.
- Phase 4: charge/payment allocation and time-zone boundary tests pass.
- Phase 6: unauthorized document access and malicious upload tests pass.
- Phase 7: keyboard, screen-reader, responsive, and API failure paths are verified.
- Phase 8: backup restore and production-readiness review complete.
