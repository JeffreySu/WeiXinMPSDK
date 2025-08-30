# Release Template for GitHub

## Release Information Template

### Release Title Format:
```
Senparc.Weixin.AI Chrome Extension v{version}
```

### Release Description Template:

```markdown
# 🚀 Senparc.Weixin.AI Chrome Extension v{version}

> **发布日期：** {date}  
> **兼容性：** Chrome 88+ / Edge 88+ / Chromium-based browsers

## 📋 本次更新

### ✨ 新功能
- [ ] 新功能描述 1
- [ ] 新功能描述 2

### 🔧 功能改进
- [ ] 改进项描述 1
- [ ] 改进项描述 2

### 🐛 问题修复
- [ ] 修复问题描述 1
- [ ] 修复问题描述 2

### 🔒 安全性更新
- [ ] 安全性改进描述

### 📚 文档更新
- [ ] 文档更新说明

## 💻 安装方法

### 方法 1：下载安装包（推荐）

1. **下载发布包**
   - 点击下方 Assets 中的 `weixin-ai-extension-v{version}.zip`

2. **安装步骤**
   ```
   ① 解压下载的ZIP文件到本地文件夹
   ② 打开Chrome浏览器，访问 chrome://extensions/
   ③ 开启右上角的"开发者模式"
   ④ 点击"加载已解压的扩展程序"
   ⑤ 选择解压后的文件夹
   ⑥ 确认安装完成
   ```

### 方法 2：源码编译安装

```bash
# 克隆仓库
git clone https://github.com/your-username/weixin-ai-browser-extension.git
cd weixin-ai-browser-extension

# 切换到指定版本
git checkout v{version}

# 构建发布包（可选）
./scripts/build-release.sh

# 手动安装
# 然后按照方法1的安装步骤操作
```

## 🎯 使用方法

1. **访问微信文档**
   ```
   https://developers.weixin.qq.com/doc/
   ```

2. **启动AI助手**
   - 页面左上角会出现"Senparc.Weixin.AI"按钮
   - 点击按钮打开AI助手浮窗

3. **智能查询**
   - AI助手会自动识别当前页面内容
   - 提供相关的接口查询和代码示例

## 📋 系统要求

| 项目 | 要求 |
|------|------|
| **浏览器** | Chrome 88+ / Edge 88+ / Chromium-based |
| **操作系统** | Windows 10+ / macOS 10.14+ / Linux |
| **网络** | 需要互联网连接（访问AI服务） |
| **权限** | activeTab, storage, host_permissions |

## 🔄 从旧版本升级

### 自动升级（Chrome Web Store用户）
- Chrome会自动更新到最新版本
- 无需手动操作

### 手动升级（开发者模式用户）
```bash
# 1. 下载新版本
# 2. 在 chrome://extensions/ 中移除旧版本
# 3. 按照安装方法重新安装
```

## 💾 配置迁移

本版本的配置与之前版本兼容，升级后会自动迁移用户设置。

如需手动备份配置：
```javascript
// 在浏览器控制台执行
chrome.storage.local.get(null, (result) => {
    console.log('当前配置:', result);
});
```

## 🐛 已知问题

- [ ] 已知问题描述 1
- [ ] 已知问题描述 2

### 临时解决方案
- 问题1解决方案
- 问题2解决方案

## 🔗 相关链接

- **项目主页：** [GitHub Repository](https://github.com/your-username/weixin-ai-browser-extension)
- **问题反馈：** [Issues](https://github.com/your-username/weixin-ai-browser-extension/issues)
- **使用文档：** [USAGE.md](./USAGE.md)
- **安装指南：** [INSTALL.md](./INSTALL.md)
- **故障排除：** [TROUBLESHOOTING.md](./TROUBLESHOOTING.md)
- **Senparc官网：** [https://sdk.weixin.senparc.com/](https://sdk.weixin.senparc.com/)

## 🤝 贡献者

感谢以下贡献者对本版本的贡献：

- [@contributor1](https://github.com/contributor1) - 功能开发
- [@contributor2](https://github.com/contributor2) - Bug修复
- [@contributor3](https://github.com/contributor3) - 文档改进

## 📊 版本统计

| 指标 | 数值 |
|------|------|
| **代码行数** | ~{lines} lines |
| **文件数量** | {files} files |
| **发布包大小** | ~{size} KB |
| **兼容性** | Chrome 88+ |

## 🎉 下个版本预告

- [ ] 计划功能 1
- [ ] 计划功能 2
- [ ] 计划改进 1

预计发布时间：{next_release_date}

---

## 📥 下载文件

请在下方 **Assets** 区域下载对应文件：

- `weixin-ai-extension-v{version}.zip` - 完整安装包
- `Source code (zip)` - 源代码压缩包
- `Source code (tar.gz)` - 源代码tar包

---

**🌟 如果这个项目对您有帮助，请给我们一个Star！**

**💬 遇到问题？** 请在 [Issues](https://github.com/your-username/weixin-ai-browser-extension/issues) 中反馈

**🔄 想要贡献？** 查看 [CONTRIBUTING.md](./CONTRIBUTING.md) 了解如何参与开发
```

## Specific Version Examples

### v1.0.0 Release Example:

```markdown
# 🚀 Senparc.Weixin.AI Chrome Extension v1.0.0

> **发布日期：** 2024-12-19  
> **兼容性：** Chrome 88+ / Edge 88+ / Chromium-based browsers

## 📋 本次更新

### ✨ 新功能
- ✅ 支持微信文档页面自动检测和AI助手集成
- ✅ 一键启动AI助手浮窗，提供智能接口查询功能
- ✅ 响应式设计，完美适配桌面和移动端
- ✅ 现代化UI设计，采用微信官方绿色主题
- ✅ 上下文感知技术，自动传递页面信息给AI助手

### 🔧 功能特性
- ✅ 基于Chrome Extension Manifest V3最新标准
- ✅ 安全沙箱模式，保护用户隐私和数据安全
- ✅ 最小权限原则，仅请求必要的浏览器权限
- ✅ 性能优化，快速加载和流畅交互
- ✅ 支持键盘快捷键操作（ESC关闭浮窗）

### 🔒 安全性特性
- ✅ 仅在微信官方域名下运行
- ✅ 不收集用户个人信息
- ✅ 本地存储配置信息
- ✅ HTTPS安全通信

## 💻 安装方法

### 方法 1：下载安装包（推荐）

1. **下载发布包**
   - 点击下方 Assets 中的 `weixin-ai-extension-v1.0.0.zip`

2. **安装步骤**
   ```
   ① 解压下载的ZIP文件到本地文件夹
   ② 打开Chrome浏览器，访问 chrome://extensions/
   ③ 开启右上角的"开发者模式"
   ④ 点击"加载已解压的扩展程序"
   ⑤ 选择解压后的文件夹
   ⑥ 确认安装完成
   ```

### 方法 2：源码编译安装

```bash
# 克隆仓库
git clone https://github.com/your-username/weixin-ai-browser-extension.git
cd weixin-ai-browser-extension

# 切换到v1.0.0版本
git checkout v1.0.0

# 构建发布包（可选）
./scripts/build-release.sh

# 手动安装
# 然后按照方法1的安装步骤操作
```

## 🎯 使用方法

1. **访问微信文档**
   ```
   https://developers.weixin.qq.com/doc/
   ```

2. **启动AI助手**
   - 页面左上角会出现"Senparc.Weixin.AI"按钮
   - 点击按钮打开AI助手浮窗

3. **智能查询**
   - AI助手会自动识别当前页面内容
   - 提供相关的接口查询和代码示例

## 📋 系统要求

| 项目 | 要求 |
|------|------|
| **浏览器** | Chrome 88+ / Edge 88+ / Chromium-based |
| **操作系统** | Windows 10+ / macOS 10.14+ / Linux |
| **网络** | 需要互联网连接（访问AI服务） |
| **权限** | activeTab, storage, host_permissions |

## 🐛 已知问题

目前版本运行稳定，暂无已知重大问题。

如遇到任何问题，请在 [Issues](https://github.com/your-username/weixin-ai-browser-extension/issues) 中反馈。

## 🔗 相关链接

- **项目主页：** [GitHub Repository](https://github.com/your-username/weixin-ai-browser-extension)
- **问题反馈：** [Issues](https://github.com/your-username/weixin-ai-browser-extension/issues)
- **使用文档：** [USAGE.md](./USAGE.md)
- **安装指南：** [INSTALL.md](./INSTALL.md)
- **故障排除：** [TROUBLESHOOTING.md](./TROUBLESHOOTING.md)
- **Senparc官网：** [https://sdk.weixin.senparc.com/](https://sdk.weixin.senparc.com/)

## 📊 版本统计

| 指标 | 数值 |
|------|------|
| **代码行数** | ~800 lines |
| **文件数量** | 12 files |
| **发布包大小** | ~50 KB |
| **兼容性** | Chrome 88+ |

## 🎉 下个版本预告

- [ ] 支持更多微信开发平台页面
- [ ] 添加英文界面支持
- [ ] 增加离线模式功能
- [ ] 优化AI响应速度

预计发布时间：2025年1月

---

**🌟 如果这个项目对您有帮助，请给我们一个Star！**

**💬 遇到问题？** 请在 [Issues](https://github.com/your-username/weixin-ai-browser-extension/issues) 中反馈
```

## Release Checklist

### 发布前检查
- [ ] 版本号已更新（manifest.json）
- [ ] 更新日志已编写
- [ ] 代码已测试
- [ ] 文档已更新
- [ ] 发布包已构建
- [ ] 发布说明已准备

### 发布步骤
- [ ] 创建Git标签
- [ ] 推送标签到GitHub
- [ ] 创建GitHub Release
- [ ] 上传发布文件
- [ ] 发布Release
- [ ] 通知用户更新

### 发布后检查
- [ ] 下载链接正常
- [ ] 安装包完整
- [ ] 用户反馈监控
- [ ] 问题快速响应

