# 故障排除指南

## 🚨 问题：在微信文档页面看不到绿色Logo按钮

### 第一步：确认插件安装状态

1. **检查插件是否已安装并启用**
   - 打开 `chrome://extensions/`
   - 确认"Senparc.Weixin.AI Assistant"显示为"已启用"
   - 如果显示错误，点击"重新加载"按钮

2. **检查插件权限**
   - 在扩展管理页面点击插件的"详细信息"
   - 确认"网站访问权限"包含 `*.weixin.qq.com`

### 第二步：确认页面URL正确

确保你访问的是正确的微信文档页面：
- ✅ `https://developers.weixin.qq.com/doc/`
- ✅ `https://developers.weixin.qq.com/miniprogram/dev/`
- ✅ `https://work.weixin.qq.com/api/doc/`
- ❌ `https://weixin.qq.com/` (这不是文档页面)

### 第三步：使用浏览器开发者工具调试

1. **打开开发者工具**
   - 按 `F12` 或右键选择"检查"
   - 切换到 "Console" 标签

2. **查看控制台日志**
   刷新页面后，应该看到类似以下日志：
   ```
   Senparc.Weixin.AI 插件开始初始化...
   当前页面URL: https://developers.weixin.qq.com/doc/
   当前域名: developers.weixin.qq.com
   ✅ 检测到微信文档页面，初始化AI助手...
   🎨 开始创建Logo按钮...
   ✅ Logo按钮已添加到页面
   ```

3. **如果没有看到日志**
   - 插件可能没有正确加载
   - 检查 "Sources" 标签中是否有 `content.js` 文件
   - 查看 "Network" 标签是否有加载错误

### 第四步：手动调试

在控制台中运行以下代码进行调试：

```javascript
// 检查插件状态
console.log('当前域名:', window.location.hostname);
console.log('是否微信域名:', window.location.hostname.endsWith('weixin.qq.com'));
console.log('WeixinAIAssistant:', typeof window.WeixinAIAssistant);

// 查找Logo按钮
const button = document.getElementById('senparc-weixin-ai-button');
console.log('Logo按钮:', button);

// 手动创建测试按钮
const testBtn = document.createElement('div');
testBtn.style.cssText = 'position:fixed;top:20px;left:20px;z-index:9999;background:red;color:white;padding:10px;';
testBtn.textContent = '测试按钮';
document.body.appendChild(testBtn);
```

### 第五步：常见问题解决方案

#### 问题1：插件权限不足
**症状**：控制台显示权限错误
**解决**：
1. 打开 `chrome://extensions/`
2. 找到插件，点击"详细信息"
3. 确保"在所有网站上"或"在特定网站上"权限已启用

#### 问题2：Content Script未加载
**症状**：控制台没有任何插件相关日志
**解决**：
1. 检查 `manifest.json` 中的 `matches` 配置
2. 确认当前页面URL匹配规则
3. 尝试重新加载插件

#### 问题3：CSS样式冲突
**症状**：按钮创建了但不可见
**解决**：
```javascript
// 检查按钮样式
const button = document.getElementById('senparc-weixin-ai-button');
if (button) {
  console.log('按钮样式:', window.getComputedStyle(button));
  // 强制显示按钮
  button.style.cssText = 'position:fixed!important;top:20px!important;left:20px!important;z-index:99999!important;background:green!important;color:white!important;padding:10px!important;display:block!important;';
}
```

#### 问题4：页面加载时机问题
**症状**：页面加载完成前插件就执行了
**解决**：
```javascript
// 手动重新初始化
if (window.WeixinAIAssistant) {
  new window.WeixinAIAssistant();
}
```

### 第六步：重新安装插件

如果以上步骤都无法解决问题：

1. **完全卸载插件**
   - 在 `chrome://extensions/` 中点击"移除"
   - 清除浏览器缓存

2. **重新安装**
   - 重新加载插件文件夹
   - 确认所有文件都存在且完整

3. **测试基本功能**
   - 访问 `https://developers.weixin.qq.com/doc/`
   - 按 F12 查看控制台日志

### 第七步：联系支持

如果问题仍然存在，请提供以下信息：

1. **浏览器信息**
   - Chrome版本号
   - 操作系统版本

2. **错误日志**
   - 控制台完整错误信息
   - Network标签中的请求失败信息

3. **插件状态**
   - 扩展管理页面截图
   - 插件详细信息截图

## 🔧 高级调试

### 使用调试脚本

将 `debug.js` 文件内容复制到控制台运行，获取详细的调试信息。

### 检查Manifest配置

确认 `manifest.json` 配置正确：
```json
{
  "content_scripts": [
    {
      "matches": ["https://*.weixin.qq.com/*"],
      "js": ["content.js"],
      "css": ["styles.css"],
      "run_at": "document_end"
    }
  ]
}
```

### 验证文件完整性

确认以下文件存在且内容完整：
- `manifest.json`
- `content.js`
- `styles.css`
- `popup.html`
- `popup.js`
- `icon.svg`

## 📞 获取帮助

- **GitHub Issues**: 报告bug和功能请求
- **开发者工具**: 使用Chrome DevTools进行深度调试
- **社区支持**: 在相关技术论坛寻求帮助
