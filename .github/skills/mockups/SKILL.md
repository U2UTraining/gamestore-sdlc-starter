---
name: mockups
description: "Use when a UI or interaction decision needs comparing — mockups, wireframes, layout options, or 'what should this look like'. Builds three HTML options, opens them in the browser, and lets the human pick the winner."
---

1. **Name the axis the three options disagree on** before building. Three variations on one layout is not a comparison.

2. **Build three** from [assets/mockup-shell.html](assets/mockup-shell.html) into `docs/specs/NNN-slug/mockups/option-a.html`, `-b`, `-c`. One must be the restrained option.

3. **Show every state** in each file: populated, empty, loading, error, and the awkward edges — a very long Game name, a large number, a zero balance, a single item.

4. **Realistic content, glossary copy.** Real board game names, prices as `49,99 €`. UI text is bound by [docs/glossary.md](../../../docs/glossary.md). Comment what the markup cannot show.

5. **Open all three in the browser.** Reading the HTML is not looking at it.

   ```powershell
   Get-ChildItem docs/specs/NNN-slug/mockups/*.html | ForEach-Object { Start-Process $_.FullName }
   ```

6. **Stop and wait.** In chat: a comparison table — idea, best when, fails when, cost to build — and a recommendation with what would change your mind. **The human picks.** Do not decide for them, and do not start implementing.

7. **Once picked**, copy the winner to `docs/design/<component>.html`; it is the reference implementations are checked against. Losers never go there.

8. **No Angular. Do not touch `src/`.**
