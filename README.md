# GameStore

An e-commerce API for a board game store, built with Clean Architecture on .NET 10.

This repository is the starting point for an **analysis and design** exercise. The
code that exists is deliberately small and deliberately imperfect: the point is to
practise working out *what to build* — with an AI assistant — before building it.

## Getting started

Both halves have to be running: the client talks to the API over HTTP.

### The API

```bash
dotnet build src/Backend/GameStore.slnx
dotnet run --project src/Backend/src/GameStore.Presentation
```

The API listens on `http://localhost:5038`. A SQLite database is created on first run
and, in Development only, seeded with four Publishers, twelve Games and one Customer
with id 1. Deleting `gamestore.db` resets it and it is seeded again on the next start.
See [src/Backend/README.md](src/Backend/README.md) for how a request flows through the layers.

### The Angular client

In a second terminal:

```bash
cd src/Frontend
npm install
npm start
```

The client runs on `http://localhost:4200` and expects the API on `http://localhost:5038`
— that address is a constant in `src/Frontend/src/app/core/api.config.ts`, together with
the hardcoded `CUSTOMER_ID`, because there is no authentication. `npm run build` produces
a production build in `src/Frontend/dist/`.

The API allows any origin in Development, so no proxy is needed. If the catalogue shows
"The GameStore API could not be reached", the API is not running.

## What is already set up

| | |
| --- | --- |
| [AGENTS.md](AGENTS.md) | How the coding agent should behave in this repository |
| [docs/glossary.md](docs/glossary.md) | The ubiquitous language — binding for all code and docs |
| [docs/adr/](docs/adr/) | Seven architecture decision records covering the existing design |
| [.github/skills/](.github/skills/) | Six skills: `interview`, `glossary-audit`, `design-options`, `spec`, `mockups`, `spec-to-issues` |
| [.github/instructions/](.github/instructions/) | Layer-scoped rules, applied automatically per file |

## The workflow

Each step produces an artifact the next step consumes. Full description in
[docs/README.md](docs/README.md).

```
 interview  →  glossary  →  design options  →  spec  →  mockups  →  issues  →  code
     ↓             ↓              ↓             ↓          ↓           ↓
 (shared      docs/         docs/adr/     docs/specs/  docs/design/  GitHub
  understanding) glossary.md  NNNN-*.md    NNN-*/spec.md  *.html      issues
```

## Layout

```
src/Backend/            .NET solution
  src/GameStore.Domain/          entities, value objects, domain events, domain services
  src/GameStore.Application/     use cases, repository interfaces, event handlers
  src/GameStore.Infrastructure/  EF Core, repositories, messaging
  src/GameStore.Presentation/    minimal API endpoints
src/Frontend/           Angular client (four screens, Tailwind + DaisyUI)
  src/app/core/                  API config, models, services holding state in signals
  src/app/shared/                the Money pipe
  src/app/features/              game-catalogue, game-detail, shopping-basket, checkout
docs/                   glossary, ADRs, specs, accepted mockups
```
