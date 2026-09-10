# GameStore — agent instructions

An e-commerce API for a board game store, built with Clean Architecture. This
repository is the working environment for an analysis-and-design exercise: most
of the work here is *deciding what to build*, and only then building it.

## Repository map

| Path | What lives there |
| --- | --- |
| `src/Backend/` | .NET 10 solution (`GameStore.slnx`), four layers |
| `src/Backend/src/GameStore.Domain/` | Entities, value objects, domain events, domain services |
| `src/Backend/src/GameStore.Application/` | Use cases, repository interfaces, event handlers |
| `src/Backend/src/GameStore.Infrastructure/` | EF Core, repositories, event publishing, email mock |
| `src/Backend/src/GameStore.Presentation/` | Minimal API endpoints |
| `src/Frontend/` | Angular client: catalogue, Game detail, Shopping Basket, Checkout |
| `docs/glossary.md` | The ubiquitous language. Binding. |
| `docs/adr/` | Architecture decision records. Binding. |
| `docs/specs/` | One folder per feature: `NNN-slug/spec.md` |
| `docs/design/` | Accepted UI mockups, kept as documentation |

## Domain language

Use the terms in [docs/glossary.md](docs/glossary.md) exactly. Never introduce a synonym
for a term listed there. If a concept is missing, propose an addition
to the glossary before writing any code.

## Architecture

Every decision that shapes this codebase is recorded in [docs/adr/](docs/adr/). Read the
relevant ADR before proposing a change to the pattern it describes. If you believe an
ADR should change, say so and propose a new ADR that supersedes it — do not quietly
deviate. ADRs are immutable once accepted.

Layer-specific rules live in `.github/instructions/` and are applied automatically
based on the files being edited. Each one links the ADRs that justify it.

## Skills

The analysis workflow below is packaged as skills in `.github/skills/`, each with its
templates as assets in the same folder. Load the one that fits the task; the user can
also invoke them directly with `/`.

## How work flows here

Analysis precedes implementation, and each step produces a written artifact that the
next step consumes:

1. **Interview** — clarify the request until the ambiguity is gone (`/interview`), and
   draft an ADR if an architectural choice surfaced
2. **Glossary** — name the new concepts before modelling them (`/glossary-audit`)
3. **Spec** — write `docs/specs/NNN-slug/spec.md` (`/spec`)
4. **Mockups** — compare UI options, keep the winner in `docs/design/` (`/mockups`)
5. **Issues** — split the spec into independently workable issues (`/spec-to-issues`)
6. **Implement** — one issue at a time, grounded in the spec and the ADRs

During steps 1–5, **do not write application code.** Producing documents, mockups and
issues is the work. If you catch yourself opening a `.cs` file to edit it during
analysis, stop and ask.

## Grounding rules

- Prefer reading the codebase over asking. If a question can be answered by exploring
  `src/`, explore `src/` instead of asking the user.
- Do not invent requirements. Anything you cannot determine from the repository or from
  the user belongs under **Open questions** in the spec.
- Cite your evidence. When you assert how something currently works, reference the file
  and line.

## Build and run

```bash
dotnet build src/Backend/GameStore.slnx
dotnet run --project src/Backend/src/GameStore.Presentation
```

The API listens on `http://localhost:5038`. The SQLite database is created on startup
(`EnsureCreatedAsync`) — there are no migrations. Deleting `gamestore.db` resets it.

There is no test project yet. If a spec needs verification steps that require tests,
raise that as an open question rather than assuming a framework.

## Validation requirements 
After changing C# code: 
1. Run `dotnet build --no-restore`. 
2. Read all compiler and analyzer diagnostics. 
3. Fix diagnostics introduced by the change. 
4. Do not suppress rules merely to make the build pass. 
5. Run the relevant tests. 
6. Repeat validation after the final edit.
