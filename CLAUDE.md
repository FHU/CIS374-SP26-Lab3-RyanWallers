# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

**Build the main project:**
```
dotnet build Lab3/Lab3.csproj
```

**Run the main program:**
```
dotnet run --project Lab3/Lab3.csproj
```

**Run all tests:**
```
dotnet test UnitTests/UnitTests.csproj
```

**Run a specific test:**
```
dotnet test UnitTests/UnitTests.csproj --filter "TestName"
```

## Project Structure

This is a C# solution (`Lab3.sln`) with two projects:

- **[Lab3/](Lab3/)** — Main library (net10.0) containing data structure implementations in the `Lab3` namespace
- **[UnitTests/](UnitTests/)** — MSTest test project (net10.0) referencing the main library

## Data Structures

**[Lab3/MinHeap.cs](Lab3/MinHeap.cs)** — Generic min-heap backed by a resizable array (initial capacity 8, doubles on full). Fully implemented: `Peek`, `Contains`, `Parent`, `LeftChild`, `RightChild`. Scaffolded but non-functional until trickle methods are completed: `Add`, `ExtractMin`. TODO stubs: `ExtractMax`, `TrickleUp`, `TrickleDown`, `Update`, `Remove`.

**[Lab3/MaxHeap.cs](Lab3/MaxHeap.cs)** — Generic max-heap, currently an empty class stub. Tests expect the same API as MinHeap but inverted: `Add`, `ExtractMax`, `ExtractMin`, `Peek`, `Contains`, `Count`, `IsEmpty`, `Capacity`, `Update`, `Remove`, and a constructor accepting an optional initial array.

**[Lab3/UnionFind.cs](Lab3/UnionFind.cs)** — Generic Union-Find with path compression and union by rank using two `Dictionary<T,T>` maps. Implemented: `Find`, `Union`, `Connected`. TODO stubs: `AreAllConnected` (currently hardcoded `return true`), `Reset` (empty body).

## Heap Array Layout

Both heaps use a flat array where for node at index `i`:
- Parent: `(i - 1) / 2`
- Left child: `2*i + 1`
- Right child: `2*i + 2`

## Test Expectations

- `Remove` and `Update` on a value not in the heap must throw an `Exception` (any exception type satisfies `Assert.ThrowsException<Exception>`).
- `ExtractMin`/`ExtractMax` on an empty heap must throw.
- `MaxHeap` constructor must accept `null` as the initial array (treat as empty).
