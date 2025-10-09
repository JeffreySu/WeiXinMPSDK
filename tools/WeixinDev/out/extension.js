"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.activate = activate;
exports.deactivate = deactivate;
const vscode = require("vscode");
const axios_1 = require("axios");
function activate(context) {
    console.log('=== WeixinDev 开始激活 ===');
    // 插入微信接口命令
    const disposable = vscode.commands.registerCommand('weixindev.insertWeixinInterface', async () => {
        console.log('=== 插入微信接口命令被调用 ===');
        try {
            // 检查是否有活动编辑器
            const editor = vscode.window.activeTextEditor;
            if (!editor) {
                vscode.window.showErrorMessage('请先打开一个文件');
                return;
            }
            // 显示输入弹窗
            const userInput = await vscode.window.showInputBox({
                prompt: '请描述您需要的微信接口功能',
                placeHolder: '例如：获取用户信息、发送模板消息、创建菜单等...',
                validateInput: (value) => {
                    if (!value || value.trim().length === 0) {
                        return '请输入您的需求描述';
                    }
                    if (value.trim().length < 3) {
                        return '需求描述至少需要3个字符';
                    }
                    return null;
                }
            });
            // 用户取消输入
            if (!userInput) {
                return;
            }
            // 显示加载提示
            await vscode.window.withProgress({
                location: vscode.ProgressLocation.Notification,
                title: '正在生成微信接口代码...',
                cancellable: false
            }, async (progress) => {
                try {
                    // 获取配置的API URL
                    const config = vscode.workspace.getConfiguration('weixindev');
                    const apiUrl = config.get('apiUrl', 'https://sdk.weixin.senparc.com/AiDoc');
                    progress.report({ increment: 30, message: '正在请求AI服务...' });
                    // 调用API
                    const response = await axios_1.default.get(apiUrl, {
                        params: {
                            query: userInput.trim()
                        },
                        timeout: 30000, // 30秒超时
                        headers: {
                            'User-Agent': 'WeixinDev-VSCode-Extension/1.0.0'
                        }
                    });
                    progress.report({ increment: 60, message: '正在处理返回结果...' });
                    // 检查响应
                    if (response.status !== 200) {
                        throw new Error(`API请求失败: ${response.status} ${response.statusText}`);
                    }
                    let generatedCode = '';
                    // 处理不同类型的响应
                    if (typeof response.data === 'string') {
                        generatedCode = response.data;
                    }
                    else if (response.data && typeof response.data === 'object') {
                        // 如果返回的是JSON对象，尝试提取代码字段
                        generatedCode = response.data.code || response.data.result || response.data.data || JSON.stringify(response.data, null, 2);
                    }
                    else {
                        throw new Error('API返回了无效的数据格式');
                    }
                    // 清理和格式化代码
                    generatedCode = generatedCode.trim();
                    if (!generatedCode) {
                        throw new Error('API返回了空的代码内容');
                    }
                    progress.report({ increment: 90, message: '正在插入代码...' });
                    // 插入代码到光标位置
                    const position = editor.selection.active;
                    const success = await editor.edit(editBuilder => {
                        // 添加注释说明
                        const comment = `// 微信接口代码 - 需求: ${userInput}\n// 生成时间: ${new Date().toLocaleString()}\n`;
                        const codeToInsert = comment + generatedCode + '\n';
                        editBuilder.insert(position, codeToInsert);
                    });
                    if (success) {
                        progress.report({ increment: 100, message: '代码插入完成！' });
                        vscode.window.showInformationMessage('✅ 微信接口代码已成功插入！');
                    }
                    else {
                        throw new Error('代码插入失败');
                    }
                }
                catch (error) {
                    console.error('插入微信接口时发生错误:', error);
                    let errorMessage = '生成微信接口代码时发生错误';
                    if (error instanceof Error) {
                        if (error.message.includes('timeout')) {
                            errorMessage = '请求超时，请检查网络连接后重试';
                        }
                        else if (error.message.includes('Network Error')) {
                            errorMessage = '网络连接失败，请检查网络设置';
                        }
                        else {
                            errorMessage = `错误: ${error.message}`;
                        }
                    }
                    vscode.window.showErrorMessage(errorMessage);
                    // 在调试模式下显示详细错误信息
                    const debugMode = vscode.workspace.getConfiguration('weixindev').get('enableDebugMode', false);
                    if (debugMode) {
                        console.error('详细错误信息:', error);
                        vscode.window.showErrorMessage(`调试信息: ${JSON.stringify(error, null, 2)}`);
                    }
                }
            });
        }
        catch (error) {
            console.error('命令执行失败:', error);
            vscode.window.showErrorMessage('命令执行失败，请重试');
        }
    });
    // 注册到context
    context.subscriptions.push(disposable);
    console.log('=== WeixinDev Minimal 激活完成 ===');
    vscode.window.showInformationMessage('🎉 微信开发助手已激活！');
}
function deactivate() {
    console.log('=== WeixinDev Minimal 停用 ===');
}
//# sourceMappingURL=extension.js.map