# docs

The written output of analysis and design. Everything here is meant to be read by both
people and the coding agent, and the agent is instructed to treat the glossary and the
ADRs as binding.

| | |
| --- | --- |
| [glossary.md](glossary.md) | The ubiquitous language. Binding for code, docs, issues and UI copy. |
| [adr/](adr/) | Architecture decision records. Immutable once accepted. |
| [specs/](specs/) | One folder per feature: `NNN-slug/spec.md` |
| [design/](design/) | Accepted UI mockups, kept as the reference for implementation |

## The workflow

Each step produces an artifact that the next step consumes. That chain is the point: by
the time an issue is written, everything needed to work on it is already decided and
written down somewhere it can be linked to.

| Step | Skill | Produces | Consumes |
| --- | --- | --- | --- |
| 1. Interview | `interview` | Shared understanding | The request, the codebase |
| 2. Glossary | `glossary-audit` | Terms in `glossary.md` | The codebase |
| 3. Design options | `design-options` | An ADR in `adr/` | Glossary, existing ADRs |
| 4. Spec | `spec` | `specs/NNN-slug/spec.md` | Glossary, ADRs |
| 5. Mockups | `mockups` | Options to compare; winner to `design/` | Spec |
| 6. Issues | `spec-to-issues` | GitHub issues | Spec, ADRs, mockups |
| 7. Implement | — | Code | Everything above |

Each is a skill in [`.github/skills/`](../.github/skills/). The agent loads one when the
task calls for it, or you can invoke it directly by typing `/` in chat. The templates
they fill in are assets alongside them, so the template and the instructions for using
it stay together.

Steps 1 to 6 produce no application code. That is not a limitation of the exercise —
it is the work.

## Why the agent obeys these files

Nothing here is loaded automatically just by existing. The wiring is:

- **[AGENTS.md](../AGENTS.md)** at the repository root is always in context, and it
  points at the glossary and the ADR folder.
- **[.github/instructions/](../.github/instructions/)** holds globbed rules that are
  applied based on which files are being edited, and each one links the ADRs that
  justify it.
- **[.github/skills/](../.github/skills/)** holds the six skills above, each with its
  templates as assets in the same folder.

When you accept a new ADR, check whether an instruction file needs to change with it.
Documentation the agent is not pointed at is documentation the agent will not read.

## What is deliberately not here

The **product-owner brief** — the answer sheet for the interview exercise — must be kept
**outside this repository**. Anything inside the workspace can be read by the coding
agent, and an agent that can read the answers will not ask the questions. Keep it in a
separate folder, or on paper.
