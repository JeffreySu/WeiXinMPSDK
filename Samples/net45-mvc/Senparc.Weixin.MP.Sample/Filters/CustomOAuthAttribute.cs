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
            //如果是多租户，appId 可以传入 null，并且忽略下一行，使用 IsLogined() 方法中的动态赋值语句
            base._appId = base._appId ?? Config.SenparcWeixinSetting.TenPayV3_AppId;//填写公众号AppId（适用于公众号、微信支付、JsApi等）
        }

        public override bool IsLogined(HttpContextBase httpContext)
        {
            //如果是多租户，也可以这样写，通过 URL 参数来区分：
            //base._appId = HttpContext.Current.Request.QueryString["appId"];//appId也可以是数据库存储的Id，避免暴露真实的AppId

            return httpContext != null && httpContext.Session["OpenId"] != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}