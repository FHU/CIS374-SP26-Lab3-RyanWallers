# Lab 3 — Heaps (and Union-Find)

Implementation of three data structures in C#: MinHeap, MaxHeap, and UnionFind.

## Project Structure

- `Lab3/` — Data structure implementations (`Lab3` namespace, net10.0)
- `UnitTests/` — MSTest unit tests (net10.0)

## Data Structures

### MinHeap\<T\>
A generic min-heap backed by a resizable array. The minimum element is always at the root.

### MaxHeap\<T\>
A generic max-heap with the same API as MinHeap but inverted — the maximum element is at the root.

Both heaps support: `Add`, `ExtractMin`, `ExtractMax`, `Peek`, `Contains`, `Update`, `Remove`, `Count`, `IsEmpty`, `Capacity`.

### UnionFind\<T\>
A generic disjoint-set (Union-Find) structure using path compression and union by rank. Supports: `Find`, `Union`, `Connected`, `AreAllConnected`, `Reset`.

## Commands

```bash
# Build
dotnet build Lab3/Lab3.csproj

# Run
dotnet run --project Lab3/Lab3.csproj

# Test
dotnet test UnitTests/UnitTests.csproj

# Run a specific test
dotnet test UnitTests/UnitTests.csproj --filter "TestName"
```
