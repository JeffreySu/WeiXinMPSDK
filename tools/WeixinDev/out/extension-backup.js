"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.activate = activate;
exports.deactivate = deactivate;
const vscode = require("vscode");
const sidebarProvider_1 = require("./sidebarProvider");
const interfaceInserter_1 = require("./interfaceInserter");
const apiService_1 = require("./apiService");
function activate(context) {
    console.log('WeixinDev 扩展开始激活...');
    try {
        // 初始化服务
        const apiService = new apiService_1.WeixinApiService();
        const interfaceInserter = new interfaceInserter_1.WeixinInterfaceInserter(apiService);
        // 创建侧边栏提供者
        const sidebarProvider = new sidebarProvider_1.WeixinDevSidebarProvider(context.extensionUri, apiService, interfaceInserter);
        // 注册命令：插入微信接口（优先注册）
        const insertInterfaceCommand = vscode.commands.registerCommand('weixindev.insertWeixinInterface', async () => {
            console.log('执行插入微信接口命令...');
            try {
                await interfaceInserter.showInputDialog();
            }
            catch (error) {
                console.error('插入接口命令执行失败:', error);
                vscode.window.showErrorMessage(`插入接口失败: ${error}`);
            }
        });
        // 注册命令：打开侧边栏
        const openSidebarCommand = vscode.commands.registerCommand('weixindev.openSidebar', () => {
            console.log('执行打开侧边栏命令...');
            try {
                // 尝试多种方式打开侧边栏
                vscode.commands.executeCommand('workbench.view.extension.weixindev').then(() => {
                    console.log('侧边栏打开成功');
                }, (error) => {
                    console.log('尝试备用方式打开侧边栏');
                    vscode.commands.executeCommand('weixindev.insertWeixinInterface');
                });
            }
            catch (error) {
                console.error('打开侧边栏失败:', error);
                // 备用方案：直接调用插入接口
                vscode.commands.executeCommand('weixindev.insertWeixinInterface');
            }
        });
        // 注册命令：刷新侧边栏
        const refreshCommand = vscode.commands.registerCommand('weixindev.refreshWebview', () => {
            console.log('刷新侧边栏...');
            sidebarProvider.refresh();
        });
        // 注册侧边栏（在命令注册之后）
        const sidebarRegistration = vscode.window.registerWebviewViewProvider('weixindev.sidebar', sidebarProvider);
        // 注册状态栏
        const statusBarItem = vscode.window.createStatusBarItem(vscode.StatusBarAlignment.Right, 100);
        statusBarItem.text = "$(code) 微信开发助手";
        statusBarItem.command = 'weixindev.insertWeixinInterface'; // 直接调用插入命令
        statusBarItem.tooltip = '点击插入微信接口';
        statusBarItem.show();
        // 添加到订阅
        context.subscriptions.push(insertInterfaceCommand, openSidebarCommand, refreshCommand, sidebarRegistration, statusBarItem);
        console.log('WeixinDev 扩展激活完成');
        // 显示激活消息
        vscode.window.showInformationMessage('🚀 WeixinDev 微信开发助手已准备就绪！点击状态栏图标开始使用');
    }
    catch (error) {
        console.error('WeixinDev 扩展激活失败:', error);
        vscode.window.showErrorMessage(`WeixinDev 激活失败: ${error}`);
    }
}
function deactivate() {
    console.log('WeixinDev 扩展已停用');
}
//# sourceMappingURL=extension-backup.js.map