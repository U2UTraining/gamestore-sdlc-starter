---
name: glossary-audit
description: Inventory the domain nouns across the codebase, find synonyms and collisions, and record the findings in docs/glossary.md. Use when asked to audit the domain language, check naming consistency, build or extend the ubiquitous language, or before starting a feature that touches layers outside the domain.
---

# Glossary audit

The domain layer is the authority on what words mean here. The Application,
Infrastructure and Presentation layers have never been checked against it. This skill
finds the drift.

Read [docs/glossary.md](../../../docs/glossary.md) first.

## 1. Inventory

Inventory the domain nouns in the scope the user gave you, or in `src/` if they gave
none. Include:

- type names
- property and field names
- method-name fragments (`Rename`, `AddItem`, `SetVipStatus`)
- JSON field names in the endpoint request records and response objects
- column and table names in the EF Core configurations

For each term, list the files that use it.

Ignore purely technical vocabulary — `Builder`, `Options`, `Context`, `Async`,
`CancellationToken`, `Request`, `Response`. You are looking for words that carry
business meaning.

## 2. Analyse

Produce three lists.

**Synonyms.** Different words that appear to mean the same thing. Group them, and say
which one the glossary already blesses — or, if none is listed, which you would pick
and why. The domain layer's word wins by default.

**Collisions.** The same word used for two different concepts. These matter most, and
they are the ones a reader will never notice on their own. Show both usages with file
references and describe the two meanings precisely enough that the difference is
obvious.

**Undocumented.** Business terms used in the code that are not in the glossary at all.

For every finding, cite `file:line`. Do not report a finding you have not read. A
plausible-sounding collision that does not exist wastes more time than a missed one.

Rank by consequence, not by count. A collision in a term that appears in the API
contract is worth ten inconsistencies in a private field name.

## 3. Record

Append your findings to the **Under review** section of
[docs/glossary.md](../../../docs/glossary.md), replacing the `_(empty)_` placeholder.
Use the format in [assets/findings-table.md](assets/findings-table.md).

Propose a resolution for each finding, but **do not apply it**. Renaming is a separate
change with its own decision behind it, and it touches files this audit has no mandate
over.

Do not modify the catalogue or the "Words we do not use" table. Those are agreed;
changing them is a conversation, not an audit.

Do not change any code.

## Then

Tell the user which findings block the work they are about to start, and which are
merely untidy. Offer to resolve the blocking ones as a separate, explicit change.
