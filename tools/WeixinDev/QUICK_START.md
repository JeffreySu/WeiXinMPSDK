# 🚀 WeixinDev 快速使用指南

## 📦 安装扩展

### 方式一：本地安装（推荐）
```bash
# 进入项目目录
cd /Volumes/DevelopAndData/SenparcProjects/WeiXinMPSDK/tools/WeixinDev

# 安装到 VSCode
code --install-extension weixindev-1.0.0.vsix
```

### 方式二：VSCode命令面板安装
1. 打开 VSCode
2. 按 `Cmd+Shift+P` (Mac) 或 `Ctrl+Shift+P` (Windows)
3. 输入 "Extensions: Install from VSIX"
4. 选择 `weixindev-1.0.0.vsix` 文件

## 🎯 快速体验

### 1️⃣ 侧栏查询模式
1. **打开侧栏**：在 VSCode 左侧找到"微信开发助手"面板
2. **输入需求**：在输入框中描述功能，例如：
   ```
   发送模板消息给用户
   ```
3. **生成代码**：点击"🎯 生成代码"按钮
4. **查看结果**：代码将自动插入到当前编辑器光标位置

### 2️⃣ 右键菜单模式
1. **打开C#文件**：确保在 `.cs` 文件中操作
2. **定位光标**：将光标移动到要插入代码的位置
3. **右键菜单**：右键选择"插入微信接口"
4. **输入需求**：在弹窗中输入需求，如：
   ```
   获取微信用户基本信息
   ```
5. **确认插入**：点击确定，代码自动插入

## 💡 示例演示

### 示例1：发送模板消息
**输入**：`发送模板消息给用户`

**生成代码**：
```csharp
// 发送模板消息功能，需要先在微信公众平台配置模板消息
var templateData = new {
    first = new TemplateDataItem("您好，您有新的消息"),
    content = new TemplateDataItem("这是消息内容"),
    remark = new TemplateDataItem("感谢您的使用")
};

var result = await Senparc.Weixin.MP.AdvancedAPIs.TemplateApi
    .SendTemplateMessageAsync(appId, openId, templateId, url, templateData);
```

### 示例2：小程序码生成
**输入**：`创建小程序二维码`

**生成代码**：
```csharp
// 生成小程序码，scene参数最多32个字符
var result = await Senparc.Weixin.WxOpen.AdvancedAPIs.WxAppApi
    .GetWxaCodeUnlimitAsync(accessToken, scene, page, width: 430);
    
if (result.errcode == ReturnCode.请求成功)
{
    // result.Result 包含二维码图片数据
    var imageBytes = result.Result;
}
```

### 示例3：微信支付下单
**输入**：`微信支付统一下单`

**生成代码**：
```csharp
// 微信支付统一下单接口，需要配置微信支付商户信息
var unifiedOrderRequest = new UnifiedOrderRequestData(
    appId, mchId, "商品描述", outTradeNo, totalFee, 
    spbillCreateIp, notifyUrl, tradeType: TenPayV3Type.JSAPI, openid: openId);

var result = await TenPayV3.UnifiedOrderAsync(unifiedOrderRequest);
if (result.IsReturnCodeSuccess())
{
    // 处理支付结果
}
```

## 🔧 常用查询示例

在侧栏的"💡 示例查询"区域，您可以直接点击以下示例：

- 📧 发送模板消息给用户
- 👤 获取微信用户基本信息
- 📱 创建小程序二维码
- 💳 微信支付统一下单
- 🏢 企业微信发送消息
- 📋 获取公众号菜单

## ⚙️ 配置选项

在 VSCode 设置中搜索 "weixindev" 可以配置：

```json
{
    "weixindev.apiUrl": "https://sdk.weixin.senparc.com/AiDoc",
    "weixindev.autoInsertComments": true,
    "weixindev.enableDebugMode": false
}
```

## 🔍 故障排除

### 问题1：扩展无法激活
**解决方案**：
1. 确保 VSCode 版本 ≥ 1.80.0
2. 重新加载 VSCode 窗口（`Cmd+R` 或 `Ctrl+R`）

### 问题2：无法生成代码
**解决方案**：
1. 检查网络连接是否正常
2. 确保能够访问 https://sdk.weixin.senparc.com
3. 尝试在浏览器中打开AI助手验证

### 问题3：代码插入位置不正确
**解决方案**：
1. 确保在 C# 文件中操作
2. 将光标精确定位到要插入的位置
3. 右键菜单只在 `.cs` 文件中出现

## 📞 技术支持

- 🐛 [报告问题](https://github.com/JeffreySu/WeiXinMPSDK/issues)
- 💡 [功能建议](https://github.com/JeffreySu/WeiXinMPSDK/discussions)
- 📧 [邮件支持](mailto:support@senparc.com)

## 🔄 卸载扩展

如需卸载：
1. 打开 VSCode 扩展面板
2. 搜索 "WeixinDev"
3. 点击齿轮图标选择"卸载"

---

**🎉 享受更高效的微信开发体验！**
