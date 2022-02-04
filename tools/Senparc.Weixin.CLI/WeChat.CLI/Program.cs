using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.RegisterServices;
using Senparc.NeuChar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.CLI
{
    [Command(Name = "weixin", Description = "微信开发辅助工具")]
    [HelpOption("-h|--help")]
    class Program
    {
        private string _hostUrl = "https://www.ncf.pub";
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _serviceCollection;

        static Task<int> Main(string[] args)
        {
            return CommandLineApplication.ExecuteAsync<Program>(args);
        }

        static void WriteColorLine(string str, ConsoleColor color, bool writeLine = true)
        {
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            if (writeLine)
            {
                Console.WriteLine(str);
            }
            else
            {
                Console.Write(str);
            }
            Console.ForegroundColor = currentForeColor;
        }

        private string GetFirstLineOfSummary(string summary)
        {
            return summary.Trim().Split('\n')[0];
        }

        public Program()
        {
            var configBuilder = new ConfigurationBuilder();
            var senparcSetting = new SenparcSetting();
            var config = configBuilder.Build();
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSenparcGlobalServices(config);//Senparc.CO2NET 全局注册
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            IRegisterService register = RegisterService.Start(senparcSetting)
                                                        .UseSenparcGlobal();
        }

        [Option(CommandOptionType.NoValue, Description = "获取学习资源", ShortName = "r", LongName = "Resource")]
        private bool? ShowResource { get; }

        [Option(CommandOptionType.NoValue, Description = "QQ群（动态更新）", ShortName = "q", LongName = "QQGroup")]
        private bool? QqGroup { get; }

        [Option(CommandOptionType.SingleValue, Description = "查询：接口关键字", ShortName = "k", LongName = "keyword")]
        private string? Keyword { get; }
        [Option(CommandOptionType.MultipleValue, Description = "查询：平台模块。公众号：1，小程序：2，企业号：4，微信开放平台：8", ShortName = "p", LongName = "platform")]
        private string?[] Platform { get; }
        [Option(CommandOptionType.SingleValue, Description = "查询：是否限定为异步方法", ShortName = "a", LongName = "async")]
        private bool? Async { get; }
        [Option(CommandOptionType.SingleValue, Description = "查询：接口主机，默认为：https://www.ncf.pub", ShortName = "u", LongName = "HostUrl")]
        private string? HostUrl { get; }


        private async Task<int> OnExecuteAsync(CommandLineApplication app, CancellationToken cancellationToken = default)
        {
            if (ShowResource == true)
            {
                Console.WriteLine(@"1、开源项目：https://github.com/JeffreySu/WeiXinMPSDK

2、微信技术交流社区：https://weixin.senparc.com/QA

3、chm帮助文档下载：https://sdk.weixin.senparc.com/Document

4、Senparc.Weixin SDK官网地址：https://weixin.senparc.com

5、Senparc.Weixin.MP.Sample Demo地址：https://sdk.weixin.senparc.com

6、微信开发资源收集：https://github.com/JeffreySu/WeixinResource");
                return 0;
            }

            if (QqGroup == true)
            {
                var dt1 = SystemTime.Now;
                var qqUrl = "https://dev.senparc.com/WeixinSdk/GetSdkQqGroupJson";
                var resultTask = Senparc.CO2NET.HttpUtility.Get.GetJsonAsync<List<SdkQQGroup>>(_serviceProvider, qqUrl);

                while (!resultTask.IsCompleted)
                {
                    WriteColorLine(".", ConsoleColor.DarkGray, false);
                    await Task.Delay(50);
                }
                WriteColorLine($"{SystemTime.DiffTotalMS(dt1)}ms", ConsoleColor.DarkGray, false);
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("QQ群（实时更新）：");
                foreach (var item in resultTask.Result)
                {
                    WriteColorLine($"{item.GroupName}: {item.GroupNumber} （{item.Note}）{(item.Open ? "开放申请" : "已满")}", item.Open ? ConsoleColor.Yellow : ConsoleColor.DarkGray);
                }
                Console.WriteLine();
                WriteColorLine($"友情提示：为开发者保留资源，请勿重复加群，谢谢合作！", ConsoleColor.DarkBlue, false);
                return 0;
            }


            if (string.IsNullOrEmpty(Keyword))
            {
                app.ShowHelp();
                return 0;
            }

            if (HostUrl != null)
            {
                _hostUrl = HostUrl.TrimEnd('/');
            }

            string platformQuery = null;
            string platformText = null;
            List<PlatformType> platformNames = new List<PlatformType>();
            Platform?.ToList().ForEach(p =>
            {
                if (p == null)
                {
                    return;
                }
                platformQuery += $"&platformType={p}";
                try
                {
                    platformNames.Add((PlatformType)Enum.Parse(typeof(PlatformType), p));
                }
                catch (Exception ex)
                {

                }
            });
            if (platformNames.Count > 0)
            {
                platformText = string.Join(',', platformNames.Select(z => z.ToString()));
            }

            var apiUrl = $"{_hostUrl}/FindWeixinAPi";
            var fullUrl = $"{apiUrl}?keyword={Keyword}{platformQuery}&isAsync={Async}";

            try
            {
                WriteColorLine($"搜索条件 >  关键字：{Keyword}    限定方法：{(Async.HasValue ? (Async.Value ? "异步" : "同步") : "全部")}    限定平台：{(platformText ?? "全部")}", ConsoleColor.Cyan);

                var dt1 = SystemTime.Now;
                var resultTask = Senparc.CO2NET.HttpUtility.Get.GetJsonAsync<FindWeixinApiResult>(_serviceProvider, fullUrl);

                while (!resultTask.IsCompleted)
                {
                    WriteColorLine(".", ConsoleColor.DarkGray, false);
                    await Task.Delay(50);
                }
                WriteColorLine($"{SystemTime.DiffTotalMS(dt1)}ms", ConsoleColor.DarkGray, false);
                var result = resultTask.Result;

                Console.WriteLine();

                var hasResult = result.ApiItemList.Count() > 0;
                for (int i = 0; i < result.ApiItemList.Count(); i++)
                {
                    var item = result.ApiItemList.ElementAt(i);
                    WriteColorLine($"[{i:0}] \t{GetFirstLineOfSummary(item.Summary)}", ConsoleColor.Green);
                    Console.WriteLine($"\t\t方法：{item.FullMethodName}");
                    //Console.WriteLine($"\t\t参数：{item.ParamsPart}");
                }

                Console.WriteLine();
                if (hasResult)
                {
                    WriteColorLine("输入编号并回车可直接展示对应代码示例，按回车（Enter）退出...", ConsoleColor.Yellow);

                    var indexStr = Console.ReadLine();
                    if (!indexStr.IsNullOrEmpty() && int.TryParse(indexStr, out int index))
                    {
                        if (index >= result.ApiItemList.Count())
                        {
                            Console.WriteLine("输入超出范围，已退出");
                        }
                        else
                        {
                            var apiItem = result.ApiItemList.ElementAt(index);
                            var code = $"var result = {(apiItem.IsAsync == true ? "await " : "")}{apiItem.FullMethodName}();";
                            Console.WriteLine();
                            Console.WriteLine("请复制以下命令直接使用：");
                            Console.WriteLine();
                            WriteColorLine($"\t//调用 {GetFirstLineOfSummary(apiItem.Summary).Replace("【异步方法】", "")} 接口", ConsoleColor.Green);
                            WriteColorLine($"\t{code}", ConsoleColor.White);
                            Console.WriteLine();
                            WriteColorLine($"\t参数：{apiItem.ParamsPart}", ConsoleColor.DarkGray);
                            WriteColorLine($"\t完整注释：{apiItem.Summary.Trim()}", ConsoleColor.DarkGray);
                        }
                    }
                }
                else
                {
                    WriteColorLine("没有找到想要的接口？让社区一起来实现：https://github.com/JeffreySu/WeiXinMPSDK/issues/new", ConsoleColor.Yellow);
                    WriteColorLine("[Y]是    [N]否", ConsoleColor.Green);

                    var yesOrNo = Console.ReadLine();
                    if (yesOrNo.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        //var url = "https://github.com/JeffreySu/WeiXinMPSDK/issues/new";
                        var url = "https://dev.senparc.com/QA";
                        OpenUrl(url);
                    }
                    else
                    {
                        WriteColorLine("欢迎前往开发者社区交流：https://dev.senparc.com/QA", ConsoleColor.Yellow);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                WriteColorLine("执行错误", ConsoleColor.Red);
                if (ex.Message.Contains("404 (Not Found)"))
                {
                    WriteColorLine($"\t请检查 HostUrl 设置是否正确，网站接口可能无法访问：{apiUrl}?keyword=anything", ConsoleColor.Red);
                }
                WriteColorLine($"\t{ex.Message}", ConsoleColor.Red);

            }
            return 1;
        }

        private void OpenUrl(string url)
        {
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception)
            {
                ProcessStartInfo info = new ProcessStartInfo(@$"C:\Program Files\Internet Explorer\iexplore.exe");
                info.Arguments = url;
                info.UseShellExecute = false;
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                Process.Start(info);
            }
        }
    }
}
