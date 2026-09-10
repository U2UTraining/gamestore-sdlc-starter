## Under review

Terms whose meaning is contested, drifting, or used differently in different layers.
Nothing may be implemented against a term in this section until it is resolved and
moved into the catalogue above.

Audited `<scope>` on `<YYYY-MM-DD>`.

### Collisions

The same word, two meanings. Resolve these before building anything that touches them.

| Term | Meaning A | Meaning B | Proposed resolution |
| --- | --- | --- | --- |
| **<Term>** | <what it means here> — `file.cs:12` | <what it means there> — `other.cs:34` | <keep A, rename B to X> |

### Synonyms

Several words, one meaning. Pick one and retire the rest.

| Meaning | Words in use | Keep | Retire |
| --- | --- | --- | --- |
| <the concept> | `Foo` — `a.cs:1`, `Bar` — `b.cs:2` | **Foo** | Bar |

### Undocumented

Business terms in the code that the glossary does not define.

| Term | Where | What it appears to mean | Add to glossary? |
| --- | --- | --- | --- |
| **<Term>** | `file.cs:56` | <inferred meaning> | yes / no, because ... |

### Blocking

Which of the above must be settled before the current piece of work can start, and why.

- **<Term>** — blocks <what>, because <why>
