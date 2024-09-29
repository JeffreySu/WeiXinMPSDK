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
        protected ServiceCollection _serviceCollection;

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

             _serviceCollection = new ServiceCollection();

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
            _senparcSetting = _config.GetSection("SenparcSetting").Get<SenparcSetting>();
            _senparcWeixinSetting = _config.GetSection("SenparcWeixinSetting").Get<SenparcWeixinSetting>();

            #region 注册 IServiceCollection 和 MemoryCache

            _serviceCollection.AddMemoryCache();//使用内存缓存

            //已经包含 AddSenparcGlobalServices()，注意：必须在所有注册完成后执行
            _serviceCollection.AddSenparcWeixin(_config);

            _serviceProvider = _serviceCollection.BuildServiceProvider();
            
            #endregion

            var mockEnv = new Mock<IHostEnvironment>();

            mockEnv.Setup(z => z.ContentRootPath).Returns(() => UnitTestHelper.RootPath);
            register = Senparc.CO2NET.AspNet.RegisterServices.RegisterService.Start(mockEnv.Object, _senparcSetting);

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
            Senparc.Weixin.All.WeixinEntensions.UseSenparcWeixin(register, _senparcWeixinSetting, (r, s) => { }, true, _serviceProvider);
            register.UseSenparcWeixin(_senparcWeixinSetting, _senparcSetting);
            register.ChangeDefaultCacheNamespace("Senparc.Weixin Test Cache");
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
