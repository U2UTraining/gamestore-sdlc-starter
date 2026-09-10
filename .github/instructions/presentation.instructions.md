---
name: Presentation layer
description: Rules for minimal API endpoints and the HTTP contract.
applyTo: "src/Backend/src/GameStore.Presentation/**/*.cs"
---

# Presentation layer

Translate HTTP into a use case call and the result back into JSON. Nothing else.

Binding decision:
[ADR-0006](../../docs/adr/0006-minimal-api-endpoints-grouped-by-feature.md).

## Endpoints

One `internal static class XxxEndpoints` per resource in `Endpoints/`, exposing
`MapXxxEndpoints(this IEndpointRouteBuilder)`. Create a route group with the base path
and tag:

```csharp
var group = app.MapGroup("/api/games").WithTags("Games");
```

Register it in `EndpointRegistration.MapApiEndpoints`. No controllers.

## Contracts

Request bodies are `internal sealed record` types at the bottom of the same file. They
are wire contracts, not shared with any other layer.

Responses are anonymous objects, mapping domain objects to primitives by hand:

```csharp
return Results.Ok(new
{
  id = game.Id.Value,
  price = game.Price.Amount,
  currency = game.Price.Currency.ToString(),
});
```

**The JSON field names are bound by [the glossary](../../docs/glossary.md).** This
hand-mapping is exactly where the API vocabulary drifts away from the domain, because
nothing checks it. A field name that uses a word from the "Words we do not use" table
is a defect, not a preference — and changing one is a breaking change, so raise it
rather than fixing it silently in passing.

## No logic

Parse, wrap primitives into value objects, call one use case, map the result. An
endpoint that makes a business decision is in the wrong layer.

## Errors

```csharp
catch (DomainException ex)   => Results.BadRequest(new { error = ex.Message });
catch (ArgumentException ex) => Results.BadRequest(new { error = ex.Message });
```

A missing entity is `Results.NotFound()`. Keep the two exception types distinct even
though both currently map to 400 — the distinction is deliberate
([ADR-0003](../../docs/adr/0003-aggregates-own-their-invariants.md)).

The repetition is known and accepted. Do not introduce exception middleware as a side
effect of another change; that is its own decision.

## CORS

There is none. The Angular client will need it. Adding it is a deliberate change to
`Program.cs`, not something to slip into an unrelated commit.
