# WeixinDev - 微信开发助手 VSCode 扩展

🚀 **Senparc.Weixin SDK AI 智能代码生成工具**

## 📖 简介

WeixinDev 是一个专为微信开发者设计的 VSCode 扩展，集成了 [Senparc.Weixin SDK](https://sdk.weixin.senparc.com) 的 AI 助手功能，让您能够在 VSCode 中快速查询和插入微信接口代码。

## ✨ 主要功能

### 🎯 侧栏AI助手
- **智能查询**: 在侧栏窗口输入需求，AI 自动生成对应的微信接口代码
- **一键跳转**: 支持直接跳转到 Senparc.Weixin AI 助手网站
- **示例查询**: 内置常用查询示例，快速上手

### 🖱️ 右键菜单集成
- **插入微信接口**: 在编辑器中右键选择"插入微信接口"
- **智能插入**: 自动在光标位置插入生成的代码
- **格式优化**: 自动格式化插入的代码，保持代码风格一致

### 🤖 智能代码生成
- **代码提取**: 自动提取 `<code class="language-csharp">` 标签内的 C# 代码
- **注释生成**: 自动提取 `class="tips-section"` 内容作为代码注释
- **占位符支持**: 智能识别并创建代码占位符，方便快速修改

## 🚀 快速开始

### 安装
1. 在 VSCode 扩展商店搜索 "WeixinDev"
2. 点击安装
3. 重新加载 VSCode

### 使用方式

#### 方式一：侧栏查询
1. 打开侧栏中的"微信开发助手"面板
2. 在输入框中描述您需要的功能，例如：
   - "发送模板消息给用户"
   - "获取微信用户基本信息"
   - "创建小程序二维码"
3. 点击"🎯 生成代码"按钮
4. 代码将自动插入到当前光标位置

#### 方式二：右键菜单
1. 在 C# 文件中将光标定位到需要插入代码的位置
2. 右键选择"插入微信接口"
3. 在弹出的输入框中输入需求
4. 点击确定，代码将自动插入

## 📋 支持的接口类型

### 🔥 微信公众号
- 模板消息发送
- 用户信息获取
- 自定义菜单管理
- 素材管理
- 群发消息

### 📱 微信小程序
- 小程序码生成
- 用户信息获取
- 数据分析
- 内容安全检测

### 💳 微信支付
- 统一下单
- 查询订单
- 申请退款
- 支付回调处理

### 🏢 企业微信
- 消息发送
- 通讯录管理
- 应用管理
- 会话存档

## ⚙️ 配置选项

在 VSCode 设置中可以配置以下选项：

```json
{
  "weixindev.apiUrl": "https://sdk.weixin.senparc.com/AiDoc",
  "weixindev.autoInsertComments": true,
  "weixindev.enableDebugMode": false
}
```

### 配置说明
- `apiUrl`: AI 助手 API 地址
- `autoInsertComments`: 是否自动插入提示注释
- `enableDebugMode`: 是否启用调试模式

## 📝 使用示例

### 发送模板消息
输入：`发送模板消息给用户`

生成代码：
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

### 获取用户信息
输入：`获取微信用户基本信息`

生成代码：
```csharp
// 获取微信用户基本信息，需要用户已关注公众号
var userInfo = await Senparc.Weixin.MP.AdvancedAPIs.UserApi
    .InfoAsync(accessToken, openId);
    
if (userInfo.errcode == ReturnCode.请求成功)
{
    var nickname = userInfo.nickname;
    var headimgurl = userInfo.headimgurl;
    // 处理用户信息
}
```

## 🔧 开发指南

### 本地开发
```bash
# 克隆项目
git clone https://github.com/JeffreySu/WeiXinMPSDK.git
cd WeiXinMPSDK/tools/WeixinDev

# 安装依赖
npm install

# 编译代码
npm run compile

# 启动开发模式
npm run watch
```

### 打包扩展
```bash
# 打包为 .vsix 文件
npm run package

# 安装扩展到 VSCode
npm run install-extension
```

## 🤝 贡献指南

欢迎提交 Issue 和 Pull Request！

1. Fork 本项目
2. 创建您的功能分支 (`git checkout -b feature/AmazingFeature`)
3. 提交您的更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 打开一个 Pull Request

## 📄 许可证

本项目采用 Apache-2.0 许可证 - 查看 [LICENSE](LICENSE) 文件了解详情

## 🔗 相关链接

- [Senparc.Weixin SDK](https://github.com/JeffreySu/WeiXinMPSDK)
- [Senparc.Weixin 官网](https://sdk.weixin.senparc.com)
- [AI 助手在线版](https://sdk.weixin.senparc.com/AiDoc)
- [微信开发文档](https://developers.weixin.qq.com/)

## 💬 技术支持

- 🐛 [提交 Bug](https://github.com/JeffreySu/WeiXinMPSDK/issues)
- 💡 [功能建议](https://github.com/JeffreySu/WeiXinMPSDK/issues)
- 📧 [邮件支持](mailto:support@senparc.com)

---

**🎉 感谢使用 WeixinDev！让微信开发变得更简单！**
