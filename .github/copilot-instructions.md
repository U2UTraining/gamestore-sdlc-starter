# GameStore

This is an e-commerce API for a board game store, built with Clean Architecture, with an Angular client in `src/Frontend/`.

| Path | What lives there |
| --- | --- |
| `docs/glossary.md` | The ubiquitous language. Binding. |
| `docs/adr/` | Architecture decision records. Binding. |
| `docs/specs/` | One folder per feature: `NNN-slug/spec.md` |
| `docs/design/` | Accepted UI mockups, kept as documentation |

## Domain language

Use the terms in [docs/glossary.md](../docs/glossary.md) exactly — in code, UI copy, JSON
and docs. Never introduce a synonym for a term listed there. If a concept is missing,
propose an addition to the glossary before writing any code.

## Architecture

Every decision that shapes this codebase is recorded in [docs/adr/](../docs/adr/). Read the
relevant ADR before proposing a change to the pattern it describes. If you believe an
ADR should change, say so and propose a new ADR that supersedes it — do not quietly
deviate. ADRs are immutable once accepted.

Layer-specific rules live in `.github/instructions/` and are applied automatically
based on the files being edited.

## Build and run

```bash
dotnet build src/Backend/GameStore.slnx
dotnet run --project src/Backend/src/GameStore.Presentation
```

The API listens on `http://localhost:5038`.

## Testing

There is no test project, backend or frontend. Do not add one, or a test framework, as a
side effect of another change. If work needs tests, raise it as an open question — how
to test is an ADR.
