"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.activate = activate;
exports.deactivate = deactivate;
const vscode = require("vscode");
function activate(context) {
    console.log('WeixinDev Simple 扩展开始激活...');
    // 注册插入微信接口命令
    const insertCommand = vscode.commands.registerCommand('weixindev.insertWeixinInterface', async () => {
        console.log('执行插入微信接口命令...');
        try {
            // 显示输入框
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
            if (!query) {
                return;
            }
            // 显示进度
            await vscode.window.withProgress({
                location: vscode.ProgressLocation.Notification,
                title: "正在生成微信接口代码...",
                cancellable: false
            }, async (progress) => {
                progress.report({ increment: 0, message: "分析需求..." });
                // 模拟处理时间
                await new Promise(resolve => setTimeout(resolve, 1000));
                progress.report({ increment: 50, message: "生成代码..." });
                // 根据查询生成代码
                const code = generateCodeByQuery(query.trim());
                const comments = generateCommentsByQuery(query.trim());
                progress.report({ increment: 80, message: "插入代码..." });
                // 插入代码到编辑器
                await insertCodeToEditor(code, comments);
                progress.report({ increment: 100, message: "完成！" });
            });
            vscode.window.showInformationMessage('微信接口代码已成功插入！');
        }
        catch (error) {
            console.error('插入接口命令执行失败:', error);
            vscode.window.showErrorMessage(`插入接口失败: ${error}`);
        }
    });
    // 注册打开助手命令
    const openCommand = vscode.commands.registerCommand('weixindev.openSidebar', async () => {
        console.log('执行打开助手命令...');
        // 直接调用插入命令
        await vscode.commands.executeCommand('weixindev.insertWeixinInterface');
    });
    // 添加状态栏
    const statusBarItem = vscode.window.createStatusBarItem(vscode.StatusBarAlignment.Right, 100);
    statusBarItem.text = "$(code) 微信开发助手";
    statusBarItem.command = 'weixindev.insertWeixinInterface';
    statusBarItem.tooltip = '点击插入微信接口代码';
    statusBarItem.show();
    // 注册到context
    context.subscriptions.push(insertCommand, openCommand, statusBarItem);
    console.log('WeixinDev Simple 扩展激活成功');
    vscode.window.showInformationMessage('🚀 微信开发助手已激活！点击状态栏开始使用');
}
// 根据查询生成代码
function generateCodeByQuery(query) {
    const lowerQuery = query.toLowerCase();
    if (lowerQuery.includes('模板消息') || lowerQuery.includes('template')) {
        return `// 发送模板消息
var templateData = new {
    first = new TemplateDataItem("您好，您有新的消息"),
    content = new TemplateDataItem("这是消息内容"),
    remark = new TemplateDataItem("感谢您的使用")
};

var result = await Senparc.Weixin.MP.AdvancedAPIs.TemplateApi
    .SendTemplateMessageAsync(appId, openId, templateId, url, templateData);`;
    }
    if (lowerQuery.includes('用户信息') || lowerQuery.includes('用户基本') || lowerQuery.includes('userinfo')) {
        return `// 获取用户基本信息
var userInfo = await Senparc.Weixin.MP.AdvancedAPIs.UserApi
    .InfoAsync(accessToken, openId);
    
if (userInfo.errcode == ReturnCode.请求成功)
{
    var nickname = userInfo.nickname;
    var headimgurl = userInfo.headimgurl;
    // 处理用户信息
}`;
    }
    if (lowerQuery.includes('二维码') || lowerQuery.includes('小程序码') || lowerQuery.includes('qrcode')) {
        return `// 创建小程序码
var result = await Senparc.Weixin.WxOpen.AdvancedAPIs.WxAppApi
    .GetWxaCodeUnlimitAsync(accessToken, scene, page, width: 430);
    
if (result.errcode == ReturnCode.请求成功)
{
    // result.Result 包含二维码图片数据
    var imageBytes = result.Result;
}`;
    }
    if (lowerQuery.includes('支付') || lowerQuery.includes('下单') || lowerQuery.includes('pay')) {
        return `// 微信支付统一下单
var unifiedOrderRequest = new UnifiedOrderRequestData(
    appId, mchId, "商品描述", outTradeNo, totalFee, 
    spbillCreateIp, notifyUrl, tradeType: TenPayV3Type.JSAPI, openid: openId);

var result = await TenPayV3.UnifiedOrderAsync(unifiedOrderRequest);
if (result.IsReturnCodeSuccess())
{
    // 处理支付结果
}`;
    }
    if (lowerQuery.includes('企业微信') || lowerQuery.includes('企业') || lowerQuery.includes('work')) {
        return `// 企业微信发送消息
var data = new SendTextMessageData()
{
    touser = "用户ID",
    msgtype = "text",
    agentid = agentId,
    text = new SendTextMessageData_Text()
    {
        content = "消息内容"
    }
};

var result = await Senparc.Weixin.Work.AdvancedAPIs.MessageApi
    .SendMessageAsync(accessToken, data);`;
    }
    // 默认代码模板
    return `// ${query} 相关功能实现
// 请参考 Senparc.Weixin SDK 文档: https://sdk.weixin.senparc.com

// 示例代码模板（请根据具体需求修改）
var result = await SomeWeixinApi();
if (result.IsSuccess())
{
    // 处理成功结果
}`;
}
// 根据查询生成注释
function generateCommentsByQuery(query) {
    const lowerQuery = query.toLowerCase();
    if (lowerQuery.includes('模板消息')) {
        return '发送模板消息功能，需要先在微信公众平台配置模板消息';
    }
    if (lowerQuery.includes('用户信息')) {
        return '获取微信用户基本信息，需要用户已关注公众号';
    }
    if (lowerQuery.includes('二维码') || lowerQuery.includes('小程序码')) {
        return '生成小程序码，scene参数最多32个字符';
    }
    if (lowerQuery.includes('支付')) {
        return '微信支付统一下单接口，需要配置微信支付商户信息';
    }
    if (lowerQuery.includes('企业微信')) {
        return '企业微信发送消息，需要配置企业微信应用';
    }
    return `${query} 功能实现，请参考 Senparc.Weixin SDK 官方文档获取更多详细信息`;
}
// 插入代码到编辑器
async function insertCodeToEditor(code, comments) {
    const editor = vscode.window.activeTextEditor;
    if (!editor) {
        // 如果没有活动编辑器，创建一个新文件
        const document = await vscode.workspace.openTextDocument({
            content: '',
            language: 'csharp'
        });
        await vscode.window.showTextDocument(document);
        return insertCodeToEditor(code, comments);
    }
    const position = editor.selection.active;
    await editor.edit(editBuilder => {
        let insertText = '';
        // 添加注释
        if (comments) {
            insertText += `// ${comments}\n`;
        }
        // 添加代码
        insertText += code;
        editBuilder.insert(position, insertText);
    });
    // 简单格式化
    try {
        await vscode.commands.executeCommand('editor.action.formatSelection');
    }
    catch (error) {
        // 格式化失败不影响主要功能
        console.warn('代码格式化失败:', error);
    }
}
function deactivate() {
    console.log('WeixinDev Simple 扩展已停用');
}
//# sourceMappingURL=extension-simple.js.map