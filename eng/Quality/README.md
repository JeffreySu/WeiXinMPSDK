# Incremental quality and performance gates

These gates are additive and opt-in. They do not change the SDK projects, package graph,
public APIs, compiler defaults, or runtime defaults.

```bash
# Build net10 with the opt-in minimum analyzer profile, run offline regressions, and check performance
dotnet msbuild eng/Quality/QualityGate.proj -t:Validate -v:minimal

# Also compare all public SDK assemblies with the previous published NuGet versions
dotnet msbuild eng/Quality/QualityGate.proj -t:ValidateFull -v:minimal

# Refresh assets only after package references or the SDK environment changed
dotnet msbuild eng/Quality/QualityGate.proj -t:Validate -p:QualityRestore=true -v:minimal

# Longer local performance run without enforcing thresholds
dotnet run --project benchmarks/Senparc.Weixin.Benchmarks/Senparc.Weixin.Benchmarks.csproj \
  -c Release --no-restore -p:GeneratePackageOnBuild=false -- --stress
```

The benchmark project has no third-party package references. Its thresholds are deliberately
wide enough to tolerate normal workstation and CI variation while still detecting runaway
allocation, severe serialization/redaction regressions, incomplete concurrent registration,
or deadlock-like throughput collapse. `ValidateFull` may access NuGet to restore published
baseline packages used by ApiCompat.
