# 多商户配置示例 (Multi-Merchant Configuration Example)

## 概述 (Overview)

本文档介绍如何在应用程序中通过代码动态注册和管理多个微信支付商户，而不仅仅依赖配置文件。

This document demonstrates how to dynamically register and manage multiple WeChat Pay merchants through code instead of relying solely on configuration files.

相关问题 (Related Issues):
- QA-29836: https://weixin.senparc.com/QA-29836
- GitHub Issue #3267: https://github.com/JeffreySu/WeiXinMPSDK/issues/3267

---

## 方法一：通过配置文件注册 (Method 1: Register via Configuration File)

### 1.1 配置 appsettings.json

```json
{
  "SenparcWeixinSetting": {
    "Items": {
      "商户A": {
        "TenPayV3_AppId": "wxabcdefg1234567",
        "TenPayV3_AppSecret": "app_secret_here",
        "TenPayV3_MchId": "1234567890",
        "TenPayV3_Key": "api_key_here",
        "TenPayV3_TenpayNotify": "https://yourdomain.com/TenPay/PayNotify",
        "TenPayV3_PrivateKey": "~/App_Data/cert/apiclient_key.pem",
        "TenPayV3_SerialNumber": "serial_number_here",
        "TenPayV3_APIv3Key": "apiv3_key_here"
      },
      "商户B": {
        "TenPayV3_AppId": "wxhijklmn7890123",
        "TenPayV3_AppSecret": "another_app_secret",
        "TenPayV3_MchId": "0987654321",
        "TenPayV3_Key": "another_api_key",
        "TenPayV3_TenpayNotify": "https://yourdomain.com/TenPay/PayNotify",
        "TenPayV3_PrivateKey": "~/App_Data/cert/merchant_b_key.pem",
        "TenPayV3_SerialNumber": "another_serial_number",
        "TenPayV3_APIv3Key": "another_apiv3_key"
      }
    }
  }
}
```

### 1.2 在 Program.cs 中注册

```csharp
var registerService = app.UseSenparcWeixin(app.Environment,
    null,
    null,
    register => { /* CO2NET 全局配置 */ },
    (register, weixinSetting) =>
    {
        // 注册商户A
        register.RegisterTenpayApiV3(weixinSetting.Items["商户A"], "商户A");
        
        // 注册商户B
        register.RegisterTenpayApiV3(weixinSetting.Items["商户B"], "商户B");
    });
```

---

## 方法二：通过代码动态注册（推荐用于多商户场景）(Method 2: Register Programmatically - Recommended for Multi-Merchant)

### 2.1 创建商户信息并注册

```csharp
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Entities;

var registerService = app.UseSenparcWeixin(app.Environment,
    null,
    null,
    register => { /* CO2NET 全局配置 */ },
    (register, weixinSetting) =>
    {
        // 动态注册商户A
        register.RegisterTenpayApiV3(() => new TenPayV3Info(
            appId: "wxabcdefg1234567",
            appSecret: "app_secret_here",
            mchId: "1234567890",
            key: "api_key_here",
            certPath: "/path/to/cert.p12",  // 可选，某些场景需要
            certSecret: "cert_password",     // 可选
            tenPayV3Notify: "https://yourdomain.com/TenPay/PayNotify",
            tenPayV3WxOpenNotify: "https://yourdomain.com/TenPay/WxOpenNotify",
            privateKey: "-----BEGIN PRIVATE KEY-----\nMIIE...\n-----END PRIVATE KEY-----",
            serialNumber: "serial_number_here",
            apiV3Key: "apiv3_key_here",
            certType: CertType.RSA  // 或 CertType.SM（国密）
        ), "商户A");

        // 动态注册商户B
        register.RegisterTenpayApiV3(() => new TenPayV3Info(
            appId: "wxhijklmn7890123",
            appSecret: "another_app_secret",
            mchId: "0987654321",
            key: "another_api_key",
            certPath: "/path/to/merchant_b_cert.p12",
            certSecret: "merchant_b_cert_password",
            tenPayV3Notify: "https://yourdomain.com/TenPay/PayNotify",
            tenPayV3WxOpenNotify: "https://yourdomain.com/TenPay/WxOpenNotify",
            privateKey: "-----BEGIN PRIVATE KEY-----\nMIIE...\n-----END PRIVATE KEY-----",
            serialNumber: "another_serial_number",
            apiV3Key: "another_apiv3_key",
            certType: CertType.RSA
        ), "商户B");
    });
```

### 2.2 从数据库动态加载商户信息

```csharp
// 假设你有一个服务来获取商户配置
public class MerchantConfigService
{
    public List<MerchantConfig> GetAllMerchants()
    {
        // 从数据库或其他数据源获取商户配置
        return new List<MerchantConfig>
        {
            new MerchantConfig { /* ... */ },
            // ...
        };
    }
}

// 在 Program.cs 中动态注册所有商户
var merchantService = new MerchantConfigService();
var merchants = merchantService.GetAllMerchants();

var registerService = app.UseSenparcWeixin(app.Environment,
    null,
    null,
    register => { /* CO2NET 全局配置 */ },
    (register, weixinSetting) =>
    {
        foreach (var merchant in merchants)
        {
            register.RegisterTenpayApiV3(() => new TenPayV3Info(
                appId: merchant.AppId,
                appSecret: merchant.AppSecret,
                mchId: merchant.MchId,
                key: merchant.Key,
                certPath: merchant.CertPath,
                certSecret: merchant.CertSecret,
                tenPayV3Notify: merchant.NotifyUrl,
                tenPayV3WxOpenNotify: merchant.WxOpenNotifyUrl,
                privateKey: merchant.PrivateKey,
                serialNumber: merchant.SerialNumber,
                apiV3Key: merchant.ApiV3Key,
                certType: merchant.CertType
            ), merchant.Name);
        }
    });
```

---

## 使用已注册的商户 (Using Registered Merchants)

### 3.1 获取特定商户的配置信息

```csharp
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.Helpers;

// 方式1：通过商户ID获取
var key = TenPayHelper.GetRegisterKey(mchId: "1234567890", subMchId: "");
var tenPayV3Info = TenPayV3InfoCollection.Data[key];

// 方式2：如果知道注册的名称，可以从 Config 获取 MchId
var merchantName = "商户A";
var mchId = Senparc.Weixin.Config.SenparcWeixinSetting.Items[merchantName].TenPayV3_MchId;
var subMchId = Senparc.Weixin.Config.SenparcWeixinSetting.Items[merchantName].TenPayV3_SubMchId;
var key2 = TenPayHelper.GetRegisterKey(mchId, subMchId);
var tenPayV3Info2 = TenPayV3InfoCollection.Data[key2];
```

### 3.2 查询特定商户的订单

```csharp
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;

public class PaymentController : Controller
{
    private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;
    private readonly BasePayApis _basePayApis;

    public PaymentController()
    {
        // 获取特定商户的配置（例如"商户A"）
        _tenpayV3Setting = Senparc.Weixin.Config.SenparcWeixinSetting.Items["商户A"];
        _basePayApis = new BasePayApis(_tenpayV3Setting);
    }

    /// <summary>
    /// 查询订单 - 使用商户订单号
    /// </summary>
    public async Task<IActionResult> QueryOrderByOutTradeNo(string out_trade_no, string merchantName = "商户A")
    {
        // 动态获取指定商户的配置
        var setting = Senparc.Weixin.Config.SenparcWeixinSetting.Items[merchantName];
        var basePayApis = new BasePayApis(setting);
        
        var queryData = new QueryRequestData(setting.TenPayV3_MchId, out_trade_no);
        var result = await basePayApis.OrderQueryByOutTradeNoAsync(queryData);
        
        return Json(result);
    }

    /// <summary>
    /// 查询订单 - 使用微信支付订单号
    /// </summary>
    public async Task<IActionResult> QueryOrderByTransactionId(string transaction_id, string merchantName = "商户A")
    {
        var setting = Senparc.Weixin.Config.SenparcWeixinSetting.Items[merchantName];
        var basePayApis = new BasePayApis(setting);
        
        var queryData = new QueryRequestData(setting.TenPayV3_MchId, transaction_id);
        var result = await basePayApis.OrderQueryByTransactionIdAsync(queryData);
        
        return Json(result);
    }
}
```

### 3.3 根据用户/订单动态选择商户

```csharp
public class SmartPaymentController : Controller
{
    /// <summary>
    /// 根据业务逻辑动态选择商户处理支付
    /// </summary>
    public async Task<IActionResult> CreateOrder(string userId, decimal amount)
    {
        // 根据业务逻辑确定使用哪个商户
        string merchantName = DetermineMerchantForUser(userId);
        
        var setting = Senparc.Weixin.Config.SenparcWeixinSetting.Items[merchantName];
        var basePayApis = new BasePayApis(setting);
        
        // 创建订单
        var requestData = new TransactionsRequestData(
            appid: setting.TenPayV3_AppId,
            mchid: setting.TenPayV3_MchId,
            description: "商品描述",
            out_trade_no: GenerateOrderNo(),
            notify_url: setting.TenPayV3_TenpayNotify,
            amount: new() { total = (int)(amount * 100), currency = "CNY" },
            payer: new() { openid = GetUserOpenId(userId) }
        );
        
        var result = await basePayApis.TransactionsAsync(requestData);
        return Json(result);
    }
    
    private string DetermineMerchantForUser(string userId)
    {
        // 实现你的业务逻辑
        // 例如：根据用户类型、地区、订单金额等选择不同商户
        if (IsVipUser(userId))
            return "商户A";  // VIP用户使用商户A
        else
            return "商户B";  // 普通用户使用商户B
    }
    
    private bool IsVipUser(string userId)
    {
        // 实现判断逻辑
        return false;
    }
    
    private string GenerateOrderNo()
    {
        return $"ORDER_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}";
    }
    
    private string GetUserOpenId(string userId)
    {
        // 获取用户的 OpenId
        return "user_openid";
    }
}
```

---

## 服务商模式（Service Provider Mode）

如果你是服务商，需要管理多个特约商户（Sub-Merchants），可以这样配置：

```csharp
register.RegisterTenpayApiV3(() => new TenPayV3Info(
    appId: "服务商AppId",
    appSecret: "服务商AppSecret",
    mchId: "服务商MchId",
    key: "服务商Key",
    certPath: "/path/to/sp_cert.p12",
    certSecret: "sp_cert_password",
    subAppId: "特约商户AppId",        // 特约商户的AppId
    subAppSecret: "特约商户AppSecret",  // 特约商户的AppSecret
    subMchId: "特约商户MchId",         // 特约商户的商户号
    tenPayV3Notify: "https://yourdomain.com/TenPay/PayNotify",
    tenPayV3WxOpenNotify: "https://yourdomain.com/TenPay/WxOpenNotify",
    privateKey: "服务商私钥",
    serialNumber: "服务商证书序列号",
    apiV3Key: "服务商APIv3密钥",
    certType: CertType.RSA
), "特约商户名称");

// 查询订单时使用服务商和特约商户的信息
var queryData = new QueryRequestData(
    sp_mchid: "服务商MchId",
    sub_mchid: "特约商户MchId",
    out_trade_no: "订单号"
);
var result = await basePayApis.OrderQueryByOutTradeNoAsync(queryData);
```

---

## 重要提示 (Important Notes)

### 安全性 (Security)

1. **私钥保护**：私钥（PrivateKey）和 API 密钥（Key、APIv3Key）属于高度敏感信息，请：
   - 不要将密钥硬编码在代码中
   - 不要提交到版本控制系统
   - 建议使用环境变量、密钥管理服务（如 Azure Key Vault）或加密配置

2. **证书管理**：
   - 定期更新证书
   - 妥善保管证书密码
   - 使用文件系统权限限制证书文件访问

### 性能优化 (Performance)

1. **单例模式**：`BasePayApis` 实例可以考虑使用依赖注入并注册为单例或作用域服务
2. **缓存配置**：商户配置信息可以缓存，避免频繁查询数据库
3. **连接池**：SDK 内部使用 `IHttpClientFactory`，会自动管理 HTTP 连接

### TenPayV3InfoCollection 的 Key 规则

TenPayV3InfoCollection 使用 `MchId + "_" + SubMchId` 作为 Key：
- 普通商户：Key = `"1234567890_"` （MchId + 空字符串）
- 服务商模式：Key = `"服务商MchId_特约商户MchId"`

---

## 常见问题 (FAQ)

### Q1: 如何列出所有已注册的商户？

```csharp
var allMerchants = TenPayV3InfoCollection.Data;
foreach (var kvp in allMerchants)
{
    Console.WriteLine($"Key: {kvp.Key}, MchId: {kvp.Value.MchId}, AppId: {kvp.Value.AppId}");
}
```

### Q2: 可以在运行时添加新商户吗？

可以！使用 `TenPayV3InfoCollection.Register()` 方法：

```csharp
var newMerchantInfo = new TenPayV3Info(
    // ... 商户参数
);
TenPayV3InfoCollection.Register(newMerchantInfo, "新商户名称");
```

### Q3: 如何处理多个商户的回调通知？

在回调 URL 中添加商户标识：

```csharp
// 注册时
tenPayV3Notify: "https://yourdomain.com/TenPay/PayNotify?merchant=商户A"

// 回调处理
public async Task<IActionResult> PayNotify(string merchant)
{
    var setting = Senparc.Weixin.Config.SenparcWeixinSetting.Items[merchant];
    // 使用对应商户的配置验证和处理回调
    // ...
}
```

### Q4: 需要什么参数才能查询订单？

查询订单需要：
1. **商户订单号**（out_trade_no）或 **微信支付订单号**（transaction_id）
2. **商户号**（MchId）
3. 对应商户的 **BasePayApis** 实例

```csharp
// 最小查询示例
var setting = Senparc.Weixin.Config.SenparcWeixinSetting.Items["商户名称"];
var basePayApis = new BasePayApis(setting);
var queryData = new QueryRequestData(setting.TenPayV3_MchId, "订单号");
var result = await basePayApis.OrderQueryByOutTradeNoAsync(queryData);
```

---

## 参考资料 (References)

- 官方文档：https://weixin.senparc.com
- GitHub 仓库：https://github.com/JeffreySu/WeiXinMPSDK
- 微信支付 API v3 文档：https://pay.weixin.qq.com/wiki/doc/apiv3/
- 示例代码：`/Samples/TenPayV3/`

---

**更新日期**: 2026-01-28  
**相关版本**: Senparc.Weixin.TenPayV3 v2.3.3+
