# 安装指南

## 快速安装

### 1. 开发者模式安装（推荐）

1. **下载插件文件**
   - 下载整个 `WeixinBrowserPlugin` 文件夹到本地

2. **打开Chrome扩展管理**
   - 在Chrome地址栏输入：`chrome://extensions/`
   - 或者：菜单 → 更多工具 → 扩展程序

3. **启用开发者模式**
   - 点击右上角的"开发者模式"开关

4. **加载插件**
   - 点击"加载已解压的扩展程序"
   - 选择 `WeixinBrowserPlugin` 文件夹
   - 点击"选择文件夹"

5. **确认安装**
   - 插件列表中出现"Senparc.Weixin.AI Assistant"
   - 状态显示为"已启用"

### 2. 验证安装

1. **访问微信文档页面**
   ```
   https://developers.weixin.qq.com/doc/
   ```

2. **查看Logo按钮**
   - 页面左上角应该出现绿色的"Senparc.Weixin.AI"按钮

3. **测试功能**
   - 点击Logo按钮
   - 应该弹出AI助手浮窗
   - 浮窗中加载 Senparc AI 助手界面

## 故障排除

### 插件未显示
- 确认文件夹结构正确
- 检查 `manifest.json` 文件是否存在
- 刷新扩展程序页面

### Logo按钮不出现
- 确认当前页面是 `weixin.qq.com` 域名
- 按F12打开开发者工具，查看Console错误
- 尝试刷新页面

### 浮窗无法加载
- 检查网络连接
- 确认 `https://sdk.weixin.senparc.com` 可访问
- 检查浏览器是否阻止弹窗

## 卸载插件

1. 打开 `chrome://extensions/`
2. 找到"Senparc.Weixin.AI Assistant"
3. 点击"移除"按钮
4. 确认删除

## 更新插件

1. 下载新版本文件
2. 在扩展程序页面点击插件的"重新加载"按钮
3. 或者先移除旧版本，再重新安装新版本

## 权限说明

插件请求的权限：
- **activeTab**: 访问当前活动标签页，用于检测微信文档页面
- **storage**: 本地存储插件设置（暂未使用）
- **host_permissions**: 
  - `https://weixin.qq.com/*` - 在微信文档页面运行
  - `https://sdk.weixin.senparc.com/*` - 加载AI助手服务

这些权限都是必要的，插件不会收集任何个人信息。
