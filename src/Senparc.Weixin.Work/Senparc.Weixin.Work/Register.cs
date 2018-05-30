/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.Work 快捷注册流程


    创建标识：Senparc - 20180222

----------------------------------------------------------------*/

using Senparc.Weixin.RegisterServices;
using Senparc.Weixin.Work.Containers;

namespace Senparc.Weixin.Work
{
    public static class Register
    {
        /// <summary>
        /// 注册公众号（或小程序）信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinCorpId">weixinCorpId</param>
        /// <param name="weixinCorpSecret">weixinCorpSecret</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterWorkAccount(this IRegisterService registerService, string weixinCorpId, string weixinCorpSecret, string name = null)
        {
            ProviderTokenContainer.Register(weixinCorpId, weixinCorpSecret, name);
            return registerService;
        }

    }
}
