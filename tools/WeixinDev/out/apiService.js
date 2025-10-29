"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.WeixinApiService = void 0;
const vscode = require("vscode");
const axios_1 = require("axios");
const cheerio = require("cheerio");
class WeixinApiService {
    constructor() {
        this.defaultApiUrl = 'https://sdk.weixin.senparc.com/AiDoc';
    }
    getApiUrl() {
        const config = vscode.workspace.getConfiguration('weixindev');
        return config.get('apiUrl', this.defaultApiUrl);
    }
    /**
     * 查询微信接口并解析代码
     * @param query 查询内容
     * @returns 解析结果
     */
    async queryInterface(query) {
        try {
            const apiUrl = this.getApiUrl();
            const requestUrl = `${apiUrl}?request=${encodeURIComponent(query)}`;
            // 发送HTTP请求
            const response = await axios_1.default.get(requestUrl, {
                timeout: 30000,
                headers: {
                    'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36',
                    'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8',
                    'Accept-Language': 'zh-CN,zh;q=0.9,en;q=0.8'
                }
            });
            if (response.status !== 200) {
                throw new Error(`HTTP错误: ${response.status}`);
            }
            // 解析HTML内容
            const result = this.parseApiResponse(response.data);
            if (!result.success) {
                // 如果直接请求失败，尝试等待页面加载完成后再次解析
                await this.delay(3000);
                return await this.queryInterfaceWithRetry(query);
            }
            return result;
        }
        catch (error) {
            console.error('API查询失败:', error);
            return {
                success: false,
                error: error instanceof Error ? error.message : '网络请求失败'
            };
        }
    }
    /**
     * 重试查询（用于处理动态内容）
     */
    async queryInterfaceWithRetry(query) {
        try {
            // 这里可以实现更复杂的重试逻辑或使用Puppeteer等工具
            // 目前简化为直接返回一个示例结果
            return this.generateFallbackResult(query);
        }
        catch (error) {
            return {
                success: false,
                error: '重试查询失败'
            };
        }
    }
    /**
     * 解析API响应HTML
     */
    parseApiResponse(html) {
        try {
            const $ = cheerio.load(html);
            // 查找C#代码块
            const codeElement = $('code.language-csharp').first();
            const code = codeElement.text().trim();
            if (!code) {
                return {
                    success: false,
                    error: '未找到代码内容，请确保查询内容准确'
                };
            }
            // 查找提示信息
            let comments = '';
            const tipsElement = $('.tips-section').first();
            if (tipsElement.length > 0) {
                comments = tipsElement.text().trim();
                // 清理注释文本
                comments = this.cleanCommentsText(comments);
            }
            return {
                success: true,
                code: code,
                comments: comments
            };
        }
        catch (error) {
            return {
                success: false,
                error: '解析API响应失败'
            };
        }
    }
    /**
     * 清理注释文本
     */
    cleanCommentsText(text) {
        return text
            .replace(/\s+/g, ' ')
            .replace(/^\s+|\s+$/g, '')
            .split('\n')
            .map(line => line.trim())
            .filter(line => line.length > 0)
            .join('\n');
    }
    /**
     * 生成备用结果（当API无法正常解析时）
     */
    generateFallbackResult(query) {
        // 根据查询内容生成基础的代码模板
        const templates = this.getCodeTemplates();
        for (const template of templates) {
            if (this.matchesQuery(query, template.keywords)) {
                return {
                    success: true,
                    code: template.code,
                    comments: template.comments
                };
            }
        }
        // 如果没有匹配的模板，返回通用模板
        return {
            success: true,
            code: `// TODO: 实现 "${query}" 的相关功能\n// 请参考 Senparc.Weixin SDK 文档: https://sdk.weixin.senparc.com\n\n// 示例代码模板\nvar result = await SomeWeixinApi();`,
            comments: `针对 "${query}" 的功能实现，请参考官方文档获取更多详细信息。`
        };
    }
    /**
     * 检查查询是否匹配关键词
     */
    matchesQuery(query, keywords) {
        const lowerQuery = query.toLowerCase();
        return keywords.some(keyword => lowerQuery.includes(keyword.toLowerCase()));
    }
    /**
     * 获取代码模板
     */
    getCodeTemplates() {
        return [
            {
                keywords: ['模板消息', '模板', '发送消息', 'template'],
                code: `// 发送模板消息
var templateData = new {
    first = new TemplateDataItem("您好，您有新的消息"),
    content = new TemplateDataItem("这是消息内容"),
    remark = new TemplateDataItem("感谢您的使用")
};

var result = await Senparc.Weixin.MP.AdvancedAPIs.TemplateApi
    .SendTemplateMessageAsync(appId, openId, templateId, url, templateData);`,
                comments: '发送模板消息功能，需要先在微信公众平台配置模板消息'
            },
            {
                keywords: ['用户信息', '获取用户', '用户基本信息', 'userinfo'],
                code: `// 获取用户基本信息
var userInfo = await Senparc.Weixin.MP.AdvancedAPIs.UserApi
    .InfoAsync(accessToken, openId);
    
if (userInfo.errcode == ReturnCode.请求成功)
{
    var nickname = userInfo.nickname;
    var headimgurl = userInfo.headimgurl;
    // 处理用户信息
}`,
                comments: '获取微信用户基本信息，需要用户已关注公众号'
            },
            {
                keywords: ['二维码', '小程序码', 'qrcode', '小程序'],
                code: `// 创建小程序码
var result = await Senparc.Weixin.WxOpen.AdvancedAPIs.WxAppApi
    .GetWxaCodeUnlimitAsync(accessToken, scene, page, width: 430);
    
if (result.errcode == ReturnCode.请求成功)
{
    // result.Result 包含二维码图片数据
    var imageBytes = result.Result;
}`,
                comments: '生成小程序码，scene参数最多32个字符'
            },
            {
                keywords: ['微信支付', '支付', '下单', 'pay'],
                code: `// 微信支付统一下单
var unifiedOrderRequest = new UnifiedOrderRequestData(
    appId, mchId, "商品描述", outTradeNo, totalFee, 
    spbillCreateIp, notifyUrl, tradeType: TenPayV3Type.JSAPI, openid: openId);

var result = await TenPayV3.UnifiedOrderAsync(unifiedOrderRequest);
if (result.IsReturnCodeSuccess())
{
    // 处理支付结果
}`,
                comments: '微信支付统一下单接口，需要配置微信支付商户信息'
            },
            {
                keywords: ['企业微信', '企业', 'work', '发送消息'],
                code: `// 企业微信发送消息
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
    .SendMessageAsync(accessToken, data);`,
                comments: '企业微信发送文本消息，需要配置企业微信应用'
            }
        ];
    }
    /**
     * 延迟函数
     */
    delay(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
}
exports.WeixinApiService = WeixinApiService;
//# sourceMappingURL=apiService.js.map