# ADR-0007: Money carries its currency; no conversion at write time

- **Status:** accepted
- **Date:** 2026-09-10
- **Applies to:** domain

## Context

GameStore sells in EUR, USD and JPY. A price is meaningless without its currency, and
adding two amounts in different currencies is not an arithmetic operation — it is a
business decision involving a rate, a timestamp, and a rounding rule.

Exchange rates move. An order total computed with today's rate is not reproducible
tomorrow unless we record the rate we used. Recording rates properly means storing them
per order, which is real work.

Nobody has yet asked for a basket containing games priced in different currencies.

## Considered options

### Option A: Store everything in a base currency, convert on display

Arithmetic is trivial and totals are always addable. Every stored price becomes a
derived number that no longer matches what the publisher set, and rounding error
accumulates in a place nobody can see.

### Option B: Money carries its currency, and mixed-currency arithmetic is forbidden

Prices stay exactly as they were set. The system refuses to answer questions it cannot
answer correctly. Costs us the ability to have a mixed basket at all.

### Option C: Money carries its currency, and conversion happens automatically on demand

Convenient and the most dangerous of the three: a conversion buried inside an `operator +`
means an implicit business decision with no visible timestamp or rate.

## Decision

Option B.

- `Money` is a record of `decimal Amount` and `CurrencyName Currency`. There is no
  currency-less money. `Money.Zero` is EUR by convention.
- `operator +` and `operator -` require matching currencies. Today this is a
  `Debug.Assert`, which means it is **not enforced in a release build** — a known
  weakness, deliberately left visible.
- A Shopping Basket must be single-currency. `OrderPricingService.Calculate` inspects
  the distinct currencies across the basket lines and throws `DomainException` on a
  mixed basket. This is the real enforcement point.
- An Order's Subtotal, Discount and Final Total are all in the same currency, and
  `Order.ApplyDiscount` rejects a discount whose currency does not match.
- `CurrencyName` is a small enum, not a lookup table. The `Currency` entity carries a
  `ValueInEuro` rate and exists for future conversion work; nothing uses it yet.
- `ICultureToCurrencyService` exists to map a request culture to a currency. It is
  `internal` and currently unused.

## Consequences

- A price means exactly what the publisher set. There is no hidden conversion anywhere.
- A customer cannot buy a EUR game and a USD game in one order. Nobody has complained,
  and the day somebody does, this ADR is what gets superseded.
- Reporting across currencies is not possible in the domain. It will need a separate
  read model with an explicit, recorded rate.
- The `Debug.Assert` in `Money` is a real gap. The basket-level check catches the case
  we know about; a future code path that adds two Money values directly would be silent
  in production.
- Any new monetary concept must decide its currency at creation. For a value derived
  from a basket the answer is the basket's currency. For a value that is not derived
  from a basket — a credit, a voucher, a points balance — there is no obvious answer,
  and that question must be settled before the concept is modelled.
