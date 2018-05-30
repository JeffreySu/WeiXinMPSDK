/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin 快捷注册流程（包括Thread、TraceLog等）


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180516
    修改描述：优化 RegisterService

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.Threads;

namespace Senparc.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 修改默认缓存命名空间
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="customNamespace">自定义命名空间名称</param>
        /// <returns></returns>
        public static IRegisterService ChangeDefaultCacheNamespace(this IRegisterService registerService, string customNamespace)
        {
            Weixin.Config.DefaultCacheNamespace = customNamespace;
            return registerService;
        }


        /// <summary>
        /// 注册 Threads 的方法（如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <returns></returns>
        public static IRegisterService RegisterThreads(this IRegisterService registerService)
        {
            ThreadUtility.Register();//如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
            return registerService;
        }

        /// <summary>
        /// 注册 TraceLog 的方法
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IRegisterService RegisterTraceLog(this IRegisterService registerService, Action action)
        {
            action();
            return registerService;
        }
    }
}
