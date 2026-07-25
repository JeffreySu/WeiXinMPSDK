# Public API compatibility gate

This gate compares the current `netstandard2.1` assemblies with the previous
published NuGet versions before packages can be pushed.

Run from the repository root:

```powershell
dotnet msbuild eng/ApiCompat/ApiCompat.proj -restore -t:ValidateApiCompatibility -v:minimal
```

To validate one package while developing a module, pass its exact package ID:

```powershell
dotnet msbuild eng/ApiCompat/ApiCompat.proj -restore -t:ValidateApiCompatibility -p:ApiCompatAssembly=Senparc.Weixin.Work -v:minimal
```

The release pipeline does not pass this property and therefore always checks
the complete list below.

The project currently checks every SDK assembly changed by the current release:

- `Senparc.Weixin` 6.24.0 against the current Core assembly.
- `Senparc.Weixin.TenPay` 1.18.3 against the current TenPay assembly.
- `Senparc.Weixin.MP` 16.25.0 against the current MP assembly.
- `Senparc.Weixin.WxOpen` 3.28.0 against the current WxOpen assembly.
- `Senparc.Weixin.Open` 4.24.3 against the current Open assembly.
- `Senparc.Weixin.Work` 3.32.0 against the current Work assembly.
- `Senparc.Weixin.TenPayV3` 2.5.0 against the current TenPayV3 assembly.

When a new version is published, update each baseline version to that published
version in the next development cycle. Do not point the baseline at an
unpublished local package.

The gate uses the ApiCompat task bundled with the .NET 10 SDK and does not add a
runtime dependency to any SDK package.
