---
name: Backend
description: Clean Architecture rules that apply across the whole .NET solution.
applyTo: "src/Backend/**/*.cs"
---

# Backend

Binding decisions: [ADR-0001](../../docs/adr/0001-clean-architecture-layering.md).

## The dependency rule

```
Presentation  ->  Application  ->  Domain
Infrastructure ->  Application  ->  Domain
```

Domain references nothing. Never add a package reference to
`GameStore.Domain.csproj` — if a change appears to need one, the design is wrong, and
say so rather than adding it.

Application defines the interfaces it needs from the outside world and implements none
of them. Infrastructure implements them. Presentation wires them together.

## Conventions

- Two-space indentation, matching the existing files.
- Namespaces are file-scoped.
- File-per-type, with the exception of a use case interface and its implementation,
  which share a file (ADR-0005), and endpoint request records, which sit in the file of
  the endpoints that use them (ADR-0006).
- Primary constructors for dependency injection.
- `internal sealed` for implementations; `public` only for the contract a consumer
  actually needs.
- Domain terms come from [the glossary](../../docs/glossary.md). A type or property name
  that uses a word from the "Words we do not use" table is a defect.

## Money and identity

Never a bare `decimal` for an amount — use `Money` (ADR-0002, ADR-0007). Never a bare
`int` for an identity — use the strongly-typed id. A new id type needs a converter
registered in `ApplicationDbContext.ConfigureConventions` or it will fail at runtime.

## Errors

`DomainException` for a broken business rule. `ArgumentException` for malformed input.
The distinction is load-bearing at the endpoint boundary — do not collapse it.

## Tests

There is no test project. Do not add one, or a test framework, as a side effect of
another change. If work needs tests, raise it as its own decision.
