This project is being built incrementally to demonstrate backend system design concepts such as event-driven architecture, scalability, and fault tolerance.

## Architecture

Client → API → Kafka → Consumer → PostgreSQL

## Key Concepts

- Event-driven architecture
- Asynchronous processing
- Idempotency
- Retry + DLQ


## Current Status

This project is actively being developed to simulate a production-grade event-driven notification system.

### Completed
- API service with PostgreSQL persistence
- Clean architecture separation (API, Domain, Infrastructure)
- Entity modeling and database integration

### In Progress
- Kafka integration for asynchronous event processing
- Consumer service for notification handling

### Planned
- Redis for idempotency and rate limiting
- Retry mechanisms and Dead Letter Queue (DLQ)
- Observability and logging improvements