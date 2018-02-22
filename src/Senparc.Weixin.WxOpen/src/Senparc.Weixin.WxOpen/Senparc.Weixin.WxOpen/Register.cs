using Senparc.Weixin.Exceptions;
using Senparc.Weixin.RegisterServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen
{
    public static class Register
    {
        /// <summary>
        /// 注册小程序信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterWxOpenAccount(this IRegisterService registerService, string appId, string appSercet, string name = null)
        {
            throw new WeixinException("请统一使用Senparc.Weixin.MP.Register.RegisterMpAccount()方法进行注册！");
        }

    }
}
