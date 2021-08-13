/*----------------------------------------------------------------
    Copyright (C) 2021 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.Work 快捷注册流程


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterWorkAccount() 方法

    修改标识：Senparc - 20191003
    修改描述：注册过程自动添加更多 SenparcSettingItem 信息

----------------------------------------------------------------*/

using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work
{
    /// <summary>
    /// 注册企业微信
    /// </summary>
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
        public static IRegisterService RegisterWorkAccount(this IRegisterService registerService, string weixinCorpId, string weixinCorpSecret, string name = null, 
            Func<string, Task<string>> getSuiteTicketFunc = null,
            Func<string, Task<FuncGetIdSecretResult>> getPermanentCodeFunc = null)
        {
            ProviderTokenContainer.Register(weixinCorpId, weixinCorpSecret, getSuiteTicketFunc, getPermanentCodeFunc, name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册第三方平台信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForWork">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterWorkAccount(this IRegisterService registerService, Weixin.Entities.ISenparcWeixinSettingForWork weixinSettingForWork,string name = null,
            Func<string, Task<string>> getSuiteTicketFunc = null,
            Func<string, Task<FuncGetIdSecretResult>> getPermanentCodeFunc = null)
        {
            //配置全局参数
            if (!string.IsNullOrWhiteSpace(name))
            {
                Config.SenparcWeixinSetting[name] = new SenparcWeixinSettingItem(weixinSettingForWork);
            }
            return RegisterWorkAccount(registerService, weixinSettingForWork.WeixinCorpId, weixinSettingForWork.WeixinCorpSecret, name ?? weixinSettingForWork.ItemKey, getSuiteTicketFunc, getPermanentCodeFunc);
        }
        #region 设置 ApiHandlerWapper 处理方法

        /// <summary>
        /// 设置所有使用了 ApiHandlerWapper 的接口，可以自动进入重试的 API 错误代码
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="invalidCredentialReturnCodes">可进入重试的 API 错误代码</param>
        /// <returns></returns>
        public static IRegisterService SetWork_InvalidCredentialValues(this IRegisterService registerService, IEnumerable<ReturnCode> invalidCredentialReturnCodes)
        {
            ApiHandlerWapper.InvalidCredentialValues = invalidCredentialReturnCodes.Select(z => (int)z);
            return registerService;
        }
        #endregion
    }
}
