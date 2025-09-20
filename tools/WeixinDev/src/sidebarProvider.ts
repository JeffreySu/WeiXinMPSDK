import * as vscode from 'vscode';
import { WeixinApiService } from './apiService';
import { WeixinInterfaceInserter } from './interfaceInserter';

export class WeixinDevSidebarProvider implements vscode.WebviewViewProvider {
    public static readonly viewType = 'weixindev.sidebar';
    private _view?: vscode.WebviewView;

    constructor(
        private readonly _extensionUri: vscode.Uri,
        private readonly _apiService: WeixinApiService,
        private readonly _interfaceInserter: WeixinInterfaceInserter
    ) {}

    public resolveWebviewView(
        webviewView: vscode.WebviewView,
        context: vscode.WebviewViewResolveContext,
        _token: vscode.CancellationToken,
    ) {
        this._view = webviewView;

        webviewView.webview.options = {
            enableScripts: true,
            localResourceRoots: [this._extensionUri]
        };

        webviewView.webview.html = this._getHtmlForWebview(webviewView.webview);

        // 处理来自webview的消息
        webviewView.webview.onDidReceiveMessage(async (data) => {
            switch (data.type) {
                case 'queryInterface':
                    await this._handleQueryInterface(data.query);
                    break;
                case 'openInBrowser':
                    const url = `${this._apiService.getApiUrl()}?request=${encodeURIComponent(data.query)}`;
                    vscode.env.openExternal(vscode.Uri.parse(url));
                    break;
                case 'showError':
                    vscode.window.showErrorMessage(data.message);
                    break;
                case 'showInfo':
                    vscode.window.showInformationMessage(data.message);
                    break;
            }
        });
    }

    public refresh() {
        if (this._view) {
            this._view.webview.html = this._getHtmlForWebview(this._view.webview);
        }
    }

    private async _handleQueryInterface(query: string) {
        try {
            vscode.window.showInformationMessage(`正在查询：${query}`);
            
            // 显示进度
            await vscode.window.withProgress({
                location: vscode.ProgressLocation.Notification,
                title: "微信接口查询中...",
                cancellable: false
            }, async (progress) => {
                progress.report({ increment: 0, message: "连接到 Senparc.Weixin AI助手..." });
                
                // 调用API服务获取接口信息
                const result = await this._apiService.queryInterface(query);
                
                progress.report({ increment: 50, message: "解析接口信息..." });
                
                if (result.success && result.code) {
                    // 插入代码到编辑器
                    await this._interfaceInserter.insertCodeToEditor(result.code, result.comments);
                    progress.report({ increment: 100, message: "完成！" });
                    
                    vscode.window.showInformationMessage('微信接口代码已成功插入！');
                } else {
                    throw new Error(result.error || '无法获取接口信息');
                }
            });
            
        } catch (error) {
            const errorMessage = error instanceof Error ? error.message : '未知错误';
            vscode.window.showErrorMessage(`查询失败：${errorMessage}`);
        }
    }

    private _getHtmlForWebview(webview: vscode.Webview): string {
        // 获取配置
        const config = vscode.workspace.getConfiguration('weixindev');
        const apiUrl = config.get('apiUrl', 'https://sdk.weixin.senparc.com/AiDoc');

        return `<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>微信开发助手</title>
    <style>
        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: var(--vscode-editor-background);
            color: var(--vscode-editor-foreground);
        }
        
        .container {
            max-width: 100%;
        }
        
        .header {
            text-align: center;
            margin-bottom: 20px;
        }
        
        .logo {
            font-size: 24px;
            font-weight: bold;
            color: var(--vscode-textLink-foreground);
            margin-bottom: 10px;
        }
        
        .subtitle {
            font-size: 14px;
            color: var(--vscode-descriptionForeground);
            margin-bottom: 20px;
        }
        
        .input-section {
            margin-bottom: 20px;
        }
        
        label {
            display: block;
            margin-bottom: 8px;
            font-weight: 500;
            color: var(--vscode-editor-foreground);
        }
        
        .input-wrapper {
            position: relative;
            margin-bottom: 15px;
        }
        
        textarea {
            width: 100%;
            min-height: 80px;
            padding: 12px;
            border: 1px solid var(--vscode-input-border);
            border-radius: 4px;
            background-color: var(--vscode-input-background);
            color: var(--vscode-input-foreground);
            font-size: 14px;
            resize: vertical;
            box-sizing: border-box;
        }
        
        textarea:focus {
            outline: 1px solid var(--vscode-focusBorder);
            border-color: var(--vscode-focusBorder);
        }
        
        .button-group {
            display: flex;
            gap: 10px;
            margin-top: 15px;
        }
        
        button {
            flex: 1;
            padding: 10px 16px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
            font-weight: 500;
            transition: background-color 0.2s;
        }
        
        .btn-primary {
            background-color: var(--vscode-button-background);
            color: var(--vscode-button-foreground);
        }
        
        .btn-primary:hover {
            background-color: var(--vscode-button-hoverBackground);
        }
        
        .btn-secondary {
            background-color: var(--vscode-button-secondaryBackground);
            color: var(--vscode-button-secondaryForeground);
        }
        
        .btn-secondary:hover {
            background-color: var(--vscode-button-secondaryHoverBackground);
        }
        
        .examples {
            margin-top: 20px;
            padding: 15px;
            background-color: var(--vscode-textBlockQuote-background);
            border-left: 4px solid var(--vscode-textBlockQuote-border);
            border-radius: 4px;
        }
        
        .examples h4 {
            margin: 0 0 10px 0;
            color: var(--vscode-editor-foreground);
            font-size: 14px;
        }
        
        .example-item {
            margin: 8px 0;
            padding: 8px;
            background-color: var(--vscode-editor-background);
            border-radius: 3px;
            cursor: pointer;
            font-size: 13px;
            transition: background-color 0.2s;
        }
        
        .example-item:hover {
            background-color: var(--vscode-list-hoverBackground);
        }
        
        .status {
            margin-top: 15px;
            padding: 10px;
            border-radius: 4px;
            font-size: 13px;
            display: none;
        }
        
        .status.info {
            background-color: var(--vscode-inputValidation-infoBackground);
            border: 1px solid var(--vscode-inputValidation-infoBorder);
            color: var(--vscode-inputValidation-infoForeground);
        }
        
        .status.error {
            background-color: var(--vscode-inputValidation-errorBackground);
            border: 1px solid var(--vscode-inputValidation-errorBorder);
            color: var(--vscode-inputValidation-errorForeground);
        }
        
        .loading {
            display: none;
            text-align: center;
            margin: 15px 0;
            color: var(--vscode-descriptionForeground);
        }
        
        .spinner {
            display: inline-block;
            width: 16px;
            height: 16px;
            border: 2px solid var(--vscode-progressBar-background);
            border-radius: 50%;
            border-top-color: var(--vscode-button-background);
            animation: spin 1s ease-in-out infinite;
            margin-right: 8px;
        }
        
        @keyframes spin {
            to { transform: rotate(360deg); }
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="header">
            <div class="logo">🚀 微信开发助手</div>
            <div class="subtitle">Senparc.Weixin SDK AI 智能代码生成</div>
        </div>
        
        <div class="input-section">
            <label for="queryInput">输入你想调用的微信接口：</label>
            <div class="input-wrapper">
                <textarea 
                    id="queryInput" 
                    placeholder="例如：发送模板消息、获取用户信息、创建小程序码等..."
                    rows="3"
                ></textarea>
            </div>
            
            <div class="button-group">
                <button id="queryBtn" class="btn-primary">🎯 生成代码</button>
                <button id="browserBtn" class="btn-secondary">🌐 在浏览器中打开</button>
            </div>
            
            <div id="loading" class="loading">
                <span class="spinner"></span>正在处理请求...
            </div>
            
            <div id="status" class="status"></div>
        </div>
        
        <div class="examples">
            <h4>💡 示例查询：</h4>
            <div class="example-item" data-query="发送模板消息给用户">📧 发送模板消息给用户</div>
            <div class="example-item" data-query="获取微信用户基本信息">👤 获取微信用户基本信息</div>
            <div class="example-item" data-query="创建小程序二维码">📱 创建小程序二维码</div>
            <div class="example-item" data-query="微信支付统一下单">💳 微信支付统一下单</div>
            <div class="example-item" data-query="企业微信发送消息">🏢 企业微信发送消息</div>
            <div class="example-item" data-query="获取公众号菜单">📋 获取公众号菜单</div>
        </div>
    </div>
    
    <script>
        const vscode = acquireVsCodeApi();
        const queryInput = document.getElementById('queryInput');
        const queryBtn = document.getElementById('queryBtn');
        const browserBtn = document.getElementById('browserBtn');
        const loading = document.getElementById('loading');
        const status = document.getElementById('status');
        
        // 查询按钮点击事件
        queryBtn.addEventListener('click', () => {
            const query = queryInput.value.trim();
            if (!query) {
                showStatus('请输入要查询的微信接口', 'error');
                return;
            }
            
            showLoading(true);
            hideStatus();
            
            vscode.postMessage({
                type: 'queryInterface',
                query: query
            });
        });
        
        // 浏览器打开按钮点击事件
        browserBtn.addEventListener('click', () => {
            const query = queryInput.value.trim();
            if (!query) {
                showStatus('请输入要查询的微信接口', 'error');
                return;
            }
            
            vscode.postMessage({
                type: 'openInBrowser',
                query: query
            });
        });
        
        // 回车键快捷查询
        queryInput.addEventListener('keydown', (e) => {
            if (e.key === 'Enter' && (e.ctrlKey || e.metaKey)) {
                e.preventDefault();
                queryBtn.click();
            }
        });
        
        // 示例点击事件
        document.querySelectorAll('.example-item').forEach(item => {
            item.addEventListener('click', () => {
                queryInput.value = item.getAttribute('data-query');
                queryInput.focus();
            });
        });
        
        // 显示/隐藏加载状态
        function showLoading(show) {
            loading.style.display = show ? 'block' : 'none';
            queryBtn.disabled = show;
            browserBtn.disabled = show;
        }
        
        // 显示状态消息
        function showStatus(message, type = 'info') {
            status.textContent = message;
            status.className = \`status \${type}\`;
            status.style.display = 'block';
        }
        
        // 隐藏状态消息
        function hideStatus() {
            status.style.display = 'none';
        }
        
        // 监听来自扩展的消息
        window.addEventListener('message', event => {
            const message = event.data;
            
            switch (message.type) {
                case 'queryComplete':
                    showLoading(false);
                    if (message.success) {
                        showStatus('代码已成功插入到编辑器！', 'info');
                    } else {
                        showStatus(message.error || '查询失败', 'error');
                    }
                    break;
                case 'queryError':
                    showLoading(false);
                    showStatus(message.error || '查询出错', 'error');
                    break;
            }
        });
        
        // 页面加载完成后焦点到输入框
        queryInput.focus();
    </script>
</body>
</html>`;
    }
}
