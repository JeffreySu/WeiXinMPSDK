/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：RegisterService.cs
    文件功能描述：Senparc.Weixin SDK 快捷注册流程


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180517
    修改描述：完善 .net core 注册流程

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Senparc.Weixin.Entities;
#endif

namespace Senparc.Weixin.RegisterServices
{
    /// <summary>
    /// 快捷注册接口
    /// </summary>
    public interface IRegisterService
    {

    }

    /// <summary>
    /// 快捷注册类，IRegisterService的默认实现
    /// </summary>
    public class RegisterService : IRegisterService
    {

#if NETCOREAPP2_0 || NETCOREAPP2_1
        /// <summary>
        /// 全局 ServiceCollection
        /// </summary>
        public static IServiceCollection GlobalServiceCollection { get; set; }

        /// <summary>
        /// 单个实例引用全局的 ServiceCollection
        /// </summary>
        public IServiceCollection ServiceCollection => GlobalServiceCollection;

        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程（.NET Core）
        /// </summary>
        /// <param name="env"></param>
        /// <param name="senparcWeixinSetting"></param>
        /// <param name="isDebug"></param>
        /// <returns></returns>
        public static RegisterService Start(IHostingEnvironment env, IOptions<SenparcWeixinSetting> senparcWeixinSetting,bool isDebug)
        {
            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting.Value;

            //提供网站根目录
            if (env.ContentRootPath != null)
            {
                Senparc.Weixin.Config.RootDictionaryPath = env.ContentRootPath;
            }

            var register = new RegisterService();

            //如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
            register.RegisterThreads();//默认把线程注册好

            return register;
        }

#else
        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程
        /// </summary>
        /// <returns></returns>
        public static RegisterService Start()
        {
            var register = new RegisterService();

            //如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
            register.RegisterThreads();//默认把线程注册好

            return register;
        }
#endif
    }
}
