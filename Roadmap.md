# Roadmap

What's done, what's next.

## v0.1 — Skeleton

- [x] Project structure (src/, tests/)
- [x] ASP.NET Core 8 Minimal API
- [x] Health and info endpoints
- [x] PostgreSQL connection with health check
- [x] Database migrations with DbUp
- [x] `deliveries` table

## v0.2 — MVP: working delivery

- [ ] `POST /api/v1/webhooks` — accept webhook, store, return 202 with id
- [ ] `GET /api/v1/webhooks/{id}` — delivery status
- [ ] `GET /api/v1/webhooks` — list deliveries
- [ ] Background worker that picks up pending deliveries
- [ ] HTTP delivery with timeout
- [ ] Retry with backoff (5s → 30s → 2m → 10m → 1h)
- [ ] Max attempts, then mark as `dead`
- [ ] Graceful shutdown for the worker
- [ ] Unit and integration tests
- [ ] Full Docker Compose
- [ ] GitHub Actions CI
- [ ] README with examples

## v0.3 — Reliability

- [ ] HMAC signatures (Stripe-style)
- [ ] Idempotency-Key support
- [ ] Dead letter queue
- [ ] Prometheus metrics on `/metrics`

## v0.4 — Integration

- [ ] Reads config from `config-service` via Go client
- [ ] Go client for `webhook-service`
- [ ] Publishes delivery events to `audit-log-service`

## v1.0 — Production-ready

- [ ] Multiple workers with distributed locks (`SELECT FOR UPDATE SKIP LOCKED`)
- [ ] Secret rotation without downtime
- [ ] Outgoing rate limiting per destination

## Not planned

- Message queue (Kafka, RabbitMQ) — the database is enough at this scale
- UI for viewing deliveries — API is enough
- Multi-tenancy — not a commercial product