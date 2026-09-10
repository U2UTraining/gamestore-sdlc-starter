---
name: spec
description: Write a feature spec grounded in the glossary and the ADRs, using the repository's six-section template. Use when asked to write a spec, specify a feature, turn an interview into requirements, or produce something an implementation can be split from.
---

# Spec

A spec exists so that implementation can be split up and handed off without the person
doing the work having to reconstruct the reasoning.

Write it to `docs/specs/NNN-slug/spec.md`, taking the next free number. Ask the user for
the slug if it is not obvious.

Copy [assets/spec-template.md](assets/spec-template.md) and fill it in. The six sections
are fixed.

## Before you write

Read [docs/glossary.md](../../../docs/glossary.md), the ADRs in
[docs/adr/](../../../docs/adr/), and the parts of `src/` this feature touches.

A requirement that contradicts how the system already works is worse than a missing one.
Where the spec depends on current behaviour, cite `file:line` so the reader can check
it.

## The three rules

**Do not invent requirements.** Anything you cannot determine from the repository or
from the conversation goes under **Open questions**. A plausible guess written as a
requirement is worse than a blank, because nobody will ever go back and check it.

**Ground every term in the glossary.** Every noun in the Requirements section is either
a glossary term or a mistake. If the feature needs a concept the glossary does not have,
do not invent a word for it — raise it as an open question and note that the glossary
needs the entry first.

**Ground every pattern in an ADR.** Link the ADR rather than restating it. If the
feature cannot be built within the accepted ADRs, name the one that must be superseded
and stop there — designing the replacement is the `design-options` skill, not this one.

## What each section has to satisfy

**Goal** — one sentence. Names who benefits. Describes the outcome, not the work.

**Scope** — the out-of-scope list may not be empty. If you genuinely cannot think of
anything that is out of scope, the goal is too vague; say so rather than padding it.

**Requirements** — numbered, independently checkable, one idea each. Split any
requirement containing "and".

**Acceptance criteria** — observable behaviour, grouped under the requirement number it
verifies. Someone who has not read the spec should be able to tell whether each one
holds. Include the unhappy paths that came out of the interview, not only the happy
path.

**Constraints** — stack, patterns, performance, security, compatibility. An ADR link
per architectural constraint.

**Open questions** — a table, and every row has an owner. A question with no owner will
not get answered. This section being empty is a claim that nothing is undecided, which
is almost never true.

## Do not

Write code. Not even a type sketch. The spec describes behaviour that can be checked
from outside; the moment it contains an implementation, reviewers start reviewing the
implementation instead of the requirements.

## Then

Report which requirements are blocked by an unanswered open question. Those cannot
become issues yet. Offer the `spec-to-issues` skill for the rest.
