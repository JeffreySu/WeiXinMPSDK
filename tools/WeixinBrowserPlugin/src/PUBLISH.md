# 发布指南

## 🎯 发布方式选择

### 1. Chrome Web Store 发布（推荐）✨

**优势：**
- 用户最容易安装和更新
- 官方审核，安全性有保障
- 自动更新机制
- 用户评价和反馈系统

**发布步骤：**

#### 1.1 准备工作

1. **注册Chrome Web Store开发者账号**
   - 访问：https://chrome.google.com/webstore/devconsole
   - 使用Google账号登录
   - 支付一次性注册费用：$5 USD
   - 完成身份验证

2. **准备发布资源**
   ```bash
   # 创建发布资源文件夹
   mkdir release-assets
   cd release-assets
   ```

3. **创建应用图标**（需要以下尺寸）：
   - 16x16px - 工具栏图标
   - 48x48px - 扩展管理页面
   - 128x128px - Chrome Web Store 详情页
   - 以PNG格式保存，背景透明

4. **准备宣传图片**：
   - 小宣传图：440x280px
   - 大宣传图：920x680px
   - 截屏图片：1280x800px 或 640x400px（建议3-5张）

#### 1.2 项目打包

1. **清理项目文件**
   ```bash
   # 创建发布版本文件夹
   mkdir weixin-ai-extension-v1.0.0
   
   # 复制必要文件
   cp manifest.json weixin-ai-extension-v1.0.0/
   cp content.js weixin-ai-extension-v1.0.0/
   cp styles.css weixin-ai-extension-v1.0.0/
   cp popup.html weixin-ai-extension-v1.0.0/
   cp popup.js weixin-ai-extension-v1.0.0/
   cp icon.svg weixin-ai-extension-v1.0.0/
   cp -r icons/ weixin-ai-extension-v1.0.0/
   ```

2. **创建ZIP包**
   ```bash
   zip -r weixin-ai-extension-v1.0.0.zip weixin-ai-extension-v1.0.0/
   ```

#### 1.3 上传到Chrome Web Store

1. **访问开发者控制台**
   - https://chrome.google.com/webstore/devconsole

2. **创建新项目**
   - 点击"Add new item"
   - 上传ZIP文件
   - 等待基础检查完成

3. **填写商店信息**

   **基本信息：**
   ```
   名称：Senparc.Weixin.AI Assistant
   摘要：微信文档页面AI助手，提供智能接口查询功能
   类别：Developer Tools
   语言：中文（简体）
   ```

   **详细描述：**
   ```
   🚀 专为微信开发者设计的Chrome浏览器插件

   ✨ 主要功能：
   • 智能检测微信官方文档页面
   • 一键启动AI助手，获取接口查询和代码示例
   • 上下文感知，自动传递当前页面信息
   • 现代化UI设计，完美融入微信文档界面

   🎯 适用场景：
   • 微信公众号开发
   • 微信小程序开发
   • 微信支付集成
   • Senparc.Weixin SDK使用

   📱 支持平台：
   • 桌面端Chrome浏览器
   • 基于Chromium的其他浏览器

   🔒 隐私保护：
   • 仅在微信官方文档页面运行
   • 不收集用户个人信息
   • 本地存储配置信息
   ```

4. **上传宣传资源**
   - 上传各尺寸图标
   - 添加应用截图
   - 添加宣传图片

5. **设置权限说明**
   ```
   activeTab：检测当前页面是否为微信文档页面
   storage：保存用户设置（可选）
   host_permissions：仅在微信官方域名和Senparc服务域名下运行
   ```

6. **提交审核**
   - 完善所有必填信息
   - 点击"Submit for review"
   - 等待Google审核（通常1-3个工作日）

#### 1.4 审核通过后

1. **发布设置**
   - 选择发布到特定地区
   - 设置可见性（公开/私有/仅限组织）

2. **版本管理**
   - 后续更新通过上传新的ZIP包
   - 版本号需要递增（在manifest.json中）

---

### 2. GitHub Releases 发布 🐙

**优势：**
- 开源透明
- 技术用户友好
- 版本历史清晰
- 免费

**发布步骤：**

#### 2.1 准备Git仓库

1. **初始化Git仓库**（如果还没有）
   ```bash
   git init
   git add .
   git commit -m "Initial commit: Weixin AI Browser Extension v1.0.0"
   ```

2. **创建GitHub仓库**
   - 访问GitHub，创建新仓库
   - 仓库名建议：`weixin-ai-browser-extension`
   - 添加README.md、LICENSE等文件

3. **推送代码**
   ```bash
   git remote add origin https://github.com/yourusername/weixin-ai-browser-extension.git
   git branch -M main
   git push -u origin main
   ```

#### 2.2 创建Release

1. **打标签**
   ```bash
   git tag -a v1.0.0 -m "Release version 1.0.0"
   git push origin v1.0.0
   ```

2. **在GitHub创建Release**
   - 访问仓库页面
   - 点击"Releases" → "Create a new release"
   - 选择标签：v1.0.0
   - 填写发布信息

3. **Release信息模板**
   ```markdown
   # Senparc.Weixin.AI Chrome Extension v1.0.0

   ## 🚀 新功能
   - ✨ 支持微信文档页面自动检测
   - 🎯 一键启动AI助手浮窗
   - 📱 响应式设计，支持多端适配
   - 🔗 集成Senparc.Weixin.AI服务

   ## 📦 安装方法

   ### 方法1：下载安装包
   1. 下载下方的 `weixin-ai-extension-v1.0.0.zip`
   2. 解压到本地文件夹
   3. 打开Chrome浏览器，访问 `chrome://extensions/`
   4. 开启"开发者模式"
   5. 点击"加载已解压的扩展程序"，选择解压后的文件夹

   ### 方法2：克隆源码
   ```bash
   git clone https://github.com/yourusername/weixin-ai-browser-extension.git
   cd weixin-ai-browser-extension
   ```
   然后按照方法1的步骤3-5进行安装。

   ## 🔧 使用方法
   1. 访问任意微信官方文档页面
   2. 点击页面左上角的"Senparc.Weixin.AI"按钮
   3. 在弹出的浮窗中使用AI助手功能

   ## 📋 系统要求
   - Chrome 88+ 或基于Chromium的浏览器
   - 网络连接（用于访问AI服务）

   ## 🐛 问题反馈
   如果遇到问题，请在 [Issues](https://github.com/yourusername/weixin-ai-browser-extension/issues) 中反馈。
   ```

4. **上传发布文件**
   - 上传打包好的ZIP文件
   - 可以上传多个格式（ZIP、tar.gz等）

---

### 3. 企业内部发布 🏢

**适用场景：**
- 企业内部使用
- 特定用户群体
- 测试版本发布

**发布方式：**

#### 3.1 通过企业G Suite

1. **Google Workspace管理**
   - 管理员可以在Google Workspace中部署扩展
   - 强制安装到组织内所有用户

2. **创建企业应用**
   - 在Chrome Web Store Developer Console中
   - 选择"Private" visibility
   - 仅对特定域名用户可见

#### 3.2 直接分发

1. **创建安装包**
   ```bash
   # 创建完整的安装包
   mkdir enterprise-package
   cp -r * enterprise-package/
   cd enterprise-package
   
   # 创建安装脚本
   cat > install.bat << 'EOF'
   @echo off
   echo 正在安装 Senparc.Weixin.AI Assistant...
   echo.
   echo 请按照以下步骤操作：
   echo 1. 打开Chrome浏览器
   echo 2. 地址栏输入：chrome://extensions/
   echo 3. 开启右上角的"开发者模式"
   echo 4. 点击"加载已解压的扩展程序"
   echo 5. 选择当前文件夹
   echo.
   pause
   EOF
   ```

2. **创建用户手册**
   - 详细的安装步骤
   - 使用方法说明
   - 常见问题解答

---

## 📋 发布前检查清单

### 代码质量检查 ✅

- [ ] 所有功能正常工作
- [ ] 错误处理完善
- [ ] 性能优化完成
- [ ] 安全性检查通过
- [ ] 兼容性测试完成

### 文档准备 ✅

- [ ] README.md 完整
- [ ] INSTALL.md 清晰
- [ ] USAGE.md 详细
- [ ] TROUBLESHOOTING.md 全面
- [ ] LICENSE 文件存在

### 发布资源 ✅

- [ ] 各尺寸图标准备完成
- [ ] 宣传截图拍摄完成
- [ ] 描述文案撰写完成
- [ ] 版本号更新正确

### 测试验证 ✅

- [ ] 在多个Chrome版本测试
- [ ] 在不同操作系统测试
- [ ] 功能完整性测试
- [ ] 性能压力测试

---

## 🔄 版本更新流程

### 更新版本号

1. **修改manifest.json**
   ```json
   {
     "version": "1.0.1"  // 递增版本号
   }
   ```

2. **遵循语义化版本**
   - 主版本号：重大不兼容更新
   - 次版本号：新功能添加
   - 修订号：Bug修复

### 发布更新

1. **Chrome Web Store**
   - 上传新的ZIP包
   - 更新商店信息
   - 提交审核

2. **GitHub Releases**
   - 创建新的Git标签
   - 发布新的Release
   - 更新文档

---

## 📊 发布后监控

### 用户反馈收集

1. **Chrome Web Store评价**
   - 定期查看用户评价
   - 及时回复用户问题

2. **GitHub Issues**
   - 处理用户提交的问题
   - 收集功能建议

### 数据分析

1. **使用统计**
   - Chrome Web Store提供的安装数据
   - 用户活跃度分析

2. **错误监控**
   - 收集崩溃报告
   - 性能监控数据

---

## 💡 发布建议

### 选择发布方式

1. **面向普通用户** → Chrome Web Store
2. **开源项目** → GitHub Releases + Chrome Web Store
3. **企业内部** → 直接分发或企业G Suite

### 版本策略

1. **先发布GitHub** → 收集反馈 → 再发布Chrome Web Store
2. **Beta版本测试** → 稳定后正式发布
3. **渐进式发布** → 先小范围，再全面推广

### 营销推广

1. **技术博客文章**
2. **社交媒体宣传**
3. **开发者社区分享**
4. **Senparc官方合作**

---

## 🎯 下一步行动

根据您的需求，我建议按以下优先级进行：

1. **立即可行：GitHub Releases发布**
   - 准备Git仓库
   - 创建Release
   - 编写安装文档

2. **中期目标：Chrome Web Store发布**
   - 注册开发者账号
   - 准备宣传资源
   - 提交审核

3. **长期维护：版本迭代优化**
   - 收集用户反馈
   - 持续功能改进
   - 扩展适用场景

您希望我先帮您进行哪种方式的发布准备？

