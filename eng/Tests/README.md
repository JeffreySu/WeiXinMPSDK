# Test execution layers

The existing `.net8.csproj` and `.net10.csproj` project structure is intentionally preserved.
`OfflineTests.proj` provides an additive, cross-platform entry point for the modern .NET tests
that are known to be deterministic and do not require external credentials or services.

## Categories

- No category: local unit or contract test.
- `Integration`: requires a local cache, database, middleware, or multi-process environment.
- `Live`: calls a real Weixin or payment endpoint and requires explicit credentials.
- `Stress`: intentionally expensive concurrency, allocation, or throughput coverage.

Tests that make network requests must use `Live`. Tests that require Redis, CsRedis, Dapr,
Memcached, or another locally provisioned service must use `Integration`.

## Commands

```bash
# Stable modern .NET offline regression set
dotnet msbuild eng/Tests/OfflineTests.proj -t:Run -v:minimal

# Restore test graphs when package references or the SDK environment changed
dotnet msbuild eng/Tests/OfflineTests.proj -t:Run -p:OfflineRestore=true -v:minimal

# Explicit stress tests
dotnet msbuild eng/Tests/OfflineTests.proj -t:RunStress -v:minimal
```

The runsettings files under `tests/` can also be supplied to an individual `dotnet test`
command. Live and integration tests are never part of the default offline entry point.
