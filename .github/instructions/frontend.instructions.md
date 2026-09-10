---
name: Frontend
description: Rules for the Angular client, including implementing against an accepted mockup.
applyTo: "src/Frontend/**"
---

# Frontend

Angular client for the GameStore API. **Not built yet** — when it is, these rules apply.

## Language

UI copy is bound by [docs/glossary.md](../../docs/glossary.md) exactly as code is. A
button that says "Add to cart" is a defect: the glossary says Shopping Basket. This is
where domain language most often escapes, because copy feels like a writing decision
rather than a modelling one.

Component, service and model names use glossary terms too: `BasketLine`, not
`CartItem`.

## Implementing against a mockup

Accepted mockups live in [docs/design/](../../docs/design/) and are the **reference**,
not a suggestion. When implementing a component that has one:

- Match the layout. Do not improve it in passing — if it is wrong, say so and change the
  mockup first.
- Keep the DaisyUI classes from the mockup.
- Implement **every state the mockup shows**: populated, empty, loading, error, and the
  edges. A mockup shows those states specifically so they do not get skipped.
- If the implementation has to diverge, update the mockup in the same change. Otherwise
  the next person is told to match something that is no longer true.

## API

The backend is at `http://localhost:5038`. **It has no CORS configuration** — that has
to be added to `Program.cs` before the client can call it, and that is a deliberate
backend change.

Responses are anonymous objects assembled by hand in the endpoints, so the wire shape is
whatever those files say. Read the endpoint before writing the client model; do not
assume it mirrors the domain. Ids arrive unwrapped (`id: 3`), and money arrives split
into `price` and `currency`.

There is no authentication. `customerId` is passed in the route.

## Stack

Tailwind and DaisyUI, matching the mockups.

Nothing else is decided yet — state management, HTTP layer, routing, testing. Those are
open decisions. When one has to be made, it is an ADR
(`applies to: frontend`), not a choice made silently inside a component.
