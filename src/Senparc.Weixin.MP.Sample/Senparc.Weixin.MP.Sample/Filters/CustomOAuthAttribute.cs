using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Senparc.Weixin.MP.Sample.Filters
{
    public class CustomOAuthAttribute : SenparcOAuthAttribute
    {
        static TenPayV3Info _tenPayV3Info = null;
        public static TenPayV3Info TenPayV3Info
        {
            get
            {
                if (_tenPayV3Info == null)
                {
                    _tenPayV3Info =
                        TenPayV3InfoCollection.Data[System.Configuration.ConfigurationManager.AppSettings["TenPayV3_MchId"]];
                }
                return _tenPayV3Info;
            }
        }

        public CustomOAuthAttribute(string appId, string oauthCallbackUrl) : base(appId, oauthCallbackUrl)
        {
            base._appId = base._appId ?? _tenPayV3Info.AppId;
        }

        public override bool IsLogined(HttpContextBase httpContext)
        {
            //也可以使用其他方法如Session验证用户登录
            return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}