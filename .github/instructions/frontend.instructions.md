---
name: Frontend
description: Rules for the Angular client, including implementing against an accepted mockup.
applyTo: "src/Frontend/**"
---

# Frontend

Angular client for the GameStore API. It lives in `src/Frontend/` and has four screens:
the Game catalogue, a Game's detail, the Shopping Basket and Checkout.

Binding decision: [ADR-0009](../../docs/adr/0009-frontend-stack.md).

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

The backend is at `http://localhost:5038`. It allows any origin in Development
(`Program.cs`), so there is no proxy here. Widening that policy, or making it apply
outside Development, is a deliberate backend change.

Responses are anonymous objects assembled by hand in the endpoints, so the wire shape is
whatever those files say. Read the endpoint before writing the client model; do not
assume it mirrors the domain. Ids arrive unwrapped (`id: 3`), and money arrives split
into `price` and `currency`.

There is no authentication. `customerId` is passed in the route.

## Stack

Angular with standalone components, Tailwind and DaisyUI matching the mockups, and state
held in signals inside services. [ADR-0009](../../docs/adr/0009-frontend-stack.md) has
the rules and the reasoning:

- Components inject a service, never `HttpClient`.
- Shared state lives in `core/*.service.ts` as read-only signals with `computed`
  derivations. A component keeps only state that is its own.
- Each screen carries one `LoadState` (`idle | loading | loaded | error`). Empty is not
  one of them: a screen is `loaded` and asks its data whether it is empty.
- Wire shapes stay private to the service that calls the endpoint and are mapped onto
  glossary-named models on the way in.

**Testing is still an open decision.** There is no spec in the project and no ADR for
how to write one. When that has to be settled it is an ADR (`applies to: frontend`), not
a choice made silently inside a component.
