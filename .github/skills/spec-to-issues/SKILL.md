---
name: spec-to-issues
description: Split a spec into independently workable GitHub issues and create them with the gh CLI. Use when asked to break down a spec, create issues or tickets, plan implementation work, or turn requirements into tasks.
---

# Spec to issues

**The issue is the prompt, so write it like one.** Assume the person picking it up — or
the agent picking it up — has read nothing else. Everything needed to start work is
either in the issue or one click away.

Read the spec, [docs/glossary.md](../../../docs/glossary.md) and the ADRs the spec
links, before you split anything.

## Every issue contains

Use [assets/issue-template.md](assets/issue-template.md).

- **A goal in one sentence**, plus links to the spec and to the ADRs that constrain the
  work.
- **Acceptance criteria copied from the spec, not paraphrased.** Verbatim, keeping the
  requirement numbers. If you want to reword a criterion, the spec is wrong — fix the
  spec first, then copy it.
- **The files or folders the change is expected to touch.** Real paths, checked against
  the repository. Not a guess.
- **An explicit out of scope line.** What a reasonable person might otherwise pull into
  this issue, and which issue it belongs to instead.
- **How to verify it.** The command to run, the request to send, the state to inspect.
  `dotnet build src/Backend/GameStore.slnx` is a start, not an answer — say what the
  person should observe.

## How to split

- **Along layer boundaries** where the layers are genuinely separable — see
  [ADR-0001](../../../docs/adr/0001-clean-architecture-layering.md). A single issue
  saying "implement the feature" is not a split.
- **One issue is one sitting.** If you cannot say what "done" looks like in a sentence,
  it is too big.
- **Say what blocks what.** Name dependencies explicitly in the body. Where one issue
  changes a shared type the others build on, it goes first, and every dependent issue
  says so.
- **No issue may depend on an unanswered open question.** If one would, do not write it.
  List it at the end as blocked, naming the open question and its owner.

Riskiest issue first, not easiest. The change that could invalidate the others should be
the one that gets done while there is still time to react.

## Creating them

Show the full list as a plan first: title, one-line goal, dependencies, in order.
**Wait for approval before creating anything.**

Once approved:

```bash
gh label list
gh issue create --title "..." --body-file <path> --label "..."
```

Write each body to a temporary file rather than passing it inline — issue bodies contain
Markdown, backticks and newlines that do not survive shell quoting.

Use labels that already exist. Do not invent new ones without asking.

If `gh` is not authenticated or there is no remote, stop and say so — do not write the
issues to a Markdown file as a silent substitute unless the user asks for that.

## Then

Print the issue numbers and URLs, say which to start with and why, and list anything
left blocked.

Do not write application code.
