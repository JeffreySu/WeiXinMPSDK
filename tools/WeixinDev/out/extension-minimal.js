"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.activate = activate;
exports.deactivate = deactivate;
const vscode = require("vscode");
function activate(context) {
    console.log('=== WeixinDev Minimal 开始激活 ===');
    // 最简单的命令注册
    const disposable = vscode.commands.registerCommand('weixindev.insertWeixinInterface', () => {
        console.log('=== 命令被调用 ===');
        vscode.window.showInformationMessage('WeixinDev 命令执行成功！');
        // 简单的代码插入
        const editor = vscode.window.activeTextEditor;
        if (editor) {
            const position = editor.selection.active;
            editor.edit(editBuilder => {
                editBuilder.insert(position, '// 微信开发助手测试代码\nvar result = "Hello WeixinDev";');
            });
        }
        else {
            vscode.window.showInformationMessage('请先打开一个文件');
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
//# sourceMappingURL=extension-minimal.js.map