#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：WeChatMcpRouter.cs
    文件功能描述：微信 McpRouter
    
    
    创建标识：Senparc - 20250814
  
    修改标识：Senparc - 20250820
    修改描述：1.5.0.1-preview.3 优化 ApiItemList 的缓存

----------------------------------------------------------------*/


#if NET8_0_OR_GREATER

using ModelContextProtocol.Server;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using Senparc.CO2NET.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.AspNet.MCP
{
    [Description("WeChat(Weixin) MCP Toolkit")]
    [McpServerToolType]
    public class WeChatMcpRouter
    {
        IBaseObjectCacheStrategy _cache;
        public WeChatMcpRouter(IBaseObjectCacheStrategy cache)
        {
            _cache = cache;
        }

        private async Task<List<ApiItem>> GetApiItems()
        {
            var cacheKey = "WeixinSdkApiItems";
            var existKey = await _cache.CheckExistedAsync(cacheKey);
            if (!existKey)
            {
                await _cache.SetAsync(cacheKey, Senparc.CO2NET.WebApi.FindApiService.ApiItemList);
            }

            return await _cache.GetAsync<List<ApiItem>>(cacheKey);
        }

        [Description("WeChat McpRouter entry point. Get all WeChat platform names")]
        [McpServerTool()]
        public WeChatMcpResult<List<PlatformDescription>> GetPlatformNames()
        {
            var result = new WeChatMcpResult<List<PlatformDescription>>()
            {
                NextRoundTip = "请根据用户需求，从返回的 Result 中选择最合适的 PlatformName，调用 Tool：WeChatMcpRouter.SearchWeChatApiCatalogInPlatform，获取最合适的 API 目录",

                Result = new List<PlatformDescription>() {
                        new PlatformDescription(NeuChar.PlatformType.WeChat_OfficialAccount.ToString(),"微信公众号"),
                        new PlatformDescription(NeuChar.PlatformType.WeChat_MiniProgram.ToString(),"小程序"),
                        new PlatformDescription(NeuChar.PlatformType.WeChat_Open.ToString(),"开放平台"),
                        new PlatformDescription(NeuChar.PlatformType.WeChat_Work.ToString(),"企业微信"),

                        //new PlatformDescription(PlatformType.TenPayV3.ToString(),"微信支付")
                        }
            };

            return result;
        }

        [Description("Search WeChat API Catalog in the specified platform")]
        [McpServerTool]
        public async Task<WeChatMcpResult<string[]>> SearchWeChatApiCatalogInPlatform(
            [Description("平台名称，只能从以下名称中选择，不能出现任何偏差，且大小写敏感：WeChat_OfficialAccount, WeChat_MiniProgram, WeChat_Open, WeChat_Work")]
            string platformName)
        {

            var items = await GetApiItems();
            
            var apiItems = items
                                .Where(z => z.Category == platformName)
                                .Select(z => z.FullMethodName.Substring(0, z.FullMethodName.LastIndexOf(".")))
                                .Distinct()
                                .ToArray();

            SenparcTrace.SendCustomLog("WeixinMcpRouter-SearchWeChatApiCatalogInPlatform", $"platformName:{platformName} , result:{apiItems.ToJson(true)}");

            //TODO: 添加每个 API 模块的说明

            var result = new WeChatMcpResult<string[]>()
            {
                NextRoundTip = "请根据用户需求，选择最合适的 API 目录，调用 Tool：WeChatMcpRouter.SearchWeChatApiListInPlatform，获取最合适的 API 列表",
                Result = apiItems
            };
            return result;
        }

        [Description("Search WeChat API List in the specified platform")]
        [McpServerTool]
        public async Task<WeChatMcpResult<string[]>> SearchWeChatApiListInPlatform(
           [Description("API 目录名称，必须精准匹配 SearchWeChatApiCatalogInPlatform 返回信息中的 Result 中的某一条，不允许做任何修改")]
            string apiCatalogName)
        {
            var items = await GetApiItems();

            var apiItems = items.Where(z => z.FullMethodName.StartsWith(apiCatalogName))
                                 .ToArray();

            SenparcTrace.SendCustomLog("WeixinMcpRouter-SearchWeChatApiListInPlatform", $"apiCatalogName:{apiCatalogName} , result:{apiItems.ToJson(true)}");


            //TODO: 添加每个 API 模块的说明

            var result = new WeChatMcpResult<string[]>()
            {
                NextRoundTip = "根据 Result 中的方法注释，选择一条最适合的方法名称，并尝试生成调用代码。除非：当前接口传入参数需要从其他微信接口获取，请继续使用同一个平台名称请求 SearchWeChatApiCatalogInPlatform，获取对应的参数结果，并传递给当前方法。",
                Result = apiItems.Select(z => new
                {
                    z.FullMethodName,
                    z.ParamsPart,
                    z.IsAsync,
                    z.Summary,
                    z.Category
                }.ToJson()).ToArray()
            };
            return result;
        }
    }
}
#endif
