/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin 快捷注册流程（包括Thread、TraceLog等）


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180516
    修改描述：优化 RegisterService

----------------------------------------------------------------*/

using Microsoft.Extensions.Options;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin
{
    /// <summary>
    /// 
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程（.NET Core）
        /// </summary>
        /// <param name="env"></param>
        /// <param name="senparcWeixinSetting"></param>
        /// <param name="isDebug"></param>
        /// <returns></returns>
        public static RegisterService Start(this RegisterService registerService, IOptions<SenparcWeixinSetting> senparcWeixinSetting, bool isDebug)
        {
            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting.Value;
            return registerService;
        }

    }
}
