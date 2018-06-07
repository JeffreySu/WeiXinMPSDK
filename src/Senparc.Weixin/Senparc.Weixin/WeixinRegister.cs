/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：WeixinRegister.cs
    文件功能描述：Senparc.Weixin 快捷注册流程（包括Thread、TraceLog等）


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180516
    修改描述：优化 RegisterService


    修改标识：Senparc - 20180607
    修改描述：Register 改名为 WeixinRegister

----------------------------------------------------------------*/

#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.Extensions.Options;
#endif
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin
{
    /// <summary>
    /// 微信注册
    /// </summary>
    public static class WeixinRegister
    {

#if NETCOREAPP2_0 || NETCOREAPP2_1

        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程（.NET Core）
        /// </summary>
        /// <param name="env"></param>
        /// <param name="senparcWeixinSetting"></param>
        /// <param name="isDebug"></param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IRegisterService registerService, IOptions<SenparcWeixinSetting> senparcWeixinSetting, bool isDebug)
        {
            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting.Value;
            return registerService;
        }
#endif
    }
}
