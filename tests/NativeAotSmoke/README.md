# Native AOT smoke test

This executable verifies the source-generated JSON paths used by the Weixin core and legacy TenPay package under a real Native AOT publish.

macOS arm64:

```bash
dotnet publish tests/NativeAotSmoke/NativeAotSmoke.csproj -c Release -r osx-arm64 --self-contained
```

Windows x64:

```powershell
dotnet publish tests/NativeAotSmoke/NativeAotSmoke.csproj -c Release -r win-x64 --self-contained
```

The published executable must print `AOT_SMOKE_OK`. The project restricts its referenced multi-target projects to `net10.0`; it does not require building or upgrading .NET Framework targets.
