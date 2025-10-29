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
   # 进入插件项目目录
   cd tools/WeixinBrowserPlugin
   ```

3. **应用图标**（已准备）：
   - 16x16px - `src/icons/icon16.png`
   - 48x48px - `src/icons/icon48.png`
   - 128x128px - `src/icons/icon128.png`
   - PNG格式，背景透明

4. **准备宣传图片**：
   - 小宣传图：440x280px
   - 大宣传图：920x680px
   - 截屏图片：1280x800px 或 640x400px（建议3-5张）

#### 1.2 项目打包

1. **使用自动化构建脚本**
   ```bash
   # 构建发布版本
   npm run build
   
   # 或者使用原生Node构建
   node build.js
   ```

2. **生成的文件**
   - 输出目录：`dist/`
   - 发布包：`dist/senparc-weixin-ai-{version}.zip`
   - 当前版本：`senparc-weixin-ai-0.1.2.zip`

#### 1.3 上传到Chrome Web Store

1. **访问开发者控制台**
   - https://chrome.google.com/webstore/devconsole

2. **创建新项目**
   - 点击"Add new item"
   - 上传 `dist/senparc-weixin-ai-{version}.zip` 文件
   - 等待基础检查完成

3. **填写商店信息**

   **基本信息：**
   ```
   名称：WeChat Developer AI Assistant
   摘要：微信文档页面AI助手，提供智能接口查询功能
   类别：Developer Tools
   语言：中文（简体）+ English
   ```

   **详细描述：**
   ```
   🚀 专为微信开发者设计的Chrome浏览器插件

   ✨ 主要功能：
   • 智能检测微信官方文档页面（developers.weixin.qq.com, pay.weixin.qq.com等）
   • 一键启动AI助手，获取接口查询和代码示例
   • 上下文感知，自动传递当前页面信息
   • 现代化UI设计，悬浮窗和停靠模式两种界面
   • 拖拽功能，位置记忆

   🎯 适用场景：
   • 微信公众号开发
   • 微信小程序开发
   • 微信支付集成
   • 企业微信开发
   • Senparc.Weixin SDK使用

   📱 支持平台：
   • 桌面端Chrome浏览器
   • 基于Chromium的其他浏览器（Edge, Brave等）

   🔒 隐私保护：
   • 仅在微信官方文档页面运行
   • 不收集用户个人信息
   • 本地存储配置信息
   • 开源透明

   🛠️ 技术特性：
   • Manifest V3 兼容
   • 现代化JavaScript (ES6+)
   • 响应式设计
   • 性能优化
   ```

4. **设置权限说明**
   ```
   activeTab：检测当前页面是否为微信文档页面
   host_permissions：
   - https://developers.weixin.qq.com/* (微信开发文档)
   - https://developer.work.weixin.qq.com/* (企业微信文档)
   - https://pay.weixin.qq.com/* (微信支付文档)
   - https://sdk.weixin.senparc.com/* (Senparc.Weixin.AI服务)
   ```

5. **提交审核**
   - 完善所有必填信息
   - 点击"Submit for review"
   - 等待Google审核（通常1-3个工作日）

#### 1.4 审核通过后

1. **发布设置**
   - 选择发布到特定地区
   - 设置可见性（公开/私有/仅限组织）

2. **版本管理**
   - 后续更新通过上传新的ZIP包
   - 版本号需要递增（参见版本管理章节）

---

### 2. GitHub Releases 发布 🐙

**优势：**
- 开源透明
- 技术用户友好
- 版本历史清晰
- 免费

**发布步骤：**

#### 2.1 准备Git仓库

1. **项目结构**
   ```
   WeiXinMPSDK/
   └── tools/
       └── WeixinBrowserPlugin/
           ├── src/          # 源码文件
           ├── dist/         # 构建输出
           ├── package.json  # npm配置
           ├── build.js      # 构建脚本
           └── build.config.js # 构建配置
   ```

2. **标准发布流程**
   ```bash
   # 确保在正确目录
   cd tools/WeixinBrowserPlugin
   
   # 构建发布版本
   npm run build
   
   # 创建标签
   git tag -a v0.1.2 -m "Release version 0.1.2 - 修复语法错误"
   git push origin v0.1.2
   ```

#### 2.2 创建Release

1. **在GitHub创建Release**
   - 访问主仓库页面
   - 点击"Releases" → "Create a new release"
   - 选择标签：v0.1.2
   - 填写发布信息

2. **Release信息模板**
   ```markdown
   # WeChat Developer AI Assistant v0.1.2

   ## 🐛 Bug修复
   - ✅ 修复content.js中的语法错误问题
   - ✅ 完善调试系统配置
   - ✅ 优化条件调试语句格式

   ## 📦 安装方法

   ### 方法1：下载发布包
   1. 下载下方的 `senparc-weixin-ai-0.1.2.zip`
   2. 解压到本地文件夹
   3. 打开Chrome浏览器，访问 `chrome://extensions/`
   4. 开启"开发者模式"
   5. 点击"加载已解压的扩展程序"，选择解压后的文件夹

   ### 方法2：源码构建
   ```bash
   cd tools/WeixinBrowserPlugin
   npm run build
   # 使用 dist/ 目录中的文件进行安装
   ```

   ## 🔧 使用方法
   1. 访问任意微信官方文档页面
   2. 点击页面右侧的"Senparc.Weixin.AI"浮动按钮
   3. 在弹出的浮窗中使用AI助手功能
   4. 支持悬浮窗和停靠模式两种界面

   ## 📋 系统要求
   - Chrome 88+ 或基于Chromium的浏览器
   - 网络连接（用于访问AI服务）

   ## 🐛 问题反馈
   如果遇到问题，请在 [Issues](https://github.com/JeffreySu/WeiXinMPSDK/issues) 中反馈。
   ```

3. **上传发布文件**
   - 上传 `dist/senparc-weixin-ai-0.1.2.zip`
   - 可选：上传源码压缩包

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

1. **使用构建脚本创建安装包**
   ```bash
   # 构建发布版本
   npm run build
   
   # 创建完整的安装包
   mkdir enterprise-package
   cp -r dist/* enterprise-package/
   
   # 创建安装说明
   cat > enterprise-package/安装说明.txt << 'EOF'
   Senparc.Weixin.AI Assistant 安装指南
   
   安装步骤：
   1. 打开Chrome浏览器
   2. 地址栏输入：chrome://extensions/
   3. 开启右上角的"开发者模式"
   4. 点击"加载已解压的扩展程序"
   5. 选择此文件夹
   
   使用方法：
   1. 访问微信开发文档页面
   2. 点击右侧浮动的AI助手按钮
   3. 在弹窗中进行智能查询
   EOF
   ```

---

## 🔄 版本管理系统

### 版本号格式

本项目采用**语义化版本控制**（Semantic Versioning）：

```
主版本号.次版本号.修订号 (MAJOR.MINOR.PATCH)
```

- **主版本号**：重大不兼容更新、架构重构
- **次版本号**：新功能添加、向下兼容
- **修订号**：Bug修复、安全补丁

### 版本更新流程

#### 🚨 重要：更新版本号需要修改3个文件

每次发布新版本时，**必须**同时更新以下3个文件中的版本号：

1. **`src/manifest.json`** - Chrome扩展清单文件
   ```json
   {
     "version": "0.1.3"  // 递增版本号
   }
   ```

2. **`package.json`** - npm包配置文件
   ```json
   {
     "version": "0.1.3"  // 保持与manifest.json一致
   }
   ```

3. **`build.config.js`** - 构建配置文件
   ```javascript
   build: {
     version: '0.1.3'  // 保持与前两者一致
   }
   ```

#### 📋 版本更新检查清单

- [ ] 确认要发布的版本类型（MAJOR/MINOR/PATCH）
- [ ] 更新 `src/manifest.json` 中的版本号
- [ ] 更新 `package.json` 中的版本号
- [ ] 更新 `build.config.js` 中的版本号
- [ ] 确保所有三个文件的版本号完全一致
- [ ] 运行构建测试：`npm run build`
- [ ] 验证生成的ZIP文件名包含正确版本号
- [ ] 创建Git标签：`git tag v{版本号}`
- [ ] 推送标签：`git push origin v{版本号}`

#### 🔧 自动化版本更新脚本

为避免手动更新出错，可以创建版本更新脚本：

```bash
#!/bin/bash
# update-version.sh
# 使用方法: ./update-version.sh 0.1.3

if [ -z "$1" ]; then
    echo "请提供版本号，例如: ./update-version.sh 0.1.3"
    exit 1
fi

NEW_VERSION=$1

echo "🔄 更新版本号到 $NEW_VERSION ..."

# 更新 manifest.json
sed -i '' "s/\"version\": \".*\"/\"version\": \"$NEW_VERSION\"/" src/manifest.json

# 更新 package.json
sed -i '' "s/\"version\": \".*\"/\"version\": \"$NEW_VERSION\"/" package.json

# 更新 build.config.js
sed -i '' "s/version: '.*'/version: '$NEW_VERSION'/" build.config.js

echo "✅ 版本号更新完成"
echo "📋 请检查以下文件："
echo "   - src/manifest.json"
echo "   - package.json" 
echo "   - build.config.js"

# 构建测试
echo "🔨 运行构建测试..."
npm run build

if [ $? -eq 0 ]; then
    echo "✅ 构建成功！"
    echo "📦 发布包: dist/senparc-weixin-ai-$NEW_VERSION.zip"
    
    echo "🏷️ 创建Git标签..."
    git add .
    git commit -m "Release version $NEW_VERSION"
    git tag -a "v$NEW_VERSION" -m "Release version $NEW_VERSION"
    
    echo "✅ 版本发布准备完成！"
    echo "🚀 推送标签: git push origin v$NEW_VERSION"
else
    echo "❌ 构建失败，请检查错误信息"
    exit 1
fi
```

#### 📊 版本历史追踪

维护一个版本历史记录：

| 版本号 | 发布日期 | 更新类型 | 主要变更 |
|-------|----------|----------|----------|
| 0.1.0 | 2024-01-XX | 初始版本 | 首次发布基础功能 |
| 0.1.1 | 2024-01-XX | 补丁更新 | 优化性能和界面 |
| 0.1.2 | 2024-01-XX | 补丁更新 | 修复语法错误 |
| 0.1.3 | 待定 | 补丁更新 | 计划中的功能改进 |

### 发布类型示例

#### 🔧 补丁版本 (0.1.2 → 0.1.3)
- Bug修复
- 安全补丁
- 文档更新
- 性能优化

#### ✨ 次版本 (0.1.x → 0.2.0)
- 新功能添加
- UI/UX改进
- 新的API集成
- 向下兼容的改进

#### 🚀 主版本 (0.x.x → 1.0.0)
- 重大架构更改
- 不兼容的API变更
- 全新的用户界面
- 核心功能重构

---

## 📋 发布前检查清单

### 代码质量检查 ✅

- [ ] 所有功能正常工作
- [ ] 语法错误检查完成：`npm run build`
- [ ] 错误处理完善
- [ ] 性能优化完成
- [ ] 安全性检查通过
- [ ] 兼容性测试完成（Chrome 88+）

### 版本管理检查 ✅

- [ ] 版本号在3个文件中保持一致
- [ ] 版本号符合语义化版本规范
- [ ] Git标签已创建：`git tag v{版本号}`
- [ ] 构建生成的ZIP文件名正确

### 文档准备 ✅

- [ ] `README.md` 完整
- [ ] `INSTALL.md` 清晰
- [ ] `USAGE.md` 详细
- [ ] `TROUBLESHOOTING.md` 全面
- [ ] `PUBLISH.md` 更新（本文件）

### 发布资源 ✅

- [ ] 图标文件齐全（16x16, 48x48, 128x128）
- [ ] 宣传截图拍摄完成
- [ ] 描述文案撰写完成
- [ ] 商店列表信息准备完成

### 测试验证 ✅

- [ ] 在多个Chrome版本测试
- [ ] 在不同操作系统测试（Windows, macOS, Linux）
- [ ] 功能完整性测试
- [ ] 支持的微信文档页面测试
- [ ] 性能压力测试

---

## 🔄 持续集成建议

### GitHub Actions 自动化

创建 `.github/workflows/release.yml`：

```yaml
name: Release Build

on:
  push:
    tags:
      - 'v*'

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup Node.js
      uses: actions/setup-node@v3
      with:
        node-version: '18'
        
    - name: Install dependencies
      run: |
        cd tools/WeixinBrowserPlugin
        npm install
        
    - name: Build extension
      run: |
        cd tools/WeixinBrowserPlugin
        npm run build
        
    - name: Create Release
      uses: softprops/action-gh-release@v1
      with:
        files: tools/WeixinBrowserPlugin/dist/*.zip
        generate_release_notes: true
```

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

根据当前项目状态，建议按以下优先级进行：

1. **立即可行：版本0.1.3发布准备**
   - 确定下个版本的功能改进
   - 使用版本更新脚本
   - 准备发布说明

2. **中期目标：Chrome Web Store发布**
   - 注册开发者账号
   - 准备宣传资源
   - 提交审核

3. **长期维护：版本迭代优化**
   - 收集用户反馈
   - 持续功能改进
   - 扩展适用场景

您希望我先帮您进行哪种方式的发布准备？
