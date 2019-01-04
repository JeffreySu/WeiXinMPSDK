using System.Web;
using Microsoft.AspNetCore.Http;
using Senparc.CO2NET;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.MP.MvcExtension;

namespace Senparc.Weixin.MP.CoreSample.Filters
{
    /// <summary>
    /// OAuth自动验证，可以加在Action或整个Controller上
    /// </summary>
    public class CustomOAuthAttribute : SenparcOAuthAttribute
    {
        public CustomOAuthAttribute(string appId, string oauthCallbackUrl)
            : base(appId, oauthCallbackUrl)
        {
            base._appId = base._appId ?? Config.SenparcWeixinSetting.TenPayV3_AppId;

            //如果是多租户，也可以这样写：
#if NETCOREAPP2_2
            var httpContextAccessor = SenparcDI.GetService<IHttpContextAccessor>();
            base._appId = httpContextAccessor.HttpContext.Request.Query["appId"];

            SenparcTrace.SendCustomLog("SenparcOAuthAttribute 测试", httpContextAccessor.HttpContext.Request.Query["appId"]);
#else
            base._appId = HttpContext.Current.Request.QueryString["appId"];
#endif

        }

        public override bool IsLogined(HttpContext httpContext)
        {
            return httpContext != null && httpContext.Session.GetString("OpenId") != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}