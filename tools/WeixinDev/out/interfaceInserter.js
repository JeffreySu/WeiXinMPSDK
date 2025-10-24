"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.WeixinInterfaceInserter = void 0;
const vscode = require("vscode");
class WeixinInterfaceInserter {
    constructor(_apiService) {
        this._apiService = _apiService;
    }
    /**
     * 显示输入对话框（右键菜单调用）
     */
    async showInputDialog() {
        const query = await vscode.window.showInputBox({
            prompt: '请输入你想调用的微信接口',
            placeHolder: '例如：发送模板消息、获取用户信息、创建小程序码等...',
            validateInput: (value) => {
                if (!value || value.trim().length === 0) {
                    return '请输入要查询的微信接口';
                }
                return undefined;
            }
        });
        if (query) {
            await this.processQuery(query.trim());
        }
    }
    /**
     * 处理查询并插入代码
     */
    async processQuery(query) {
        try {
            // 显示进度提示
            await vscode.window.withProgress({
                location: vscode.ProgressLocation.Notification,
                title: "正在查询微信接口...",
                cancellable: false
            }, async (progress) => {
                progress.report({ increment: 0, message: "连接到 Senparc.Weixin AI助手..." });
                // 调用API服务
                const result = await this._apiService.queryInterface(query);
                progress.report({ increment: 70, message: "解析接口信息..." });
                if (result.success && result.code) {
                    // 插入代码
                    await this.insertCodeToEditor(result.code, result.comments);
                    progress.report({ increment: 100, message: "完成！" });
                }
                else {
                    throw new Error(result.error || '查询失败');
                }
            });
            vscode.window.showInformationMessage('微信接口代码已成功插入！');
        }
        catch (error) {
            const errorMessage = error instanceof Error ? error.message : '未知错误';
            vscode.window.showErrorMessage(`插入失败：${errorMessage}`);
        }
    }
    /**
     * 插入代码到编辑器
     */
    async insertCodeToEditor(code, comments) {
        const editor = vscode.window.activeTextEditor;
        if (!editor) {
            // 如果没有活动编辑器，创建一个新文件
            const document = await vscode.workspace.openTextDocument({
                content: '',
                language: 'csharp'
            });
            await vscode.window.showTextDocument(document);
            return this.insertCodeToEditor(code, comments);
        }
        const position = editor.selection.active;
        const config = vscode.workspace.getConfiguration('weixindev');
        const autoInsertComments = config.get('autoInsertComments', true);
        let insertText = '';
        // 添加注释（如果有且配置允许）
        if (comments && autoInsertComments) {
            const commentLines = this.formatComments(comments);
            insertText += commentLines + '\n';
        }
        // 添加代码
        insertText += this.formatCode(code, editor, position);
        await editor.edit(editBuilder => {
            editBuilder.insert(position, insertText);
        });
        // 格式化插入的代码
        await this.formatInsertedCode(editor, position, insertText);
    }
    /**
     * 格式化注释
     */
    formatComments(comments) {
        const lines = comments.split('\n').filter(line => line.trim().length > 0);
        if (lines.length === 1) {
            return `// ${lines[0]}`;
        }
        else {
            const formattedLines = [
                '/**',
                ...lines.map(line => ` * ${line}`),
                ' */'
            ];
            return formattedLines.join('\n');
        }
    }
    /**
     * 格式化代码
     */
    formatCode(code, editor, position) {
        // 获取当前行的缩进
        const currentLine = editor.document.lineAt(position.line);
        const indent = this.getIndentation(currentLine.text);
        // 应用缩进到每一行
        const lines = code.split('\n');
        const indentedLines = lines.map((line, index) => {
            if (index === 0 && position.character > 0) {
                // 第一行，如果不是在行首，则不添加额外缩进
                return line;
            }
            return line.trim() ? indent + line : line;
        });
        return indentedLines.join('\n');
    }
    /**
     * 获取行的缩进
     */
    getIndentation(lineText) {
        const match = lineText.match(/^(\s*)/);
        return match ? match[1] : '';
    }
    /**
     * 格式化插入的代码
     */
    async formatInsertedCode(editor, insertPosition, insertedText) {
        try {
            // 计算插入内容的范围
            const lines = insertedText.split('\n');
            const endLine = insertPosition.line + lines.length - 1;
            const endCharacter = lines.length === 1
                ? insertPosition.character + lines[0].length
                : lines[lines.length - 1].length;
            const range = new vscode.Range(insertPosition, new vscode.Position(endLine, endCharacter));
            // 选中插入的内容
            editor.selection = new vscode.Selection(range.start, range.end);
            // 格式化选中的代码
            await vscode.commands.executeCommand('editor.action.formatSelection');
            // 将光标移动到插入内容的末尾
            const newPosition = new vscode.Position(endLine, endCharacter);
            editor.selection = new vscode.Selection(newPosition, newPosition);
        }
        catch (error) {
            // 格式化失败不影响主要功能
            console.warn('代码格式化失败:', error);
        }
    }
    /**
     * 插入代码片段（支持占位符）
     */
    async insertCodeSnippet(code, comments) {
        const editor = vscode.window.activeTextEditor;
        if (!editor) {
            vscode.window.showWarningMessage('请先打开一个C#文件');
            return;
        }
        // 创建代码片段
        let snippetText = '';
        if (comments) {
            snippetText += this.formatComments(comments) + '\n';
        }
        // 处理代码中的占位符
        const processedCode = this.processCodePlaceholders(code);
        snippetText += processedCode;
        const snippet = new vscode.SnippetString(snippetText);
        await editor.insertSnippet(snippet);
    }
    /**
     * 处理代码中的占位符
     */
    processCodePlaceholders(code) {
        // 替换常见的占位符
        let processed = code
            .replace(/appId/g, '${1:appId}')
            .replace(/openId/g, '${2:openId}')
            .replace(/accessToken/g, '${3:accessToken}')
            .replace(/"[^"]*模板ID[^"]*"/g, '"${4:templateId}"')
            .replace(/"[^"]*商品描述[^"]*"/g, '"${5:商品描述}"')
            .replace(/outTradeNo/g, '${6:outTradeNo}')
            .replace(/totalFee/g, '${7:totalFee}');
        return processed;
    }
}
exports.WeixinInterfaceInserter = WeixinInterfaceInserter;
//# sourceMappingURL=interfaceInserter.js.map