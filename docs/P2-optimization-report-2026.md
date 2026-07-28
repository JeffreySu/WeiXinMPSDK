# P2 测试与质量门禁实施报告（2026）

## 1. 本批范围

- 分支：`Developer`。
- 日期：2026-07-28。
- 本批完成原架构优化清单中的 P2-28（测试分层）和 P2-29（质量与性能门禁）。
- 本批不修改生产源码、公开 API、运行时默认行为、NuGet 包引用、解决方案文件或 SDK 目标框架。

## 2. 向下兼容边界

1. 保留全部 `.net8.csproj`、`.net10.csproj` 和现有 `.sln`；没有合并或删除项目。
2. 仅向现代测试工程补充 `IsTestProject=true`，不会进入 SDK NuGet 包或调用方编译图。
3. `unit`、`integration`、`live`、`stress` runsettings 只有显式传入时生效。
4. 离线测试、压力测试、分析器和性能阈值都是显式命令，不接管现有构建或运行入口。
5. 基准工程只引用本仓库 Core 项目，没有第三方 `PackageReference`。
6. 未新增 `.slnx`、`packages.lock.json`、中央包管理或根级分析器配置。

因此，安装旧版 SDK 的程序升级当前 NuGet 包后，不需要因本批改动修改调用代码或配置；本批没有可进入发布程序集的生产代码差异。

## 3. P2-28：测试分层

- 19/19 个包含 `Microsoft.NET.Test.Sdk` 的现代测试工程都具有明确测试项目元数据。
- `tests/unit.runsettings`：排除 `Integration`、`Live` 和 `Stress`。
- `tests/integration.runsettings`：只运行本地基础设施集成测试。
- `tests/live.runsettings`：只运行需要真实微信/支付凭据的测试。
- `tests/stress.runsettings`：只运行昂贵的并发与性能压力测试。
- `eng/Tests/OfflineTests.proj` 提供跨平台离线入口，并明确排除真实服务和本地中间件依赖。
- 新增 10,000 次并发注册压力测试，只有 `RunStress` 才会执行。

默认离线集合当前覆盖：

| 模块 | 通过数 |
| --- | ---: |
| Core 安全与 JSON | 8 |
| MP 契约 | 18 |
| WxOpen 契约 | 105 |
| Open 契约 | 27 |
| Work 契约 | 338 |
| TenPayV3 契约与通知 | 137 |
| 合计 | 633 |

## 4. P2-29：质量与性能门禁

- `eng/Quality/QualityGate.proj`：显式执行 net10 聚合构建、最小高价值分析器、离线回归和性能门禁。
- 构建门禁禁用“构建时打包”和 SourceLink 仓库查询，并使用单 MSBuild 节点，避免多目标工程图重复打包或争用输出；这只影响门禁命令。
- `ValidateFull` 在上述检查后继续执行 7 个发布 SDK 程序集的 ApiCompat。
- `benchmarks/Senparc.Weixin.Benchmarks`：无第三方依赖，测量日志脱敏分配与并发注册吞吐/分配。
- 阈值用于发现数量级退化、写入不完整或近似死锁，不把工作站微小抖动当作失败。

本机一次验证快照：

| 场景 | 耗时 | 分配 | 结果 |
| --- | ---: | ---: | --- |
| 日志脱敏，50,000 次 | 2,654.7 ns/op | 1,014.5 B/op | 通过 |
| 并发注册，20,000 次 | 436.0 ns/op | 138.6 B/op | 20,000/20,000 完成 |

这些数值是 macOS arm64 当前运行快照，不是 SDK 的公开性能承诺。

## 5. 已取得的验证证据

- XML/MSBuild 文件结构校验：通过。
- `git diff --check`：通过。
- P2 质量门禁：构建 0 错误；633/633 离线测试通过；性能门禁通过。
- 显式 Stress：1/1 通过，10,000 次并发注册完整。
- ApiCompat：Core、TenPay、MP、WxOpen、Open、Work、TenPayV3 共 7 个程序集通过。
- Native AOT：生成 macOS arm64 Mach-O 原生可执行文件并实际输出 `AOT_SMOKE_OK`。

## 6. 使用方式

```bash
# 默认离线回归
dotnet msbuild eng/Tests/OfflineTests.proj -t:Run -v:minimal

# 显式压力测试
dotnet msbuild eng/Tests/OfflineTests.proj -t:RunStress -v:minimal

# net10 构建、离线测试和性能门禁
dotnet msbuild eng/Quality/QualityGate.proj -t:Validate -v:minimal

# 再增加已发布 NuGet 公共 API 对比
dotnet msbuild eng/Quality/QualityGate.proj -t:ValidateFull -v:minimal
```

只有包引用或 SDK 环境变化、或出现资产解析失败时，才传入 `-p:QualityRestore=true`；默认命令均使用 `--no-restore`。

## 7. 尚需外部环境验证的边界

- macOS 无法证明 Windows 上的 .NET Framework/net462 构建和运行结果。
- 没有真实公众号、小程序、企业微信或微信支付凭据，因此未执行线上写接口 E2E。
- 以上边界不影响本批“没有生产程序集变更”的静态兼容结论，但发布前仍应在 Windows CI 执行原有解决方案和 .NET Framework 回归。
