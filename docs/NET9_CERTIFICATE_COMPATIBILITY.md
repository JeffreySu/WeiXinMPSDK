# .NET 9.0 证书兼容性说明 / .NET 9.0 Certificate Compatibility Guide

## 中文版本

### 问题描述

在升级到 .NET 9.0 后，TenPayV3 退款操作可能会遇到 SSL 证书错误：

```
Senparc.Weixin.Exceptions.WeixinException: The SSL connection could not be established, see inner exception
```

### 根本原因

.NET 9.0 对 X509Certificate2 的加载和处理进行了更严格的验证，特别是：

1. **X509KeyStorageFlags.MachineKeySet** 标志在非 Windows 平台上可能失败
2. 证书私钥权限要求更加严格
3. TLS 1.3 成为默认协议，某些情况下需要显式配置

### 解决方案

本项目已经实现了以下兼容性修复：

#### 1. 平台自适应的证书加载标志

```csharp
X509KeyStorageFlags storageFlags;

#if NET9_0_OR_GREATER
// .NET 9.0+: 使用更兼容的标志组合
storageFlags = X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet;
if (System.OperatingSystem.IsWindows())
{
    // 仅在 Windows 上使用 MachineKeySet
    storageFlags |= X509KeyStorageFlags.MachineKeySet;
}
#else
// 旧版本 .NET: 保持原有行为
storageFlags = X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet;
#endif

var cert = new X509Certificate2(certPath, certPassword, storageFlags);
```

#### 2. 显式的 SSL/TLS 协议配置

```csharp
#if NET9_0_OR_GREATER
// 显式支持 TLS 1.2 和 TLS 1.3
httpClientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 
                                | System.Security.Authentication.SslProtocols.Tls13;

// 确保证书选择回调正确处理客户端证书
httpClientHandler.ClientCertificateOptions = System.Net.Http.ClientCertificateOption.Manual;
#endif
```

### 使用建议

#### 推荐配置（.NET 8.0 LTS）

如问题中 [@JeffreySu](https://github.com/JeffreySu) 所建议，.NET 8.0 是长期支持（LTS）版本，推荐用于生产环境：

- ✅ .NET 8.0 - 推荐使用（LTS，支持到 2026 年 11 月）
- ⚠️ .NET 9.0 - 短期支持版本（支持到 2025 年 5 月）
- 🔮 .NET 10.0 - 将在 2025 年 11 月发布（下一个 LTS 版本）

#### 如果必须使用 .NET 9.0

确保：

1. **证书文件格式正确**：使用 .p12 或 .pfx 格式
2. **证书密码正确**：验证证书密码是否正确
3. **证书包含私钥**：确保证书文件包含私钥
4. **Linux/macOS 权限**：在非 Windows 系统上，确保证书文件权限正确（600 或 400）
5. **更新到最新版本**：确保使用本项目的最新版本，包含 .NET 9.0 兼容性修复

### 故障排查

#### 1. 检查证书文件

```bash
# 验证证书是否有效（Windows PowerShell）
$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2("apiclient_cert.p12", "password")
$cert | Format-List

# 验证证书是否有效（Linux/macOS）
openssl pkcs12 -info -in apiclient_cert.p12
```

#### 2. 启用详细日志

在配置中启用 Senparc.Weixin 的详细日志，查看证书加载过程：

```csharp
// appsettings.json
{
  "SenparcWeixinSetting": {
    "IsDebug": true,
    // ...其他配置
  }
}
```

#### 3. 常见错误信息

| 错误信息 | 可能原因 | 解决方案 |
|---------|---------|---------|
| "The SSL connection could not be established" | 证书加载失败 | 检查证书路径、密码、格式 |
| "Unable to read data from the transport connection" | TLS 协议不匹配 | 更新到包含 .NET 9.0 修复的版本 |
| "The credentials supplied to the package were not recognized" | 证书私钥权限问题 | 在 Linux/macOS 上检查文件权限 |

### 技术细节

#### X509KeyStorageFlags 说明

| 标志 | .NET 8.0 | .NET 9.0 | 说明 |
|------|----------|----------|------|
| **Exportable** | 可选 | 推荐 | 允许私钥导出，提高跨平台兼容性 |
| **PersistKeySet** | 必需 | 必需 | 将密钥持久化到密钥存储 |
| **MachineKeySet** | 推荐 | 仅 Windows | 在机器级别存储密钥（非 Windows 平台不支持）|

#### 平台差异

- **Windows**：完全支持所有 X509KeyStorageFlags
- **Linux**：不支持 MachineKeySet，使用 UserKeySet
- **macOS**：类似 Linux，需要特殊处理密钥存储

### 相关资源

- [.NET 9.0 Breaking Changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0)
- [X509Certificate2 Class Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2)
- [微信支付 API 文档](https://pay.weixin.qq.com/wiki/doc/api/index.html)

---

## English Version

### Issue Description

After upgrading to .NET 9.0, TenPayV3 refund operations may encounter SSL certificate errors:

```
Senparc.Weixin.Exceptions.WeixinException: The SSL connection could not be established, see inner exception
```

### Root Cause

.NET 9.0 introduced stricter validation for X509Certificate2 loading and handling:

1. **X509KeyStorageFlags.MachineKeySet** flag may fail on non-Windows platforms
2. More restrictive certificate private key permissions
3. TLS 1.3 is now the default, requiring explicit configuration in some cases

### Solution

This project has implemented the following compatibility fixes:

#### 1. Platform-Adaptive Certificate Loading Flags

```csharp
X509KeyStorageFlags storageFlags;

#if NET9_0_OR_GREATER
// .NET 9.0+: Use more compatible flag combination
storageFlags = X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet;
if (System.OperatingSystem.IsWindows())
{
    // Only use MachineKeySet on Windows
    storageFlags |= X509KeyStorageFlags.MachineKeySet;
}
#else
// Older .NET versions: Maintain original behavior
storageFlags = X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet;
#endif

var cert = new X509Certificate2(certPath, certPassword, storageFlags);
```

#### 2. Explicit SSL/TLS Protocol Configuration

```csharp
#if NET9_0_OR_GREATER
// Explicitly support TLS 1.2 and TLS 1.3
httpClientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 
                                | System.Security.Authentication.SslProtocols.Tls13;

// Ensure certificate selection callback handles client certificates correctly
httpClientHandler.ClientCertificateOptions = System.Net.Http.ClientCertificateOption.Manual;
#endif
```

### Usage Recommendations

#### Recommended Configuration (.NET 8.0 LTS)

As suggested by [@JeffreySu](https://github.com/JeffreySu) in the issue, .NET 8.0 is the Long-Term Support (LTS) version recommended for production:

- ✅ .NET 8.0 - Recommended (LTS, supported until November 2026)
- ⚠️ .NET 9.0 - Short-term support (supported until May 2025)
- 🔮 .NET 10.0 - Will be released in November 2025 (next LTS version)

#### If You Must Use .NET 9.0

Ensure:

1. **Correct certificate file format**: Use .p12 or .pfx format
2. **Correct certificate password**: Verify the certificate password
3. **Certificate contains private key**: Ensure the certificate file includes the private key
4. **Linux/macOS permissions**: On non-Windows systems, ensure certificate file permissions are correct (600 or 400)
5. **Update to latest version**: Use the latest version of this project with .NET 9.0 compatibility fixes

### Troubleshooting

#### 1. Check Certificate File

```bash
# Verify certificate (Windows PowerShell)
$cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2("apiclient_cert.p12", "password")
$cert | Format-List

# Verify certificate (Linux/macOS)
openssl pkcs12 -info -in apiclient_cert.p12
```

#### 2. Enable Detailed Logging

Enable detailed logging in Senparc.Weixin configuration:

```csharp
// appsettings.json
{
  "SenparcWeixinSetting": {
    "IsDebug": true,
    // ...other settings
  }
}
```

#### 3. Common Error Messages

| Error Message | Possible Cause | Solution |
|--------------|----------------|----------|
| "The SSL connection could not be established" | Certificate loading failed | Check certificate path, password, format |
| "Unable to read data from the transport connection" | TLS protocol mismatch | Update to version with .NET 9.0 fixes |
| "The credentials supplied to the package were not recognized" | Certificate private key permission issue | Check file permissions on Linux/macOS |

### Technical Details

#### X509KeyStorageFlags Explanation

| Flag | .NET 8.0 | .NET 9.0 | Description |
|------|----------|----------|-------------|
| **Exportable** | Optional | Recommended | Allows private key export, improves cross-platform compatibility |
| **PersistKeySet** | Required | Required | Persists keys to key storage |
| **MachineKeySet** | Recommended | Windows Only | Stores keys at machine level (not supported on non-Windows platforms) |

#### Platform Differences

- **Windows**: Fully supports all X509KeyStorageFlags
- **Linux**: Does not support MachineKeySet, uses UserKeySet
- **macOS**: Similar to Linux, requires special key storage handling

### Related Resources

- [.NET 9.0 Breaking Changes](https://learn.microsoft.com/en-us/dotnet/core/compatibility/9.0)
- [X509Certificate2 Class Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2)
- [WeChat Pay API Documentation](https://pay.weixin.qq.com/wiki/doc/api/index.html)
