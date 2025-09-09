# 使用指南

## 🚀 快速开始

### 1. 安装插件

按照 [INSTALL.md](INSTALL.md) 中的说明安装插件到Chrome浏览器。

### 2. 访问微信文档

打开任意微信官方文档页面，例如：
- https://developers.weixin.qq.com/doc/
- https://developers.weixin.qq.com/miniprogram/dev/
- https://work.weixin.qq.com/api/doc

### 3. 使用AI助手

1. **查看Logo按钮**：页面左上角会自动出现绿色的"Senparc.Weixin.AI"按钮
2. **打开AI助手**：点击按钮打开AI助手浮窗
3. **智能查询**：AI助手会自动获取当前页面信息，提供相关的接口查询和代码示例

## 🎯 功能详解

### 自动检测功能

插件会自动检测当前页面是否为微信官方文档：
- ✅ `developers.weixin.qq.com` - 微信公众号/小程序文档
- ✅ `work.weixin.qq.com` - 企业微信文档  
- ✅ `pay.weixin.qq.com` - 微信支付文档
- ✅ 其他 `*.weixin.qq.com` 子域名

### Logo按钮

- **位置**：固定在页面左上角
- **样式**：微信绿色渐变背景
- **交互**：支持hover效果和点击动画
- **响应式**：在移动设备上自动调整大小

### AI助手浮窗

- **尺寸**：桌面端 1200x800px，移动端全屏
- **内容**：嵌入 Senparc.Weixin.AI 助手页面
- **参数传递**：自动将当前页面URL作为query参数
- **关闭方式**：
  - 点击右上角关闭按钮
  - 按ESC键
  - 点击浮窗外部区域

## 🔧 高级功能

### 键盘快捷键

- `ESC` - 关闭AI助手浮窗

### 浏览器兼容性

- ✅ Chrome 88+
- ✅ Edge 88+
- ✅ Opera 74+
- ✅ 其他基于Chromium的浏览器

### 响应式设计

插件支持多种屏幕尺寸：
- **桌面端** (>768px)：完整功能和界面
- **平板端** (768px-480px)：优化的中等尺寸界面
- **移动端** (<480px)：全屏浮窗，简化按钮

## 🎨 界面定制

### 主题适配

插件自动适配系统主题：
- **浅色主题**：白色背景，深色文字
- **深色主题**：深色背景，浅色文字
- **高对比度**：增强边框和颜色对比

### 动画效果

- **按钮动画**：hover时上浮和缩放效果
- **浮窗动画**：淡入淡出和缩放过渡
- **加载动画**：旋转加载指示器

## 🔍 故障排除

### 常见问题

#### 1. Logo按钮不显示

**可能原因**：
- 当前页面不是微信文档域名
- 插件未正确安装或启用
- 页面加载未完成

**解决方法**：
```bash
# 检查当前URL是否包含 weixin.qq.com
console.log(window.location.hostname);

# 检查插件是否加载
console.log(window.WeixinAIAssistant);
```

#### 2. 浮窗无法打开

**可能原因**：
- 网络连接问题
- 浏览器阻止弹窗
- CSP策略限制

**解决方法**：
1. 检查网络连接
2. 允许弹窗权限
3. 查看浏览器控制台错误信息

#### 3. AI助手加载失败

**可能原因**：
- Senparc服务器不可访问
- 网络防火墙阻止
- iframe安全策略限制

**解决方法**：
1. 直接访问 https://sdk.weixin.senparc.com/AiDoc 测试
2. 检查防火墙设置
3. 尝试刷新页面

### 调试模式

开启浏览器开发者工具进行调试：

```javascript
// 检查插件状态
console.log('插件版本:', chrome.runtime.getManifest().version);

// 检查当前页面检测结果
console.log('是否微信文档页面:', window.location.hostname.endsWith('weixin.qq.com'));

// 手动触发插件初始化
new WeixinAIAssistant();
```

## 📊 性能优化

### 加载性能

- **懒加载**：仅在微信文档页面加载功能代码
- **资源压缩**：CSS和JS文件经过优化
- **缓存策略**：合理利用浏览器缓存

### 运行时性能

- **事件委托**：减少DOM事件监听器
- **防抖处理**：避免频繁的DOM操作
- **内存管理**：及时清理不需要的对象

### 网络优化

- **CDN加速**：静态资源使用CDN分发
- **HTTP/2**：支持多路复用和服务器推送
- **压缩传输**：启用gzip/brotli压缩

## 🔒 隐私和安全

### 数据收集

插件**不会收集**以下信息：
- 个人身份信息
- 浏览历史记录
- 表单输入内容
- Cookie或本地存储数据

### 权限使用

插件请求的权限及用途：
- `activeTab`：检测当前页面是否为微信文档
- `storage`：存储插件设置（可选）
- `host_permissions`：在指定域名下运行

### 安全措施

- **HTTPS通信**：所有网络请求使用加密传输
- **沙箱隔离**：iframe采用安全沙箱模式
- **权限最小化**：仅请求必要的浏览器权限

## 🆕 更新和维护

### 自动更新

插件支持自动更新机制：
1. Chrome会定期检查插件更新
2. 发现新版本时自动下载安装
3. 用户可在扩展管理页面手动检查更新

### 版本历史

查看 [README.md](README.md) 中的更新日志了解版本变化。

### 反馈渠道

- **GitHub Issues**：报告bug和功能请求
- **Senparc社区**：技术讨论和经验分享
- **邮件联系**：紧急问题和商务合作

## 🤝 社区支持

### 开发者资源

- **Senparc.Weixin SDK**：https://github.com/JeffreySu/WeiXinMPSDK
- **官方文档**：https://sdk.weixin.senparc.com/
- **视频教程**：https://study.163.com/course/courseMain.htm?courseId=1004873017

### 技术交流

- **QQ群**：300313885
- **微信群**：扫描官网二维码加入
- **技术博客**：https://www.cnblogs.com/szw/

---

**让微信开发更简单，让AI助手更智能！** 🎉
