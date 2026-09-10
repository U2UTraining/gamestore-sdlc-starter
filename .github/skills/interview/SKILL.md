---
name: interview
description: "Use when a feature request, change or idea is not yet specified enough to build — including 'grill me', 'interview me', 'help me work out what I need', or 'I want to add X'. Asks one question at a time, writes no code, and drafts an ADR if an architectural decision surfaces."
---

Interview me relentlessly about every aspect of this plan until we reach a shared 
understanding.

1. If a question can be answered by exploring the codebase, explore the codebase instead.

2. One question at a time.

3. Propose options and a recommendation if useful. Options are about the business rule, never the implementation.

4. Work the seams: collisions with glossary terms; ADRs the feature strains; cancellation, refund, repetition, and acting on something since deleted; timing and timezone; currency and rounding; who is allowed and what an administrator overrides; how we would know afterwards that it works.

5. Refuse vague answers.

6. Stop when the consequential unknowns run out, or the moment the user says so. Summarise: what was agreed, what is still open and who owns each, which glossary terms and ADRs this collides with.

7. Draft an ADR only if an architectural choice surfaced — one that constrains code which does not exist yet. Most do not; say so and move on. If one did, copy [assets/adr-template.md](assets/adr-template.md) to `docs/adr/NNNN-slug.md` at the next free number, `Status: proposed`, and add its row to the index. Give each option how it works in this codebase, what it costs over the next six months, and what breaks it; one must be the smallest change that could work; keep the rejected options with their real reasons; put actual costs in Consequences. Superseding an accepted ADR means saying so in the new one and changing only the status line of the old.

8. Offer the `spec` skill. Do not start writing it.
