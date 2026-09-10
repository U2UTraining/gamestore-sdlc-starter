---
name: mockups
description: Generate three genuinely different HTML mockups for a UI decision so they can be compared side by side, then keep the winner as documentation. Use when there is a visual or interaction choice to make, or when asked for mockups, wireframes, UI options, or to compare layouts.
---

# Mockups

Three variations on one layout is not a comparison. Each option should embody a
different answer to the underlying question — how much control the Customer gets, how
much the system decides for them, how prominent the feature is on the screen.

**State that underlying question at the top of your reply, before you build anything.**
If you cannot articulate what the three options disagree about, there is nothing to
compare.

## Build them

Write to `docs/specs/NNN-slug/mockups/option-a.html`, `option-b.html`, `option-c.html`.

Start each from [assets/mockup-shell.html](assets/mockup-shell.html), which carries the
Tailwind and DaisyUI CDN links, the state-section scaffolding and the header comment
block.

At least one option must be the restrained one: the version that adds the least to the
screen.

Each file:

- **Self-contained.** Opens in a browser with no build step. No framework, no data
  fetching, no build tooling. Static HTML.
- **Every state**, laid out down the page with a heading above each: populated, empty,
  loading, error, and the interesting edges — a very long game name, a large number, a
  zero balance, a single item. A mockup that shows only the happy path produces an
  implementation that only handles the happy path.
- **Realistic content.** Real board game names, prices formatted for the currency
  (EUR as `49,99 €`), plausible quantities, names that are actually too long.
  Placeholder text hides layout problems.
- **Glossary copy.** UI text is bound by [docs/glossary.md](../../../docs/glossary.md)
  exactly as code is. This is where "Cart" creeps back in.
- **Comments where behaviour is not visible** from the markup: what a control does, what
  changes on interaction, what the loading state is waiting for.

## Compare them

In your reply — not in the files:

| | Option A | Option B | Option C |
| --- | --- | --- | --- |
| Idea in one line | | | |
| Best when | | | |
| Fails when | | | |
| Cost to build | | | |

Recommend one, and say what would change your mind.

## Keep the winner

Once the user picks, copy that file to `docs/design/<component>.html`. It stops being a
sketch at that point and becomes the reference the implementation is checked against:

```
Implement the product listing to match docs/design/listing-cards.html.
Keep the DaisyUI classes. Include the empty and loading states shown
in the mockup. Do not change the layout.
```

That only works if the file is still there and still accurate. Delete the losing
options, or leave them under the spec — never in `docs/design/`.

## Do not

Write Angular components. Do not touch `src/`. This skill decides what the thing should
look like; building it is a separate issue against a spec.
