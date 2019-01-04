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

            //如果是多租户，也可以这样写，通过 URL 参数来区分：
            var httpContextAccessor = SenparcDI.GetService<IHttpContextAccessor>();
            base._appId = httpContextAccessor.HttpContext.Request.Query["appId"];//appId也可以是数据库存储的Id，避免暴露真实的AppId

            SenparcTrace.SendCustomLog("SenparcOAuthAttribute2 测试11", httpContextAccessor.HttpContext.Request.Query["appId"]);
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute2 测试22", httpContextAccessor.HttpContext.Request.Query["oauthCallbackUrl"]);
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute2 测试22", httpContextAccessor.HttpContext.Request.Query["oauthCallbackUrl"]);
        }

        public override bool IsLogined(HttpContext httpContext)
        {
            return httpContext != null && httpContext.Session.GetString("OpenId") != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}