---
name: interview
description: "Use when a feature request, change or idea is not yet specified enough to build — including 'grill me', 'interview me', 'help me work out what I need', or 'I want to add X'. Asks one question at a time and writes no code."
---

1. **Read first, ask second.** [docs/glossary.md](../../../docs/glossary.md), the index in [docs/adr/README.md](../../../docs/adr/README.md), and the parts of `src/` the request touches. Never ask what the repository can answer. Do not report what you read — start asking.

2. **One question at a time, then wait.** A numbered list of eight gets one answer and silence on the rest.

3. **Every question carries options and a recommendation.** State what you found in the code, offer two or three concrete answers, recommend one in a line, and say what would change your mind. "Should discounts combine?" makes the user do the work. This does not:

   > `Order.ApplyDiscount` replaces the discount rather than adding to it, so a second source would silently erase the first. Either (a) they stack, (b) the larger wins, or (c) the new one is refused while a discount already exists. I would pick (b): it is the only one a Customer can predict without reading the rules. That changes if Finance needs each source itemised on the invoice.

4. **Options are about the business rule, never the implementation.** What the answer could be, not how it would be built. No code, no type sketches, no file layouts.

5. **Work the seams**: collisions with glossary terms; ADRs the feature strains; cancellation, refund, repetition, and acting on something since deleted; timing and timezone; currency and rounding; who is allowed and what an administrator overrides; how we would know afterwards that it works.

6. **Refuse vague answers.** "It should be configurable" is a deferral — ask who configures it, when, and what happens if nobody ever does.

7. **Stop** when the consequential unknowns run out, or the moment the user says so. Summarise: what was agreed, what is still open and who owns each, which glossary terms and ADRs this collides with. Then offer the `spec` skill — do not start writing it.
