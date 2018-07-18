using System.Web;
using Senparc.Weixin.MP.MvcExtension;

namespace Senparc.Weixin.MP.Sample.Filters
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
        }

        public override bool IsLogined(HttpContextBase httpContext)
        {
            return httpContext != null && httpContext.Session["OpenId"] != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}