using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;
using System;
using System.IO;
using System.Text;

namespace Senparc.WeixinTests
{
    /// <summary>
    /// 单元测试基类
    /// </summary>
    public class BaseTest
    {
        protected IServiceProvider _serviceProvider;
        protected SenparcSetting _senparcSetting;
        protected SenparcWeixinSetting _senparcWeixinSetting;

        protected IConfiguration _config;

        public BaseTest()
        {
            RegisterStart();
        }

        /// <summary>
        /// 运行默认注册流程
        /// </summary>
        protected void RegisterStart()
        {
            var builder = new ConfigurationBuilder();
            //var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddJsonFile($"appsettings.Test.json", false, true);
            Console.WriteLine("完成 appsettings.json 添加");

            _config = builder.Build();
            Console.WriteLine("完成 ServiceCollection 和 ConfigurationBuilder 初始化");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//支持 GB2312

            //注册开始
            RegisterService register;

            //注册 CON2ET 全局
            var senparcSetting = _config.GetSection("SenparcSetting").Get<SenparcSetting>();
            var senparcWeixinSetting = _config.GetSection("SenparcWeixinSetting").Get<SenparcWeixinSetting>();

            var mockEnv = new Mock<IHostEnvironment>();

            mockEnv.Setup(z => z.ContentRootPath).Returns(() => UnitTestHelper.RootPath);
            register = Senparc.CO2NET.AspNet.RegisterServices.RegisterService.Start(mockEnv.Object, senparcSetting);

            RegisterServiceCollection();

            //Func<List<IDomainExtensionCacheStrategy>> func = () =>
            //{
            //    var list = new List<IDomainExtensionCacheStrategy>();
            //    list.Add(LocalContainerCacheStrategy.Instance);
            //    list.Add(RedisContainerCacheStrategy.Instance);
            //    //list.Add(MemcachedContainerCacheStrategy.Instance);
            //    return list;
            //};
            //register.UseSenparcGlobal(false, func);
            register.UseSenparcGlobal(false);

            //注册微信
            Senparc.Weixin.All.WeixinEntensions.use
            register.UseSenparcWeixin(senparcWeixinSetting, senparcSetting);
            register.ChangeDefaultCacheNamespace("Senparc.Weixin Test Cache");
        }

        /// <summary>
        /// 注册 IServiceCollection 和 MemoryCache
        /// </summary>
        public void RegisterServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder();

            var appSettingsTestFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UnitTestHelper.RootPath, "appsettings.Test.json"));
            var appSettingsFileExisted = File.Exists(appSettingsTestFilePath);

            if (appSettingsFileExisted)
            {
                configBuilder.AddJsonFile("appsettings.Test.json", false, false);//此文件可能包含敏感信息，不可上传至公共库
            }
            else
            {
                if (File.Exists(appSettingsTestFilePath.Replace(".Test", "")))
                {
                    configBuilder.AddJsonFile("appsettings.json", false, false);//默认使用 appsettings.json
                    appSettingsFileExisted = true;
                }
            }

            var config = configBuilder.Build();

            _senparcSetting = new SenparcSetting() { IsDebug = true };
            _senparcWeixinSetting = new SenparcWeixinSetting() { IsDebug = true };

            if (appSettingsFileExisted)
            {
                config.GetSection("SenparcSetting").Bind(_senparcSetting);
                config.GetSection("SenparcWeixinSetting").Bind(_senparcWeixinSetting);
            }

            serviceCollection.AddMemoryCache();//使用内存缓存

            //已经包含 AddSenparcGlobalServices()，注意：必须在所有注册完成后执行
            serviceCollection.AddSenparcWeixin(config);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// 获取到达项目根目录的相对路径
        /// </summary>
        /// <returns></returns>
        protected string GetParentRootRelativePath()
        {
            return @"..\..\..\";
        }
    }
}
