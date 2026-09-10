# ADR-0008: An Order carries a single discount amount

- **Status:** accepted
- **Date:** 2026-09-10
- **Applies to:** domain

## Context

There is exactly one way to get money off an order: be a VIP, and check out at the
weekend. `OrderPricingService` applies 10% and that is the entire discounting story.

An Order therefore needs somewhere to record how much was taken off, so that Subtotal,
Discount and Final Total add up and can be shown on a confirmation.

The question is how much structure to build now for a business that currently has one
rule.

## Considered options

### Option A: No discount field; store only the Final Total

Smallest model. Loses the ability to show the customer what they saved, which is the
main commercial reason for having a discount in the first place.

### Option B: A single `Money Discount` on the Order

Subtotal, one Discount, Final Total. Three fields, trivially checkable arithmetic, maps
to one database column. Assumes there will only ever be one thing to subtract.

### Option C: A collection of discount lines, each with a reason and an amount

`IReadOnlyList<OrderAdjustment>` where each entry names its source. Supports several
discounts, gives a complete audit trail, and answers "why is this order cheaper?"
without archaeology. Costs a child entity, an EF Core configuration, a change to the
response shape of every endpoint that returns an order, and a rule for how the
adjustments interact.

## Decision

Option B. A single `Money Discount` property on `Order`, set through
`ApplyDiscount(Money)`.

`ApplyDiscount` **replaces** the discount rather than accumulating it, and validates
that the currency matches the subtotal, that the amount is not negative, and that it
does not exceed the subtotal. `FinalTotal` is recalculated as `Subtotal - Discount`.

We chose Option B over Option C on the grounds that there is one discount rule, there
is no second one on the roadmap, and `OrderAdjustment` would be a speculative
generalisation today.

## Consequences

- The pricing arithmetic is three numbers and is obvious to read and to verify.
- One column, no child table, no extra EF Core configuration.
- The order confirmation can show what the customer saved, but not *why*. With one rule,
  the reason is inferable. With two, it is not.
- **`ApplyDiscount` replacing rather than accumulating is a trap.** The second caller to
  come along will silently erase the first caller's discount, and nothing in the type
  signature warns them.
- This decision holds only while there is exactly one source of discount. The moment a
  second one appears — a voucher, a bundle price, a loyalty redemption, a staff
  discount — this model cannot represent the result honestly, and this ADR must be
  superseded rather than worked around.

## Revisit when

A second source of discount is proposed. That is the trigger; do not wait for a third.
