# URL变化检测与iframe自动重新加载功能增强

## 📋 概述

本次更新为微信浏览器插件（Senparc.Weixin.AI）添加了智能URL变化检测功能，解决了用户反馈的核心问题：**当浮窗被关闭并再次打开时，如果页面URL已变化，iframe应该自动重新加载新URL对应的内容**。

## 🎯 解决的问题

### 原始问题
- 用户在微信文档页面A打开AI助手浮窗
- 关闭浮窗后，用户导航到微信文档页面B  
- 再次打开浮窗时，iframe仍然显示页面A的内容
- 用户需要手动刷新才能获得页面B的相关帮助

### 解决方案
- ✅ 自动检测URL变化
- ✅ 智能判断是否需要重新加载
- ✅ 无缝的用户体验
- ✅ 性能优化的防抖机制

## 🚀 新增功能

### 1. URL变化检测机制
```javascript
// 检查URL是否已变化
hasUrlChanged() {
  const currentUrl = window.location.href;
  const urlChanged = currentUrl !== this.lastUrl;
  return urlChanged;
}
```

**特性：**
- 精确比较页面URL
- 支持SPA应用的URL变化
- 详细的调试日志

### 2. 智能iframe重新加载
```javascript
// 重新加载iframe内容
reloadIframeContent() {
  // 构造新的iframe URL
  const currentUrl = encodeURIComponent(window.location.href);
  const newIframeUrl = `https://sdk.weixin.senparc.com/AiDoc?query=${currentUrl}`;
  
  // 检查是否真的需要重新加载
  if (this.lastIframeUrl === newIframeUrl) {
    return; // 无需重新加载
  }
  
  // 更新iframe的src并显示加载状态
  iframe.src = newIframeUrl;
}
```

**特性：**
- 只在必要时重新加载，避免不必要的网络请求
- 优雅的加载指示器
- 错误处理和重试机制
- 自动尺寸重新计算

### 3. 防抖优化机制
```javascript
// 带防抖的URL变化检查
debouncedUrlChangeCheck(callback, delay = 300) {
  if (this.urlCheckDebounceTimeout) {
    clearTimeout(this.urlCheckDebounceTimeout);
  }
  
  this.urlCheckDebounceTimeout = setTimeout(() => {
    if (this.hasUrlChanged()) {
      callback();
    }
  }, delay);
}
```

**特性：**
- 避免频繁的URL检查
- 提升性能和用户体验
- 可配置的防抖延迟

### 4. 增强的状态管理
- **关闭时记录状态**：在浮窗关闭前记录当前URL
- **打开时智能检查**：检查URL是否变化，决定是否重新加载
- **双重URL记录**：分别记录页面URL和iframe URL

## 🔧 技术实现细节

### 关键变量添加
```javascript
constructor() {
  // ... 现有代码 ...
  this.lastUrl = window.location.href; // 记录上次的URL
  this.lastIframeUrl = null; // 记录上次iframe的URL  
  this.urlCheckDebounceTimeout = null; // URL检查防抖计时器
}
```

### 核心流程改进
1. **浮窗打开时**：
   ```javascript
   openFloatingWindow() {
     // 检查URL是否已变化
     const urlChanged = this.hasUrlChanged();
     
     if (this.floatingWindow && urlChanged) {
       // 重新加载iframe内容
       this.reloadIframeContent();
       this.updateLastUrl();
     }
     // ... 继续原有逻辑
   }
   ```

2. **浮窗关闭时**：
   ```javascript
   closeFloatingWindow() {
     // 记录当前URL状态（为下次打开做准备）
     this.updateLastUrl();
     // ... 继续原有逻辑
   }
   ```

3. **新浮窗创建时**：
   ```javascript
   // 记录iframe URL和页面URL
   this.lastIframeUrl = iframeUrl;
   this.updateLastUrl();
   ```

## 🧪 测试与验证

### 测试文件
- `test-url-change-detection.js` - 自动化测试脚本
- `demo-url-change-feature.html` - 功能演示页面

### 测试用例
1. **URL变化检测测试**
   - 验证`hasUrlChanged()`方法正确性
   - 测试URL更新后的状态变化

2. **iframe重新加载测试**
   - 验证URL变化时iframe自动重新加载
   - 测试加载指示器和错误处理

3. **浮窗重新打开测试**
   - 模拟完整的用户操作流程
   - 验证URL同步的准确性

### 手动测试步骤
1. 在微信文档页面A打开AI助手
2. 关闭浮窗
3. 导航到微信文档页面B
4. 重新打开浮窗
5. 验证iframe内容是否为页面B的相关内容

## 📊 性能优化

### 防抖机制
- 默认300ms防抖延迟
- 避免频繁的URL检查
- 降低CPU使用率

### 智能重新加载
- 只在URL真正变化时重新加载
- 避免不必要的网络请求
- 保持良好的用户体验

### 内存管理
- 及时清理防抖计时器
- 在插件销毁时释放所有资源

## 🔍 调试支持

### 详细日志
```javascript
// 启用调试模式查看详细日志
window.__SENPARC_DEBUG__.enabled = true;
```

**日志类型：**
- URL变化检测日志
- iframe重新加载状态
- 防抖机制执行情况
- 错误和异常信息

### 开发者工具
- `window.runUrlChangeTests()` - 运行自动化测试
- `window.testResults` - 查看测试结果
- 控制台中的详细调试信息

## 🌟 用户体验改进

### 无缝切换
- 用户无需手动刷新iframe
- 自动同步页面内容
- 保持AI助手的相关性

### 视觉反馈
- 加载指示器显示重新加载状态
- 自定义提示信息："检测到页面变化，正在重新加载..."
- 错误状态的友好提示

### 性能表现
- 最小化不必要的操作
- 流畅的动画和过渡效果
- 优化的资源使用

## 📝 代码质量

### 向后兼容
- 保持所有现有功能不变
- 新功能作为增强而非替换
- 优雅的错误处理

### 可维护性
- 清晰的方法命名和注释
- 模块化的功能实现
- 完整的错误处理

### 可扩展性
- 防抖延迟可配置
- 支持自定义URL检查逻辑
- 易于添加新的检测机制

## 🚀 部署说明

### 文件变更
- `src/content.js` - 主要功能实现
- 新增测试文件和演示页面
- 无需修改manifest.json或其他配置

### 兼容性
- 支持所有Chrome扩展API v3
- 兼容现有的微信文档页面
- 无需额外的权限申请

## 🎉 总结

这次功能增强显著提升了用户体验，解决了用户反馈的核心痛点。通过智能URL检测和自动iframe重新加载，用户现在可以享受到：

- ✅ **自动化**：无需手动操作即可获得最相关的AI帮助
- ✅ **智能化**：系统自动判断何时需要重新加载内容  
- ✅ **性能优化**：使用防抖和智能检查减少不必要的操作
- ✅ **无缝体验**：用户感受不到技术复杂性，只享受便利

这项增强使得Senparc.Weixin.AI插件在用户体验和技术实现上都达到了新的高度。
