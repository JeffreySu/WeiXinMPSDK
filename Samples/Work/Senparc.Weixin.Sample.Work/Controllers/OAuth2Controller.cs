using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Containers;

namespace Senparc.Weixin.Sample.Work.Controllers
{
    /// <summary>
    /// 企业微信 OAuth 2.0 示例
    /// <para>官方文档：https://developer.work.weixin.qq.com/document/path/91335</para>
    /// </summary>
    public class OAuth2Controller : Controller
    {
        private readonly ISenparcWeixinSettingForWork _workWeixinSetting;
        private readonly string _corpId;


        public OAuth2Controller()
        {
            _workWeixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting["企业微信OAuth2.0"];
            _corpId = _workWeixinSetting.WeixinCorpId;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            var url = "https://4424-222-93-135-159.ngrok.io";

            //此页面引导用户点击授权
            ViewData["UrlUserInfo"] =
                OAuth2Api.GetCode(_corpId, $"{url}/OAuth2/UserInfoCallback?returnUrl={returnUrl.UrlEncode()}",
                null, null);//snsapi_userinfo方式回调地址

            ViewData["UrlBase"] =
                OAuth2Api.GetCode(_corpId, $"{url}/OAuth2/BaseCallback?returnUrl={returnUrl.UrlEncode()}",
                null, null);//snsapi_base方式回调地址

            return View();
        }

        ///// <summary>
        ///// OAuthScope.snsapi_userinfo方式回调
        ///// </summary>
        ///// <param name="code"></param>
        ///// <param name="returnUrl">用户最初尝试进入的页面</param>
        ///// <returns></returns>
        //public async ActionResult UserInfoCallback(string code, string returnUrl)
        //{
        //    if (string.IsNullOrEmpty(code))
        //    {
        //        return Content("您拒绝了授权！");
        //    }



        //    OAuthAccessTokenResult result = null;



        //    //通过，用code换取access_token
        //    try
        //    {
        //        var appKey = AccessTokenContainer.BuildingKey(_workWeixinSetting);
        //        var accessToken = await AccessTokenContainer.GetTokenAsync(_workWeixinSetting.WeixinCorpId, _workWeixinSetting.WeixinCorpSecret);
        //        var oauthResult =await OAuth2Api.GetUserIdAsync(accessToken, code);
        //        var userId = oauthResult.UserId;
        //        result = OAuthApi.GetAccessToken(appId, appSecret, code);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //    if (result.errcode != ReturnCode.请求成功)
        //    {
        //        return Content("错误：" + result.errmsg);
        //    }

        //    //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
        //    //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
        //    HttpContext.Session.SetString("OAuthAccessTokenStartTime", SystemTime.Now.ToString());
        //    HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());

        //    //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }

        //        OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
        //        return View(userInfo);
        //    }
        //    catch (ErrorJsonResultException ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}

    }
}
