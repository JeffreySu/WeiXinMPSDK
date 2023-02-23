using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;
using Senparc.WeixinTests;
using System;
using System.IO;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Test
{
    [TestClass]
    public class BaseTenPayTest
    {
        protected static IServiceProvider _serviceProvider;

        protected static SenparcSetting _senparcSetting;
        protected static SenparcWeixinSetting _senparcWeixinSetting;

        public BaseTenPayTest()
        {
            //Senparc.Weixin.Config.UseSandBoxPay = true;
            RegisterStart();
        }

        /// <summary>
        /// 运行默认注册流程
        /// </summary>
        protected void RegisterStart()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//支持 GB2312

            //注册开始
            RegisterService register;

            //注册 CON2ET 全局
            var senparcSetting = new SenparcSetting() { IsDebug = true };

            var mockEnv = new Mock<IHostEnvironment>();

            mockEnv.Setup(z => z.ContentRootPath).Returns(() => UnitTestHelper.RootPath);
            register = Senparc.CO2NET.AspNet.RegisterServices.RegisterService.Start(mockEnv.Object, senparcSetting);

            RegisterServiceCollection();

            register.UseSenparcGlobal(false);

            //注册微信
            //var senparcWeixinSetting = new SenparcWeixinSetting(true);
            register.UseSenparcWeixin(_senparcWeixinSetting, senparcSetting).RegisterTenpayApiV3(_senparcWeixinSetting, "微信 V3");
            register.ChangeDefaultCacheNamespace("Senparc.Weixin Test Cache");
        }

        /// <summary>
        /// 注册 IServiceCollection 和 MemoryCache
        /// </summary>
        public static void RegisterServiceCollection()
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
            serviceCollection.AddSenparcWeixinServices(config);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
