#if NET8_0_OR_GREATER

using ModelContextProtocol.Server;
using Senparc.CO2NET.Extensions;
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
        public WeChatMcpResult<string[]> SearchWeChatApiCatalogInPlatform(
            [Description("平台名称，只能从以下名称中选择，不能出现任何偏差，且大小写敏感：WeChat_OfficialAccount, WeChat_MiniProgram, WeChat_Open, WeChat_Work")]
            string platformName)
        {

            var items = Senparc.CO2NET.WebApi.FindApiService.ApiItemList;
            Console.WriteLine($"ApiItems 数量：{items.Count}");
            var apiItems = items
                                .Where(z => z.Category == platformName)
                                .Select(z => z.FullMethodName.Substring(0, z.FullMethodName.LastIndexOf(".")))
                                .Distinct()
                                .ToArray();


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
        public WeChatMcpResult<string[]> SearchWeChatApiListInPlatform(
           [Description("API 目录名称，必须精准匹配 SearchWeChatApiCatalogInPlatform 返回信息中的 Result 中的某一条，不允许做任何修改")]
            string apiCatalogName)
        {
            var apiItems = Senparc.CO2NET.WebApi.FindApiService.ApiItemList
                                            .Where(z => z.FullMethodName.StartsWith(apiCatalogName))
                                            .ToArray();
            //TODO: 添加每个 API 模块的说明

            var result = new WeChatMcpResult<string[]>()
            {
                NextRoundTip = "根据 Result 中的方法注释，选择一条最适合的方法名称，并尝试生成调用代码",
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