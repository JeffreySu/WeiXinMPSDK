/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：RegisterService.cs
    文件功能描述：Senparc.Weixin SDK 快捷注册流程


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180531
    修改描述：v4.22.2 修改 AddSenparcWeixinGlobalServices() 方法命名
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.Extensions.DependencyInjection;
#endif

namespace Senparc.Weixin.RegisterServices
{
    /// <summary>
    /// 快捷注册类，RegisterService 扩展类
    /// </summary>
    public static class RegisterServiceExtension
    {
#if NETCOREAPP2_0 || NETCOREAPP2_1

        /// <summary>
        /// 注册 IServiceCollection，并返回 RegisterService，开始注册流程
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddSenparcWeixinGlobalServices(this IServiceCollection serviceCollection)
        {
            RegisterService.GlobalServiceCollection = serviceCollection;
            return serviceCollection;
        }
#endif
    }
}
