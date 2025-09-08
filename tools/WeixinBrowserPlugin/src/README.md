# Senparc.Weixin.AI Chrome Extension

一个专为微信开发者设计的Chrome浏览器插件，集成Senparc.Weixin SDK的AI助手功能，提供智能化的微信接口查询和代码生成服务。

## 🚀 功能特性

- **智能检测**: 自动检测当前页面是否为支持的微信文档页面
- **一键启动**: 在支持的页面左侧显示"Senparc.Weixin.AI"按钮
- **浮窗集成**: 点击按钮后打开集成AI助手的浮窗界面
- **上下文感知**: 自动将当前页面URL作为查询参数传递给AI助手
- **响应式设计**: 支持桌面端和移动端的完美显示
- **现代化UI**: 采用微信绿色主题，提供流畅的用户体验

## 📦 安装方法

### 开发者模式安装

1. 下载或克隆本项目到本地
2. 打开Chrome浏览器，进入扩展程序管理页面 (`chrome://extensions/`)
3. 开启右上角的"开发者模式"
4. 点击"加载已解压的扩展程序"
5. 选择项目文件夹
6. 插件安装完成！

### 从Chrome Web Store安装

*即将上线Chrome Web Store，敬请期待...*

## 🎯 使用方法

1. **访问微信文档**: 打开任意微信官方文档页面 (如 https://developers.weixin.qq.com/doc/)
2. **查看Logo按钮**: 页面左上角会自动出现"Senparc.Weixin.AI"绿色按钮
3. **打开AI助手**: 点击按钮打开AI助手浮窗
4. **智能查询**: AI助手会自动获取当前页面信息，提供相关的接口查询和代码示例

## 🛠️ 技术架构

### 文件结构

```
WeixinBrowserPlugin/
├── manifest.json          # Chrome扩展配置文件
├── content.js             # 内容脚本 - 主要功能逻辑
├── styles.css             # 样式文件
├── popup.html             # 弹窗页面
├── popup.js               # 弹窗脚本
├── icon.svg               # 插件图标
├── icons/                 # 不同尺寸图标文件夹
└── README.md              # 项目说明文档
```

### 核心组件

#### 1. Content Script (`content.js`)
- **WeixinAIAssistant类**: 主要功能控制器
- **页面检测**: 检测当前页面是否为微信文档
- **UI创建**: 动态创建Logo按钮和浮窗界面
- **事件处理**: 处理用户交互和键盘快捷键
- **SPA支持**: 监听页面URL变化，支持单页应用

#### 2. 样式系统 (`styles.css`)
- **响应式设计**: 支持桌面端、平板和移动端
- **现代化UI**: 采用微信绿色渐变主题
- **动画效果**: 流畅的过渡动画和交互反馈
- **暗色主题**: 自动适配系统暗色模式
- **高对比度**: 支持无障碍访问

#### 3. 弹窗界面 (`popup.html` + `popup.js`)
- **状态检测**: 实时检测当前标签页状态
- **快捷操作**: 提供快速访问AI助手的入口
- **链接导航**: 集成Senparc官网和GitHub项目链接

### 技术特性

- **Manifest V3**: 使用最新的Chrome扩展API
- **安全沙箱**: iframe采用安全沙箱模式
- **权限最小化**: 仅请求必要的权限
- **性能优化**: 懒加载和事件委托优化
- **错误处理**: 完善的错误处理和用户反馈

## 🔧 开发指南

### 环境要求

- Chrome 88+ 或其他基于Chromium的浏览器
- 支持ES6+的现代浏览器环境

### 本地开发

1. 克隆项目：
   ```bash
   git clone <repository-url>
   cd WeixinBrowserPlugin
   ```

2. 在Chrome中加载扩展：
   - 打开 `chrome://extensions/`
   - 启用"开发者模式"
   - 点击"加载已解压的扩展程序"
   - 选择项目文件夹

3. 开发调试：
   - 修改代码后，在扩展管理页面点击"重新加载"
   - 使用Chrome DevTools调试内容脚本
   - 在弹窗页面右键选择"检查"调试弹窗

### 代码规范

- 使用ES6+语法
- 采用类和模块化设计
- 遵循Chrome扩展最佳实践
- 添加详细的注释和错误处理

## 🌟 核心功能详解

### 支持的页面

插件仅在以下页面中激活：
- ✅ `https://developers.weixin.qq.com/*` - 微信开发者文档
- ✅ `https://pay.weixin.qq.com/doc*` - 微信支付文档

```javascript
isWeixinDocPage() {
  const url = window.location.href;
  const hostname = window.location.hostname;
  
  const allowedUrls = [
    'developers.weixin.qq.com',
    'pay.weixin.qq.com'
  ];
  
  const isAllowedDomain = allowedUrls.some(domain => hostname === domain);
  
  if (hostname === 'pay.weixin.qq.com') {
    return url.includes('/doc');
  }
  
  return isAllowedDomain;
}
```

### 动态UI创建

插件会在检测到支持的页面时，动态创建：
- Logo按钮：固定在页面左侧（避免遮挡网站Logo）
- 浮窗界面：居中显示，支持拖拽和缩放
- iframe集成：安全地嵌入AI助手页面

### 参数传递

当打开AI助手时，会自动将当前页面URL作为query参数传递：
```
https://sdk.weixin.senparc.com/AiDoc?query=<当前页面URL>
```

## 🎨 UI/UX设计

### 设计理念

- **微信风格**: 采用微信官方绿色主题 (#1aad19, #2dc653)
- **现代简约**: 扁平化设计，注重功能性
- **用户友好**: 直观的交互方式，最小化学习成本
- **响应式**: 适配各种屏幕尺寸和设备

### 交互设计

- **悬浮效果**: Logo按钮支持hover和active状态
- **动画过渡**: 所有状态变化都有流畅的过渡动画
- **键盘支持**: 支持ESC键关闭浮窗
- **点击外部**: 点击浮窗外部区域自动关闭

## 🔒 安全性

### 权限控制

- **最小权限原则**: 仅请求必要的`activeTab`和`storage`权限
- **域名限制**: 仅在微信官方域名下运行
- **沙箱模式**: iframe采用安全沙箱限制

### 数据安全

- **无数据收集**: 插件不收集用户个人信息
- **本地存储**: 配置信息仅存储在本地
- **HTTPS通信**: 所有网络请求均使用HTTPS协议

## 🚀 性能优化

### 加载优化

- **按需加载**: 仅在微信文档页面加载功能代码
- **资源压缩**: CSS和JS文件经过优化压缩
- **缓存策略**: 合理利用浏览器缓存机制

### 运行时优化

- **事件委托**: 减少事件监听器数量
- **防抖处理**: 避免频繁的DOM操作
- **内存管理**: 及时清理不需要的对象引用

## 🐛 故障排除

### 常见问题

1. **Logo按钮不显示**
   - 确认当前页面域名是否为 weixin.qq.com
   - 检查浏览器控制台是否有错误信息
   - 尝试刷新页面

2. **浮窗无法打开**
   - 检查网络连接是否正常
   - 确认是否被广告拦截器阻止
   - 查看浏览器弹窗设置

3. **AI助手加载失败**
   - 检查 sdk.weixin.senparc.com 是否可访问
   - 确认网络防火墙设置
   - 尝试清除浏览器缓存

### 调试方法

1. **开启开发者工具**：
   - 按F12或右键选择"检查"
   - 查看Console面板的错误信息

2. **检查扩展状态**：
   - 访问 `chrome://extensions/`
   - 查看插件是否正常启用
   - 点击"详细信息"查看权限设置

3. **重新加载扩展**：
   - 在扩展管理页面点击"重新加载"
   - 刷新微信文档页面

## 📈 更新日志

### v1.0.0 (2024-12-19)
- ✨ 初始版本发布
- 🎯 支持微信文档页面自动检测
- 🖱️ 添加Logo按钮和浮窗界面
- 📱 实现响应式设计
- 🔗 集成Senparc.Weixin.AI助手
- 🎨 采用微信官方设计风格

## 🤝 贡献指南

我们欢迎社区贡献！请遵循以下步骤：

1. Fork本项目
2. 创建功能分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 创建Pull Request

### 贡献类型

- 🐛 Bug修复
- ✨ 新功能开发
- 📝 文档改进
- 🎨 UI/UX优化
- ⚡ 性能提升
- 🔒 安全增强

## 📄 许可证

本项目采用 MIT 许可证 - 查看 [LICENSE](LICENSE) 文件了解详情。

## 🙏 致谢

- **Senparc团队**: 提供优秀的微信SDK和AI助手服务
- **Chrome团队**: 提供强大的扩展API
- **开源社区**: 提供各种优秀的开源工具和库

## 📞 联系我们

- **项目主页**: [GitHub Repository](https://github.com/your-username/WeixinBrowserPlugin)
- **问题反馈**: [GitHub Issues](https://github.com/your-username/WeixinBrowserPlugin/issues)
- **Senparc官网**: [https://sdk.weixin.senparc.com/](https://sdk.weixin.senparc.com/)
- **微信SDK文档**: [https://sdk.weixin.senparc.com/AiDoc](https://sdk.weixin.senparc.com/AiDoc)

---

**让微信开发更智能，让代码编写更高效！** 🚀
