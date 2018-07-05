/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：WeixinRegister.cs
    文件功能描述：Senparc.Weixin 快捷注册流程（包括Thread、TraceLog等）


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180516
    修改描述：优化 RegisterService


    修改标识：Senparc - 20180607
    修改描述：Register 改名为 WeixinRegister

    修改标识：Senparc - 201806029
    修改描述：v5.0.3.1 修复 WeixinRegister.UseSenparcWeixin() 方法的 IsDebug 设置问题

----------------------------------------------------------------*/

#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.Extensions.Options;
#endif
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;

namespace Senparc.Weixin
{
    /// <summary>
    /// 微信注册
    /// </summary>
    public static class WeixinRegister
    {

        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="senparcWeixinSetting"></param>
        /// <param name="isDebug"></param>
        /// <param name="containerCacheStrategiesFunc">需要注册的扩展Container缓存策略（LocalContainerCacheStrategy已经自动注册）</param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IRegisterService registerService, SenparcWeixinSetting senparcWeixinSetting, bool isDebug, Func<IList<IDomainExtensionCacheStrategy>> containerCacheStrategiesFunc)
        {
            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = isDebug;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting;

            // 微信的 本地 缓存
            var cache = LocalContainerCacheStrategy.Instance;

            if (containerCacheStrategiesFunc != null)
            {
                var containerCacheStrategies = containerCacheStrategiesFunc();
                if (containerCacheStrategies!=null)
                {
                    foreach (var cacheStrategy in containerCacheStrategies)
                    {
                        var exCache = cacheStrategy;//确保能运行到就行，会自动注册
                    }
                }
            }

            return registerService;
        }
    }
}
