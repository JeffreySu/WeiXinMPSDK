using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.Work;
using Senparc.Weixin.WxOpen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Senparc.Weixin.WxOpen.Tests
{
    [TestClass]
    public class WeixinRegisterTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var pathSeparator = Path.DirectorySeparatorChar.ToString();
            var altPathSeparator = Path.AltDirectorySeparatorChar.ToString();
            Console.WriteLine(pathSeparator);
            Console.WriteLine(altPathSeparator);
        }

        [TestMethod]
        public void TestRegisterMpAccount()
        {
            SenparcWeixinSetting senparcWeixinSetting;
            SenparcSetting senparcSetting;
            IConfiguration config;

            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            senparcSetting = new SenparcSetting();
            senparcWeixinSetting = new SenparcWeixinSetting();

            config.GetSection("SenparcSetting").Bind(senparcSetting);
            config.GetSection("SenparcWeixinSetting").Bind(senparcWeixinSetting);

            var services = new ServiceCollection();
            services.AddSenparcGlobalServices(config);

            IRegisterService register = RegisterService.Start(senparcSetting).UseSenparcGlobal();

            #region Redis配置
            var redisConfigurationStr = senparcSetting.Cache_Redis_Configuration;
            Senparc.CO2NET.Cache.Redis.Register.SetConfigurationOption(redisConfigurationStr);
            Senparc.CO2NET.Cache.Redis.Register.UseKeyValueRedisNow();

            // 这里需要同时安装以下两个Nuget包，否则启动报错
            // Senparc.Weixin.Cache.Redis
            // Senparc.Weixin.Cache.CsRedis
            register.UseSenparcWeixinCacheRedis();

            #endregion


            var logFile = Path.Combine(Directory.GetCurrentDirectory(), "Debugger.log");
            var logs = new List<string>();
            logs.Add("**********************    Start configuration the senparcSDK    **********************");
            logs.Add($"    ");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  Before the method: UseSenparcWeixin Invoke");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppId: {senparcWeixinSetting.WeixinAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppSecret: {senparcWeixinSetting.WeixinAppSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  Token: {senparcWeixinSetting.Token}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  EncodingAESKey: {senparcWeixinSetting.EncodingAESKey}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpId: {senparcWeixinSetting.WeixinCorpId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpSecret: {senparcWeixinSetting.WeixinCorpSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppId: {senparcWeixinSetting.WxOpenAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppSecret: {senparcWeixinSetting.WxOpenAppSecret}");

            register.UseSenparcWeixin(senparcWeixinSetting, senparcSetting);

            logs.Add($"    ");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  After the method: UseSenparcWeixin Invoke & Before the method: RegisterMpAccount Invoke");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppId: {senparcWeixinSetting.WeixinAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppSecret: {senparcWeixinSetting.WeixinAppSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  Token: {senparcWeixinSetting.Token}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  EncodingAESKey: {senparcWeixinSetting.EncodingAESKey}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpId: {senparcWeixinSetting.WeixinCorpId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpSecret: {senparcWeixinSetting.WeixinCorpSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppId: {senparcWeixinSetting.WxOpenAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppSecret: {senparcWeixinSetting.WxOpenAppSecret}");

            register.RegisterMpAccount(senparcWeixinSetting.MpSetting); // 公众号账号配置

            logs.Add($"    ");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  After the method: RegisterMpAccount Invoke & Before the method: RegisterWorkAccount");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppId: {senparcWeixinSetting.WeixinAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppSecret: {senparcWeixinSetting.WeixinAppSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  Token: {senparcWeixinSetting.Token}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  EncodingAESKey: {senparcWeixinSetting.EncodingAESKey}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpId: {senparcWeixinSetting.WeixinCorpId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpSecret: {senparcWeixinSetting.WeixinCorpSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppId: {senparcWeixinSetting.WxOpenAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppSecret: {senparcWeixinSetting.WxOpenAppSecret}");

            register.RegisterWorkAccount(senparcWeixinSetting.WorkSetting); // 企业微信账号配置

            logs.Add($"    ");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  After the method: RegisterWorkAccount Invoke & Before the method: RegisterWxOpenAccount");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppId: {senparcWeixinSetting.WeixinAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppSecret: {senparcWeixinSetting.WeixinAppSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  Token: {senparcWeixinSetting.Token}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  EncodingAESKey: {senparcWeixinSetting.EncodingAESKey}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpId: {senparcWeixinSetting.WeixinCorpId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpSecret: {senparcWeixinSetting.WeixinCorpSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppId: {senparcWeixinSetting.WxOpenAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppSecret: {senparcWeixinSetting.WxOpenAppSecret}");

            register.RegisterWxOpenAccount(senparcWeixinSetting.WxOpenSetting); // 小程序账号配置

            logs.Add($"    ");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  After the method: RegisterWxOpenAccount Invoke");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppId: {senparcWeixinSetting.WeixinAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinAppSecret: {senparcWeixinSetting.WeixinAppSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  Token: {senparcWeixinSetting.Token}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  EncodingAESKey: {senparcWeixinSetting.EncodingAESKey}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpId: {senparcWeixinSetting.WeixinCorpId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WeixinCorpSecret: {senparcWeixinSetting.WeixinCorpSecret}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppId: {senparcWeixinSetting.WxOpenAppId}");
            logs.Add($"{DateTime.Now:yyyy/MM/dd HH:mm:ss fffff}  WxOpenAppSecret: {senparcWeixinSetting.WxOpenAppSecret}");
            logs.Add($"    ");
            logs.Add("**********************    End configuration the senparcSDK    **********************");

            File.AppendAllLines(logFile, logs, Encoding.UTF8);
            Console.WriteLine(string.Join("\r\n",logs));
        }
    }
}
