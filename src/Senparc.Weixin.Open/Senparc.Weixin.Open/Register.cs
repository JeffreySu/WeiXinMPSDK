using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Containers;
using Senparc.Weixin.RegisterServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open
{
    public static class Register
    {
        /// <summary>
        /// 注册第三方平台信息
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getComponentVerifyTicketFunc">获取ComponentVerifyTicket的方法</param>
        /// <param name="getAuthorizerRefreshTokenFunc">从数据库中获取已存的AuthorizerAccessToken的方法</param>
        /// <param name="authorizerTokenRefreshedFunc">AuthorizerAccessToken更新后的回调</param>
        /// <param name="name">标记名称（如开放平台名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterOpenComponent(this IRegisterService registerService,
            string componentAppId, string componentAppSecret,
            Func<string, string> getComponentVerifyTicketFunc,
            Func<string, string, string> getAuthorizerRefreshTokenFunc,
            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc,
            string name = null)
        {
            ComponentContainer.Register(
                            componentAppId, componentAppSecret,
                            getComponentVerifyTicketFunc,
                            getAuthorizerRefreshTokenFunc,
                            authorizerTokenRefreshedFunc,
                            name);
            return registerService;
        }


    }
}
