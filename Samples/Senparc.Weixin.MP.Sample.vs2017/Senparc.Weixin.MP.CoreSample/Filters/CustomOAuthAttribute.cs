using System.Linq;
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
            base._appId = httpContextAccessor.HttpContext.Request.Query["appId"].FirstOrDefault();//appId也可以是数据库存储的Id，避免暴露真实的AppId

            SenparcTrace.SendCustomLog("SenparcOAuthAttribute3 测试00", httpContextAccessor.HttpContext.Request.ToJson(true));
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute3 测试00", httpContextAccessor.HttpContext.Request.AbsoluteUri());
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute3 测试11", httpContextAccessor.HttpContext.Request.Query["appId"].FirstOrDefault());
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute3 测试22", httpContextAccessor.HttpContext.Request.Query["oauthCallbackUrl"].FirstOrDefault());
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute3 测试33", httpContextAccessor.HttpContext.Request.Query["productId"].FirstOrDefault());
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute3 测试44", httpContextAccessor.HttpContext.Request.Query["hc"].FirstOrDefault());
        }

        public override bool IsLogined(HttpContext httpContext)
        {
            var httpContextAccessor = SenparcDI.GetService<IHttpContextAccessor>();
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute4 测试00", httpContextAccessor.HttpContext.Request.ToJson(true));
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute4 测试00", httpContextAccessor.HttpContext.Request.AbsoluteUri());
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute4 测试11", httpContextAccessor.HttpContext.Request.Query["appId"].FirstOrDefault());
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute4 测试22", httpContextAccessor.HttpContext.Request.Query["oauthCallbackUrl"].FirstOrDefault());
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute4 测试33", httpContextAccessor.HttpContext.Request.Query["productId"].FirstOrDefault());
            SenparcTrace.SendCustomLog("SenparcOAuthAttribute4 测试44", httpContextAccessor.HttpContext.Request.Query["hc"].FirstOrDefault());

            return httpContext != null && httpContext.Session.GetString("OpenId") != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}