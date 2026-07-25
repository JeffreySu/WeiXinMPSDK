# 当前工作区提交拆分清单（2026-07-25）

## 目的与执行边界

当前索引同时包含已暂存、未暂存和未跟踪文件，且多个项目文件为 `MM`/`AM` 状态。本清单只定义后续提交边界，不执行暂存、取消暂存或提交，也不改变用户现有索引。

执行拆分时必须遵守：

1. 不使用 `git add -A`、`git add .` 或按整个 `src` 暂存。
2. 对 `MM`/`AM` 文件逐段检查；同一项目文件内的安全依赖、目标框架配置和官方 API 测试引用不能依靠整文件路径自动归类。
3. 每个候选提交先检查 `git diff --cached --stat`、`git diff --cached --check` 和完整暂存差异，再创建提交。
4. 未出现在本清单边界中的文件默认不进入该提交。
5. 真实账号、商户资金和写入型 E2E 不作为离线提交的伪造证据，需在具备凭据的环境单独记录。

## 最新索引覆盖审计

2026-07-25 按 `git diff --name-only --cached`、`git diff --name-only` 和 `git ls-files --others --exclude-standard` 重新取并集；在 Work 的会议固定协议、ID 转换、客户身份迁移、上下游企业、上下游通讯录、审批模板复制、汇报导出和对外收款批次完成后再次复核，共得到 518 个实际变更文件。全部能够归入下列 9 个候选提交，未发现未归类路径。

| 候选提交 | 文件数 |
| --- | ---: |
| 1：CI ApiCompat | 3 |
| 2：MSTest 发现清理 | 7 |
| 3：安全与 Native AOT | 16 |
| 4：MP | 34 |
| 5：WxOpen | 107 |
| 6：Open | 20 |
| 7：Work | 233 |
| 8：TenPay | 96 |
| 9：工程清单与说明 | 2 |

其中 44 个文件同时存在暂存和未暂存差异，不能按整文件直接暂存。高风险混合文件包括多个生产/测试项目文件、MP `UrlApi.cs`、Open `ManagedOfficialAccountApi.cs`、Work 消息上下文与处理器、TenPayV3 请求/通知基础设施，以及若干已暂存后继续修正的产品 API 文件。执行提交时必须重新从空的隔离索引按候选边界构建，或逐段审阅这些文件；不能把当前共享索引直接视为任一候选提交。

上述数字按文件唯一归属统计。旧版 TenPay 的 `Senparc.Weixin.TenPay.net10.csproj` 在实际提交链中会先提交语言/AOT 配置，再在 TenPay 业务提交中补入企业支付发布说明，因此候选提交 8 的真实触达文件数为 97，但工作区并集仍只计算该文件一次。

被 `.gitignore` 明确排除的 `docs/optimization-delivery-status-2026-07-24.md` 和 `docs/official-api-gap-audit-2026-07-16.md` 不在上述 518 文件中。它们是本地实时证据，不会被普通 `git add` 纳入候选提交 9。

## 候选提交 1：CI 公共 API 兼容闸门

建议主题：`ci: gate NuGet publishing on changed SDK assemblies`

精确文件：

- `azure-pipelines.yml`
- `eng/ApiCompat/ApiCompat.proj`
- `eng/ApiCompat/README.md`

验证：

```powershell
dotnet msbuild eng/ApiCompat/ApiCompat.proj -restore -t:ValidateApiCompatibility -v:minimal -clp:ErrorsOnly
```

这里的 `-restore` 是有意的：闸门自身使用 `PackageDownload` 获取已发布基线。当前检查 Core `6.24.0`、旧版 TenPay `1.18.3`、MP `16.25.0`、WxOpen `3.28.0`、Open `4.24.3`、Work `3.32.0` 和 TenPayV3 `2.5.0`；发布流水线仅在七个程序集全部兼容后推送 NuGet。开发过程中可附加 `-p:ApiCompatAssembly=<精确包 ID>` 只验证一个模块。

## 候选提交 2：MSTest 发现阶段清理

建议主题：`test: remove known MSTest discovery warnings`

精确文件：

- `src/Senparc.Weixin.MP/Senparc.Weixin.MP.Test/AdvancedAPIs/GroupMessage/GroupMessageTest.cs`
- `src/Senparc.Weixin.MP/Senparc.Weixin.MP.Test/AdvancedAPIs/OAuth/OAuthTest.cs`
- `src/Senparc.Weixin.MP/Senparc.Weixin.MP.Test/TenPayV3/RedPackApiTest.cs`
- `src/Senparc.Weixin.WxOpen/src/Senparc.Weixin.WxOpen.Tests/WxOpenBaseTest.cs`
- `src/Senparc.Weixin.Work/Senparc.Weixin.Work.Test/AdvancedAPIs/DataIntelligence/DataIntelligenceTest.cs`
- `src/Senparc.Weixin.Work/Senparc.Weixin.Work.Test/AdvancedAPIs/Media/MediaTest.cs`
- `src/Senparc.Weixin.TenPay/Senparc.Weixin.TenPayV3.Test/Apis/VehicleParking/VehicleParkingApisTest.cs`

验证方式：分别编译对应 `net10.0` 测试工程，然后执行 `dotnet test <project> --no-build -f net10.0 --list-tests`，输出中不得再出现 `UTA[0-9]+` 或 `MSTestAdapter failed`。完整构建仍有既有编译警告，本提交不把“发现警告已清理”扩大为“全仓库 0 警告”。

## 候选提交 3：NuGet 安全与 Native AOT 收口

建议主题：`fix: harden modern dependencies and Native AOT paths`

精确路径边界：

- `src/Senparc.Weixin/Senparc.Weixin/CommonAPIs/CommonJsonSend.cs`
- `src/Senparc.Weixin/Senparc.Weixin/Helpers/Serializers/WeixinJsonSerializer.cs`
- `src/Senparc.Weixin/Senparc.Weixin/Utilities/HttpUtility/Post.cs`
- `src/Senparc.Weixin/Senparc.Weixin/Senparc.Weixin.net10.csproj`
- `src/Senparc.Weixin/Senparc.WeixinTests/Senparc.WeixinTests.net10.csproj`
- `src/Senparc.WebSocket/src/Senparc.WebSocket/Senparc.WebSocket/Senparc.WebSocket.net8.csproj`
- `src/Senparc.WebSocket/src/Senparc.WebSocket/Senparc.WebSocket/Senparc.WebSocket.net10.csproj`
- `src/Senparc.Weixin.AspNet/Senparc.Weixin.AspNet.net8.csproj`
- `src/Senparc.Weixin.AspNet/Senparc.Weixin.AspNet.net10.csproj`
- `src/Senparc.Weixin.Cache/Senparc.Weixin.Cache.Memcached/Senparc.Weixin.Cache.Memcached.net8.csproj`
- `src/Senparc.Weixin.Cache/Senparc.Weixin.Cache.Memcached/Senparc.Weixin.Cache.Memcached.net10.csproj`
- `src/Senparc.Weixin.All/Senparc.Weixin.All.net10.csproj`
- `src/Senparc.Weixin.TenPay/Senparc.Weixin.TenPay/Senparc.Weixin.TenPay.net10.csproj` 中仅属于 C# 12/安全/AOT 的段落
- `tests/NativeAotSmoke/NativeAotSmoke.csproj`
- `tests/NativeAotSmoke/Program.cs`
- `tests/NativeAotSmoke/README.md`

其中 WebSocket、AspNet、Memcached、All、旧版 TenPay 项目文件当前存在混合索引状态，必须逐段选择，不能直接整文件覆盖当前索引。

验证：现代聚合构建、Core/TenPay 定向测试、0 已知 NuGet 漏洞复核，以及：

```powershell
dotnet publish tests/NativeAotSmoke/NativeAotSmoke.csproj --no-restore -c Release -r osx-arm64 --self-contained -clp:ErrorsOnly
```

原生产物必须是 Mach-O arm64，运行输出必须包含 `AOT_SMOKE_OK`。Windows `win-x64` 是方便时补跑项，不阻塞当前 macOS 收口。

## 候选提交 4～8：官方 API 模块

按模块独立提交，禁止横跨五个模块打包成一个提交：

| 顺序 | 模块边界 | 离线验收基线 | 仍需外部验证 |
| --- | --- | --- | --- |
| 4 | `src/Senparc.Weixin.MP/`，排除候选提交 2 的三个发现警告文件 | 契约 18/18；现代目标编译及模块 ApiCompat | 真实认证服务号/行业账号 E2E |
| 5 | `src/Senparc.Weixin.WxOpen/`，排除候选提交 2 的 `WxOpenBaseTest.cs` | 契约 105/105；现代目标编译及模块 ApiCompat | 小程序、硬件、云开发、资金类 E2E |
| 6 | `src/Senparc.Weixin.Open/` | 契约 27/27；现代目标编译及模块 ApiCompat | 第三方平台授权及资源写入 E2E |
| 7 | `src/Senparc.Weixin.Work/`，排除候选提交 2 的两个发现警告文件 | 契约 273/273；现代目标编译及模块 ApiCompat；WeDoc 完整覆盖 48/48 项，家校沟通四批 40 项 HTTP API，会议固定协议覆盖 100/100 项，账号及群聊 ID 转换覆盖 7 项，客户身份转换与迁移覆盖 6 项，并覆盖上下游企业、上下游通讯录、审批模板复制、汇报导出和对外收款增量 | 企业权限、回调、图片上传、文档/智能表格、学校通知和写入型 E2E；家校回调和说明页待分类 |
| 8 | `src/Senparc.Weixin.TenPay/`，排除候选提交 2 的停车测试，并从旧版 TenPay 项目文件排除候选提交 3 的配置段落 | TenPayV3 契约 131/131；现代目标编译及模块 ApiCompat | 真实商户资金、通知和账单 E2E |

TenPay 候选提交 8 应包含 `Apis/Ecommerce/EcommerceMerchantCancellationContractTests.cs`，其反射测试已适配强类型 `MultipartMetaFieldStyle` 枚举；该文件属于业务契约批次，不属于 MSTest 发现阶段清理。

每个模块在提交前还需检查相应版本号和发布说明是否覆盖本模块所有生产 `.cs` 修改。项目文件若含多个批次的变更，应使用逐段暂存或在隔离索引中重建，不得用整文件暂存覆盖已审阅边界。

## 候选提交 9：工程清单与交付说明

建议主题：`docs: record project structure and delivery boundaries`

候选内容：

- `.gitignore` 中已经审阅的工程/本地文件规则。
- `docs/commit-split-manifest-2026-07-25.md`。
- 其他明确决定纳入版本控制的计划或报告。

`docs/optimization-delivery-status-2026-07-24.md` 当前被 `.gitignore` 明确忽略，因此它是实时本地交付状态，不会随普通暂存进入 Git。若后续决定将其纳入仓库，应先单独确认并调整忽略策略，不能用强制暂存绕过现有规则。

## 当前完成与未完成

已完成：七程序集 CI ApiCompat 闸门实现并使用 `/tmp` 隔离 NuGet 缓存完整实跑；已知 MSTest 发现警告清理；现代目标定向构建；MP/WxOpen/Open/Work/TenPayV3 契约回归；macOS arm64 Native AOT 发布及原生运行；WeDoc 当前可交叉确认的 48/48 项、应用管理增量 4 项、家校沟通四批 40 项、成员授权/二次验证 4 项、会议固定协议 100/100 项、账号/群聊 ID 转换 7 项、客户身份转换与迁移 6 项，以及上下游企业、上下游通讯录、审批模板复制、汇报导出和对外收款增量，Work 契约回归 273/273。

未完成：上述候选提交向真实索引和当前分支的导入、需要真实凭据的线上 E2E、可选的 Windows `win-x64` 与完整解决方案复核。最新 NuGet 漏洞源查询已使用 `/tmp` 隔离缓存完成，未发现已知漏洞；隔离索引的 9 提交链已经重建并通过逐提交检查。旧 .NET Framework 升级不在当前范围内。
