# 0014. Rename EmberLlm to LocalSharp

- Status: Accepted (Michael, 2026-10-20)
- Date: 2026-10-20

## Context

The project shipped v0.1.0 as `EmberLlm`. Kindroid released "Ember", its flagship LLM, in March 2026, and searches for "Ember LLM" return Kindroid's model almost exclusively. For a library in the same space, the name hurt discoverability and invited confusion with a model of the same name.

At the time of the decision the package had no known users beyond test installs, so renaming was as cheap as it would ever be.

## Decision

Rename the project, package and public API to **LocalSharp**:

- **Package and namespace:** `LocalSharp`.
- **Public types:** the `EmberLlm` prefix becomes `LocalSharp`: `LocalSharpClient`, `LocalSharpOptions`, `LocalSharpDiscovery`, `LocalSharpServiceCollectionExtensions.AddLocalSharp`, `LocalSharpException` and its subclasses.
- **Unchanged:** every type without the prefix (`ModelInfo`, `DiscoveredServer`, `StructuredOutputMode` and the others), and all behavior.

The name follows .NET convention for client libraries (LlamaSharp, OllamaSharp) and says what the package is for: local models from C#. It was checked against NuGet, GitHub and the web for collisions.

Accepted decision notes keep the names they were written with; this note maps them.

## Alternatives considered

- **Keep EmberLlm.** No migration work, but every promotion step would have deepened the clash.
- **Other names:**
  - **Fire-themed names** (Homefire, Pilotlight, Bothy and others) were free of collisions but read as unrelated to what the library does.
  - **Descriptive names** were mostly taken: `LocalLlm` by a Google Cloud project, `DotLlm` by a .NET inference engine on NuGet, and `LoopbackLlm` clashes with an existing LLM gateway.

## Consequences

- Existing code changes its `using` and type names; nothing else.
- `EmberLlm` is deprecated on NuGet and points to `LocalSharp`. NuGet package IDs can't be renamed, so the history of `EmberLlm` stays where it is.
- The GitHub repository is renamed; GitHub redirects the old URL.
- Earlier decision notes, the spike write-up and changelog entries before this one use the old names.
