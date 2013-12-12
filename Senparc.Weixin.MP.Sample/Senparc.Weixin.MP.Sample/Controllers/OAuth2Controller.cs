using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class OAuth2Controller : Controller
    {
        //下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        private string appId = "wxaa572be2f86423fc";
        private string secret = "21a4cdca12444e5c79e4445cb184b38c";

        public ActionResult Index()
        {
            //此页面引导用户点击授权
            var url = OAuth.GetAuthorizeUrl(appId,
                "http://weixin.senparc.com/oauth2/callback", "JeffreySu", OAuthScope.snsapi_userinfo);
            ViewData["Url"] = url;
            return View();
        }

        public ActionResult Callback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "JeffreySu")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下，实际上可以存任何想传递的数据，比如用户ID
                return Content("验证失败！请从正规途径进入！");
            }

            //通过，用code换取access_token
            var result = OAuth.GetAccessToken(appId, secret, code);
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }

            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            Session["OAuthAccessToken"] = result;

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            var userInfo = OAuth.GetUserInfo(result.access_token, result.openid);
            return View(userInfo);
        }
    }
}