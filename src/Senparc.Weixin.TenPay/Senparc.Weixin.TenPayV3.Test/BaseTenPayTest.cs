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
        /// ����Ĭ��ע������
        /// </summary>
        protected void RegisterStart()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//֧�� GB2312

            //ע�Ὺʼ
            RegisterService register;

            //ע�� CON2ET ȫ��
            var senparcSetting = new SenparcSetting() { IsDebug = true };

            var mockEnv = new Mock<IHostEnvironment>();

            mockEnv.Setup(z => z.ContentRootPath).Returns(() => UnitTestHelper.RootPath);
            register = Senparc.CO2NET.AspNet.RegisterServices.RegisterService.Start(mockEnv.Object, senparcSetting);

            RegisterServiceCollection();

            register.UseSenparcGlobal(false);

            //ע��΢��
            //var senparcWeixinSetting = new SenparcWeixinSetting(true);
            register.UseSenparcWeixin(_senparcWeixinSetting, senparcSetting).RegisterTenpayApiV3(_senparcWeixinSetting, "΢�� V3");
            register.ChangeDefaultCacheNamespace("Senparc.Weixin Test Cache");
        }

        /// <summary>
        /// ע�� IServiceCollection �� MemoryCache
        /// </summary>
        public static void RegisterServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder();

            var appSettingsTestFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UnitTestHelper.RootPath, "appsettings.Test.json"));
            var appSettingsFileExisted = File.Exists(appSettingsTestFilePath);

            if (appSettingsFileExisted)
            {
                configBuilder.AddJsonFile("appsettings.Test.json", false, false);//���ļ����ܰ���������Ϣ�������ϴ���������
            }
            else
            {
                if (File.Exists(appSettingsTestFilePath.Replace(".Test", "")))
                {
                    configBuilder.AddJsonFile("appsettings.json", false, false);//Ĭ��ʹ�� appsettings.json
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

            serviceCollection.AddMemoryCache();//ʹ���ڴ滺��

            //�Ѿ����� AddSenparcGlobalServices()��ע�⣺����������ע����ɺ�ִ��
            serviceCollection.AddSenparcWeixinServices(config);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
