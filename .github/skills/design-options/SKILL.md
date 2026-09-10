---
name: design-options
description: Compare three designs for an architectural decision, recommend one, and draft the ADR. Use when there is a real choice to make about how something should be built — persistence, modelling, an extension point — or when asked to weigh options, compare approaches, or write an architecture decision record.
---

# Design options

A decision worth an ADR is a decision where reasonable people would disagree. If there
is only one sensible answer, say so and skip the ceremony.

Read [docs/glossary.md](../../../docs/glossary.md) and the ADRs in
[docs/adr/](../../../docs/adr/) before you propose anything. An option that quietly
violates an accepted ADR is not an option, it is a mistake.

## Propose three designs

For each one:

**How it works.** Concretely, in terms of this codebase. Name the types, the files and
the layers that change. Use glossary terms.

**What it costs us over the next six months.** Not what it costs to type — what it makes
harder. What does every future change in this area have to pay? Which future
conversation does this option force, and which does it avoid?

**What breaks it.** The change in requirements that turns this from a good decision into
a bad one. If you cannot name one, you have not understood the option well enough to
recommend against it.

**Which ADRs it upholds, and which it would supersede.** By number.

At least one option must be the boring one: the smallest change that could work. If the
boring option is genuinely viable, say so plainly rather than burying it.

## Recommend

Recommend one, and say what separated it from the runner-up. If a single constraint
decided it, name that constraint.

Then: **say what would change your recommendation.** Which fact, if the user told you it
were true, would make you pick differently? Make it specific enough to go and check.
This is the most useful sentence in the answer.

## Draft the ADR

Copy [assets/adr-template.md](assets/adr-template.md) to
`docs/adr/NNNN-<slug>.md`, using the next free number, and fill it in.

- `Status: proposed` — you do not accept your own ADR. The user does.
- **Considered options** carries all three, with their real reasons for rejection. A
  strawman in this section makes the whole record worthless to whoever reads it in two
  years.
- **Context** is written in the present tense of this moment and describes the forces,
  not the answer. Never revise it later.
- **Consequences** must contain costs. An ADR whose consequences are all upside is not
  finished.

If this supersedes an existing ADR, say so in the new one, and change **only the status
line** of the old one to `superseded by ADR-NNNN`. Accepted ADRs are otherwise
immutable.

Add the new row to the index table in [docs/adr/README.md](../../../docs/adr/README.md).

## Do not

Write application code. This skill produces a decision and a document; implementation
comes later, from a spec and an issue.
