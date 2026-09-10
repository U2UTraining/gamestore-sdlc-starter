# ADR-0007: The client is an Angular application with standalone components, Tailwind and DaisyUI, and state in signals

- **Status:** accepted
- **Date:** 2026-09-10
- **Applies to:** frontend

## Context

`src/Frontend/` is empty and four screens have to exist in it: the Game catalogue, a
Game's detail, the Shopping Basket, and Checkout. They talk to the API at
`http://localhost:5038`, which has no authentication, so the client shops as a single
hardcoded Customer.

Three things are already fixed and the client has to live with them:

- The accepted mockups in [docs/design/](../design/) are static HTML using Tailwind and
  DaisyUI from a CDN, and
  [the frontend instructions](../../.github/instructions/frontend.instructions.md) say
  an implementation keeps the DaisyUI classes from the mockup it implements. Whatever we
  choose has to render those classes unchanged.
- Every mockup has to show the populated, empty, loading and error states, and the
  implementation has to have all of them. So the client needs a way of saying "which
  state is this screen in" that does not turn into four unrelated booleans per
  component.
- The API is small and hand-assembled ([ADR-0006](0006-minimal-api-endpoints-grouped-by-feature.md)).
  There is one list endpoint, one detail endpoint, one add-to-basket call and one
  checkout call. The wire shapes do not mirror the domain: ids arrive unwrapped, Money
  arrives as a `price`/`currency` pair, and the Game's name arrives as `title` on two of
  the four responses.

The frontend instructions already name Angular, Tailwind and DaisyUI, and say the
remaining choices — state management, HTTP layer, routing — are open, and that each one
is an ADR rather than a decision made quietly inside a component.

The audience is a training course. What is here is the starting point students are
handed before they do analysis and design exercises on top of it. It has to be readable
after five minutes of looking at it, and it has to be something a room full of .NET
developers can follow.

## Considered options

The framework itself was not genuinely open — the instruction file says Angular and the
repository map says Angular. What is open is *how* the Angular application is put
together, so these options are about that.

### Option A: Standalone components, signals, and HttpClient in services

No `NgModule`. Routes are an array of lazy `loadComponent` entries. Two injectable
services hold what the whole client shares — the Game catalogue and the one Shopping
Basket — as signals, exposed read-only. Screens read those signals directly in their
templates and keep their own local signals for anything that is only theirs, such as
which Add button is currently spinning.

Costs: state lives in a service by convention and nothing enforces it, so an
undisciplined component can start fetching for itself and the client ends up with two
truths about the Shopping Basket. There are no devtools showing a state history. And
because each screen owns its own load state, the loading and error markup is written out
four times instead of shared.

What breaks it: a second Customer, offline support, or optimistic updates that need
rollback. Signals in a service have no answer to any of those.

### Option B: NgRx

A store, actions, reducers, effects and selectors. Time-travel debugging, one place
where state changes are visible, and a pattern that scales well past this app.

Costs: four to six files per feature, for a client with four screens and three write
calls, plus a package and its idioms. On a course whose subject is analysis and design,
the first thing a student would have to learn is NgRx, which is not the subject. It buys
insurance against complexity this client does not have.

What breaks it: nothing technical. It is rejected on proportion, not on capability.

### Option C: RxJS subjects in services and the async pipe

A `BehaviorSubject` per piece of state, `Observable` out, `| async` in the template. The
idiom most Angular code in the wild is written in, and no extra package.

Costs: anything derived needs operators — a Shopping Basket count derived from lines
derived from a request is `combineLatest` and `map` where signals are one `computed`.
Templates fill with `| async` and `*ngIf="x$ | async as x"`. For this size of
application it is the harder version of Option A, with the same guarantees.

### Option D: Angular Material instead of Tailwind and DaisyUI

A component library with accessibility built in, which is a real advantage over
hand-assembled DaisyUI markup.

Rejected outright: the accepted mockups in `docs/design/` are DaisyUI, and the frontend
instructions say to keep the mockup's DaisyUI classes. Choosing Material would mean
every mockup in this repository stops being implementable as written, and the mockups
are documentation we intend to keep.

## Decision

Option A, with Tailwind 4 and DaisyUI 5 compiled through PostCSS rather than pulled from
a CDN.

- **Standalone components**, no `NgModule`. Each of the four screens is one component
  with a separate `.html` template, lazily loaded by the router.
- **State in signals, in two services.** `GameService` holds the Game catalogue.
  `ShoppingBasketService` holds the one Shopping Basket and is the only thing that calls
  the basket endpoints. Both expose `asReadonly()` signals plus `computed` derivations,
  so a component can read state but can only change it by calling a method.
- **One explicit `LoadState`** — `idle | loading | loaded | error` — per screen, instead
  of separate loading and error booleans, because the states the mockups demand are the
  values of that type. Empty is deliberately not one of them: it is `loaded` with
  nothing in it, and each screen asks its own data whether it is empty.
- **`HttpClient` with `withFetch()`**, called from the services only. No component
  injects it.
- **The wire shapes stay in the service that calls the endpoint**, as private
  `...Response` interfaces, mapped onto glossary-named models on the way in. The `title`
  field the API uses for a Game's name is renamed to `name` at that boundary, so a word
  the glossary rejects never reaches a component or a template.
- **Tailwind and DaisyUI at build time.** `src/styles.css` is an `@import "tailwindcss"`
  and a `@plugin "daisyui"`. Class names in a component template are then the same class
  names as in a mockup, which is what makes "match the mockup" a checkable instruction.
- **`customerId` is a constant** in `core/api.config.ts`, next to the API base URL, so
  that when authentication arrives there is exactly one place that has to change.

What separated Option A from Option B was proportion. This client has three write
operations and one shared aggregate. NgRx is the right answer to a state problem, and
this client does not have one yet.

## Consequences

- A screen is one `.ts` file and one `.html` file. Reading the whole client is an
  afternoon at most, which is what a starting point for an exercise has to be.
- Derived values — the Shopping Basket count in the header, whether the Basket is empty
  — are one `computed` each and update everywhere at once, with no subscription
  management and no `ngOnDestroy`.
- The loading and error markup is copied into all four templates. That is real
  duplication and it gets worse with the fifth screen. Lifting it into shared components
  is the obvious next move and is deliberately not done yet.
- Nothing prevents a future component from injecting `HttpClient` directly and keeping
  its own copy of the Shopping Basket. The rule that the service owns shared state is a
  convention held up by review, not by the compiler.
- There is no test setup. `ng new` left Vitest in `package.json` and there are no specs.
  How the frontend is tested is an open decision and needs its own ADR before anyone
  writes the first one.
- Tailwind at build time means the client no longer opens in a browser without a build,
  the way the mockups do. Mockups and implementation now have two different ways of
  loading the same CSS framework, and their versions can drift apart without anything
  noticing.
- **What we would have to undo if this turns out to be wrong:** the two services. Every
  component reads them through signals, so moving to a store means rewriting both
  services and every template binding that reads them. That is a day at four screens and
  considerably more later.
- **What would tell us it has turned out to be wrong:** two components disagreeing about
  the Shopping Basket; a `computed` chain nobody can follow; needing to undo an
  optimistic update; or more than one Customer being signed in at a time. Any of those
  means the state problem has arrived and Option B was the answer to it.

## Revisit when

Authentication lands and `CUSTOMER_ID` stops being a constant, or when the client grows
past roughly eight screens — whichever comes first.
