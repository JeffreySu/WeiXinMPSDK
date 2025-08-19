/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    AiDocController.cs
    文件功能描述：AI 文档
    
    
    创建标识：Senparc - 20250818

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Helpers;
using Senparc.NeuChar.Agents;
using Senparc.Weixin.MP;
using Senparc.Weixin.Tencent;
using Senparc.CO2NET.Trace;
using Senparc.CO2NET.Cache;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ModelContextProtocol.Client;
using Senparc.AI.Kernel;
using Senparc.AI.Entities;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Senparc.Weixin.Sample.Net8.Controllers
{
    /// <summary>
    /// AI 文档
    /// </summary>
    public class AiDocController : BaseController
    {
        IServiceProvider _serviceProvider;

        public AiDocController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// AI文档助手页面
        /// </summary>
        /// <param name="query">初始查询内容</param>
        /// <returns></returns>
        public ActionResult Index(string query = null)
        {
            ViewData["InitialQuery"] = query;
            return View();
        }

        /// <summary>
        /// 处理来自首页的POST请求
        /// </summary>
        /// <param name="query">查询内容</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(string query)
        {
            ViewData["InitialQuery"] = query;
            return View();
        }

        /// <summary>
        /// 处理AI接口请求
        /// </summary>
        /// <param name="query">用户查询内容</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ProcessQuery([FromBody] QueryRequest request)
        {
            try
            {

                //建立 MCP 连接，并获取信息
                var mcpEndpoint = "https://www.ncf.pub/mcp-senparc-xncf-weixinmanager/sse";
                var clientTransport = new SseClientTransport(new SseClientTransportOptions()
                {
                    Endpoint = new Uri(mcpEndpoint),
                    Name = "NCF-Server"
                });

                var client = await McpClientFactory.CreateAsync(clientTransport);
                var tools = await client.ListToolsAsync();

                var aiSetting = Senparc.AI.Config.SenparcAiSetting;
                var semanticAiHandler = new SemanticAiHandler(aiSetting);

                var parameter = new PromptConfigParameter()
                {
                    MaxTokens = 2000,
                    Temperature = 0.7,
                    TopP = 0.5,
                };

                var iWantToRun = semanticAiHandler.ChatConfig(parameter,
                  userId: "Jeffrey",
                  maxHistoryStore: 10,
                  chatSystemMessage: "你是一位智能助手，帮我选择最适合的 API 方案。",
                  senparcAiSetting: aiSetting,
                  kernelBuilderAction: kh =>
                  {
                      // kh.Plugins.AddMcpFunctionsFromSseServerAsync("NCF-Server", "http://localhost:5000/sse/sse");
#pragma warning disable SKEXP0001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
                      kh.Plugins.AddFromFunctions("WeixinMpRouter", tools.Select(z => z.AsKernelFunction()));
#pragma warning restore SKEXP0001 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
                  }
                      );
                var executionSettings2 = new OpenAIPromptExecutionSettings
                {
                    Temperature = 0,
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()// FunctionChoiceBehavior.Auto()
                };
                var ka = new KernelArguments(executionSettings2) { };

                ////输出结果
                //SenparcAiResult ret = await semanticAiHandler.ChatAsync(iWantToRun, request.RequestPrompt/*, streamItemProceessing*/);

                //////////var resultRaw = await iWantToRun.Kernel.InvokePromptAsync(request.RequestPrompt, ka);

                var prompt = request.Query;
                var resultRaw = await iWantToRun.Kernel.InvokePromptAsync(prompt, ka);


                // 模拟HTML格式的回复内容
                var htmlResponse = GenerateResponse(prompt, resultRaw.ToString());

                return Json(new
                {
                    success = true,
                    data = htmlResponse
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "处理请求时发生错误：" + ex.Message
                });
            }
        }

        /// <summary>
        /// 生成模拟的HTML响应
        /// </summary>
        /// <param name="query">用户查询</param>
        /// <returns></returns>
        private string GenerateResponse(string query,string result)
        {
            var html = $@"
<div class='ai-response'>
    <div class='response-header'>
        <h3>🤖 AI 助手回复</h3>
        <p class='query-info'>您的查询：<span class='user-query'>{query}</span></p>
    </div>
    
    <div class='response-content'>
        <div class='module-section'>
            <h4>📦 接口模块</h4>
            <div class='module-info'>
                <span class='module-tag'>微信公众号 API</span>
                <span class='module-tag'>用户管理</span>
            </div>
        </div>
        
        <div class='description-section'>
            <h4>📝 接口说明</h4>
            <p>根据您的需求，推荐使用<strong>获取用户基本信息接口</strong>。该接口可以获取用户的昵称、头像、性别、所在城市、语言和关注时间等信息。</p>
            <ul>
                <li>适用于已关注公众号的用户</li>
                <li>需要用户的OpenID作为参数</li>
                <li>返回用户详细信息的JSON数据</li>
            </ul>
        </div>
        
        <div class='code-section'>
            <h4>💻 代码示例</h4>
            <div class='code-tabs'>
                <div class='tab-buttons'>
                    <button class='tab-btn active' data-tab='csharp'>C#</button>
                    <button class='tab-btn' data-tab='api'>API调用</button>
                </div>
                
                <div class='tab-content'>
                    <div class='tab-pane active' id='csharp'>
                        <pre><code class='language-csharp'>// 获取用户基本信息
var appId = ""your_app_id"";
var openId = ""user_open_id"";

// 方法一：使用 Senparc.Weixin SDK
var userInfo = await UserApi.InfoAsync(appId, openId);
Console.WriteLine($""用户昵称：{{userInfo.nickname}}"");
Console.WriteLine($""用户头像：{{userInfo.headimgurl}}"");

// 方法二：直接调用API
var accessToken = await AccessTokenContainer.GetAccessTokenAsync(appId);
var apiUrl = $""https://api.weixin.qq.com/cgi-bin/user/info?access_token={{accessToken}}&openid={{openId}}"";
var result = await HttpHelper.GetAsync(apiUrl);</code></pre>
                    </div>
                    
                    <div class='tab-pane' id='api'>
                        <pre><code class='language-http'>GET https://api.weixin.qq.com/cgi-bin/user/info
?access_token=ACCESS_TOKEN
&openid=OPENID
&lang=zh_CN

# 响应示例
{{
    ""subscribe"": 1,
    ""openid"": ""oLVPpjqs2BhqzwPj5A-vTYAX3GLM"",
    ""nickname"": ""微信用户"",
    ""sex"": 1,
    ""language"": ""zh_CN"",
    ""city"": ""深圳"",
    ""province"": ""广东"",
    ""country"": ""中国"",
    ""headimgurl"": ""http://wx.qlogo.cn/mmopen/..."",
    ""subscribe_time"": 1672531200
}}</code></pre>
                    </div>
                </div>
            </div>
        </div>
        
        <div class='tips-section'>
            <h4>💡 使用提示</h4>
            <div class='tip-item'>
                <strong>注意事项：</strong>
                <ul>
                    <li>确保用户已关注公众号，否则无法获取详细信息</li>
                    <li>AccessToken需要定期刷新，建议使用SDK自动管理</li>
                    <li>接口调用频率限制：100万次/天</li>
                </ul>
            </div>
        </div>
    </div>
</div>";

            return html;
        }

        /// <summary>
        /// 查询请求模型
        /// </summary>
        public class QueryRequest
        {
            public string Query { get; set; }
        }
    }
}
