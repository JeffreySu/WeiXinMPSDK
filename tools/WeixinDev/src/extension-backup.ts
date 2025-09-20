import * as vscode from 'vscode';
import { WeixinDevSidebarProvider } from './sidebarProvider';
import { WeixinInterfaceInserter } from './interfaceInserter';
import { WeixinApiService } from './apiService';

export function activate(context: vscode.ExtensionContext) {
    console.log('WeixinDev 扩展开始激活...');

    try {
        // 初始化服务
        const apiService = new WeixinApiService();
        const interfaceInserter = new WeixinInterfaceInserter(apiService);
        
        // 创建侧边栏提供者
        const sidebarProvider = new WeixinDevSidebarProvider(context.extensionUri, apiService, interfaceInserter);
        
        // 注册命令：插入微信接口（优先注册）
        const insertInterfaceCommand = vscode.commands.registerCommand('weixindev.insertWeixinInterface', async () => {
            console.log('执行插入微信接口命令...');
            try {
                await interfaceInserter.showInputDialog();
            } catch (error) {
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
            } catch (error) {
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
        statusBarItem.command = 'weixindev.insertWeixinInterface';  // 直接调用插入命令
        statusBarItem.tooltip = '点击插入微信接口';
        statusBarItem.show();

        // 添加到订阅
        context.subscriptions.push(
            insertInterfaceCommand,
            openSidebarCommand,
            refreshCommand,
            sidebarRegistration,
            statusBarItem
        );

        console.log('WeixinDev 扩展激活完成');
        
        // 显示激活消息
        vscode.window.showInformationMessage('🚀 WeixinDev 微信开发助手已准备就绪！点击状态栏图标开始使用');

    } catch (error) {
        console.error('WeixinDev 扩展激活失败:', error);
        vscode.window.showErrorMessage(`WeixinDev 激活失败: ${error}`);
    }
}

export function deactivate() {
    console.log('WeixinDev 扩展已停用');
}
