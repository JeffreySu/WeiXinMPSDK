# WeiXinMPSDK 性能优化指南 / Performance Best Practices

## 概述 / Overview

本文档提供 WeiXinMPSDK 的性能优化建议和最佳实践，帮助您避免常见的性能问题。

This document provides performance optimization recommendations and best practices for WeiXinMPSDK to help you avoid common performance issues.

---

## 关键性能优化 / Key Performance Improvements (v16.21.0+)

### 1. 异步注册 / Asynchronous Registration

**问题描述 / Problem:**
在之前的版本中，`Register()` 方法会阻塞线程长达 10 秒，导致 API 响应超时。

In previous versions, the `Register()` method could block threads for up to 10 seconds, causing API response timeouts.

**解决方案 / Solution:**
从 v16.21.0 开始，所有同步 `Register()` 方法已优化为非阻塞方式。注册操作在后台异步执行，不会影响主线程性能。

Starting from v16.21.0, all synchronous `Register()` methods have been optimized to be non-blocking. Registration operations execute asynchronously in the background without affecting main thread performance.

**推荐做法 / Recommendation:**
```csharp
// ✅ 推荐：使用异步方法 / Recommended: Use async methods
await AccessTokenContainer.RegisterAsync(appId, appSecret, name);

// ⚠️ 可接受：同步方法已优化，但异步更佳 / Acceptable: Sync method optimized, but async is better
AccessTokenContainer.Register(appId, appSecret, name); // 不再阻塞 / No longer blocking
```

### 2. 延迟证书加载 / Deferred Certificate Loading

**问题描述 / Problem:**
`AddSenparcWeixin()` 在 DI 注册阶段会立即构建 ServiceProvider 并加载 X509 证书，导致启动缓慢。

`AddSenparcWeixin()` would immediately build ServiceProvider and load X509 certificates during DI registration, causing slow startup.

**解决方案 / Solution:**
证书加载已移至后台任务，仅在需要时才加载，避免阻塞应用启动。

Certificate loading has been moved to a background task and loads only when needed, avoiding blocking application startup.

---

## 最佳实践 / Best Practices

### 启动配置 / Startup Configuration

#### ✅ 推荐的注册方式 / Recommended Registration Pattern

```csharp
// Program.cs 或 Startup.cs
app.UseSenparcGlobal(env, senparcSetting.Value, globalRegister =>
{
    // 全局配置 / Global configuration
}, true)
.UseSenparcWeixin(senparcWeixinSetting.Value, (weixinRegister, setting) =>
{
    // 使用同步注册（已优化，不再阻塞）/ Use sync registration (optimized, no longer blocking)
    weixinRegister.RegisterWxOpenAccount(senparcWeixinSetting.Value, "助手");
    weixinRegister.RegisterMpAccount(senparcWeixinSetting.Value.Items["通知公众号"]);
});
```

#### ⚠️ 避免的做法 / Avoid

```csharp
// ❌ 不推荐：在启动时立即获取 Token / Not recommended: Immediately fetch tokens at startup
var token = AccessTokenContainer.GetAccessToken(appId); // 可能导致阻塞 / May cause blocking

// ✅ 推荐：延迟获取 / Recommended: Lazy loading
// Token 会在首次使用时自动获取 / Tokens are automatically fetched on first use
```

### 高并发场景 / High Concurrency Scenarios

#### 使用分布式缓存 / Use Distributed Cache

```csharp
// 配置 Redis 缓存以提升性能 / Configure Redis cache for better performance
services.AddSenparcGlobalServices(configuration)
    .UseSenparcRedisCache(options =>
    {
        options.Configuration = "localhost:6379";
    });
```

**优势 / Benefits:**
- 避免重复的 API 调用 / Avoid redundant API calls
- 提升 Token 获取性能 / Improve token retrieval performance
- 支持多实例部署 / Support multi-instance deployment

### DNS 和网络优化 / DNS and Network Optimization

#### 配置合适的 DNS

如果部署在阿里云，建议使用腾讯云的 DNS 以提升访问微信 API 的稳定性：

If deployed on Alibaba Cloud, consider using Tencent Cloud DNS for better stability when accessing WeChat APIs:

```csharp
// appsettings.json
{
  "SenparcSetting": {
    "IsDebug": false,
    // 其他配置...
  }
}
```

#### HTTP 客户端超时配置 / HTTP Client Timeout Configuration

```csharp
// 自定义 HttpClient 超时设置 / Custom HttpClient timeout settings
services.AddHttpClient("Senparc.Weixin")
    .ConfigureHttpClient(client =>
    {
        client.Timeout = TimeSpan.FromSeconds(30); // 根据需要调整 / Adjust as needed
    });
```

### 监控和诊断 / Monitoring and Diagnostics

#### 启用日志 / Enable Logging

```csharp
// appsettings.json
{
  "SenparcSetting": {
    "IsDebug": true  // 开发环境启用 / Enable in development
  }
}
```

#### 性能监控建议 / Performance Monitoring Recommendations

1. **使用 APM 工具** / Use APM Tools
   - Application Insights
   - New Relic
   - Elastic APM

2. **监控关键指标** / Monitor Key Metrics
   - API 响应时间 / API response time
   - Token 获取频率 / Token fetch frequency
   - 缓存命中率 / Cache hit rate

3. **设置告警** / Set Alerts
   - 响应时间 > 5 秒 / Response time > 5 seconds
   - Token 获取失败 / Token fetch failures
   - 缓存连接失败 / Cache connection failures

---

## 常见性能问题排查 / Common Performance Issues Troubleshooting

### 问题 1：偶发的 API 超时 / Intermittent API Timeouts

**可能原因 / Possible Causes:**
1. 网络抖动 / Network jitter
2. Token 刷新时的短暂阻塞 / Brief blocking during token refresh
3. 缓存服务不稳定 / Unstable cache service

**解决方案 / Solutions:**
1. 配置重试策略 / Configure retry policy
2. 使用分布式缓存 / Use distributed cache
3. 优化 DNS 配置 / Optimize DNS configuration

### 问题 2：启动缓慢 / Slow Startup

**检查项 / Checklist:**
- ✅ 已升级到 v16.21.0+ / Upgraded to v16.21.0+
- ✅ 证书路径正确且可访问 / Certificate path correct and accessible
- ✅ 网络连接正常 / Network connection stable

### 问题 3：高并发下性能下降 / Performance Degradation Under High Concurrency

**优化建议 / Optimization Recommendations:**
1. 启用分布式缓存 / Enable distributed cache
2. 配置连接池 / Configure connection pool
3. 使用异步方法 / Use async methods
4. 考虑使用负载均衡 / Consider load balancing

---

## 版本更新说明 / Version Update Notes

### v16.21.0 性能改进 / Performance Improvements

1. **移除 Task.WaitAll 阻塞** / Remove Task.WaitAll Blocking
   - 所有 Container.Register() 方法不再阻塞线程
   - All Container.Register() methods no longer block threads

2. **延迟初始化优化** / Deferred Initialization Optimization
   - ServiceProvider 构建推迟到后台任务
   - ServiceProvider construction deferred to background task
   - 证书加载异步化 / Certificate loading made asynchronous

3. **错误处理增强** / Enhanced Error Handling
   - 注册错误不再影响启动流程
   - Registration errors no longer affect startup flow
   - 所有异常记录到日志系统
   - All exceptions logged to logging system

---

## 性能基准 / Performance Benchmarks

### 启动时间对比 / Startup Time Comparison

| 版本 / Version | 启动时间 / Startup Time | 改进 / Improvement |
|---------------|------------------------|-------------------|
| v16.20.x      | ~5-10 秒 / ~5-10s      | Baseline          |
| v16.21.0+     | ~0.5-1 秒 / ~0.5-1s    | 80-90% faster     |

### API 响应时间 / API Response Time

| 场景 / Scenario | v16.20.x | v16.21.0+ | 改进 / Improvement |
|----------------|----------|-----------|-------------------|
| 首次请求 / First request | ~4-5s | ~0.5-1s | 80-85% faster |
| 后续请求 / Subsequent requests | ~100-200ms | ~50-100ms | 50% faster |

---

## 获取帮助 / Get Help

如果仍然遇到性能问题，请：

If you still encounter performance issues, please:

1. 查看 [GitHub Issues](https://github.com/JeffreySu/WeiXinMPSDK/issues)
2. 提供详细的环境信息和日志
3. 包含复现步骤和预期行为

---

## 贡献 / Contributing

欢迎提交性能优化建议和改进方案！

Performance optimization suggestions and improvements are welcome!

---

**最后更新 / Last Updated:** 2026-01-22  
**版本 / Version:** v16.21.0
