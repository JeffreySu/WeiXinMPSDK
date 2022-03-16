using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;
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
        private readonly string _corpSecret;


        public OAuth2Controller()
        {
            _workWeixinSetting = Senparc.Weixin.Config.SenparcWeixinSetting["企业微信OAuth2.0"];
            _corpId = _workWeixinSetting.WeixinCorpId;
            _corpSecret = _workWeixinSetting.WeixinCorpSecret;
        }

        public IActionResult Index(string returnUrl)
        {
            // 设置自己的 URL
            var url = "https://4424-222-93-135-159.ngrok.io";

            //此页面引导用户点击授权
            var oauthUrl =
                OAuth2Api.GetCode(_corpId, $"{url}/OAuth2/BaseCallback?returnUrl={returnUrl.UrlEncode()}",
                    null, null);//snsapi_base方式回调地址

            ViewData["UrlBase"] = oauthUrl;
            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="returnUrl">用户最初尝试进入的页面</param>
        /// <returns></returns>
        public async Task<ActionResult> BaseCallback(string code, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            try
            {
                var appKey = AccessTokenContainer.BuildingKey(_workWeixinSetting);
                var accessToken = await AccessTokenContainer.GetTokenAsync(_corpId, _corpSecret);
                //获取用户信息 测试链接：https://open.work.weixin.qq.com/wwopen/devtool/interface?doc_id=10019
                var oauthResult = await OAuth2Api.GetUserIdAsync(accessToken, code);
                var userId = oauthResult.UserId;
                GetMemberResult result = await MailListApi.GetMemberAsync(appKey, userId);

                if (result.errcode != ReturnCode_Work.请求成功)
                {
                    return Content("错误：" + result.errmsg);
                }

                ViewData["returnUrl"] = returnUrl;

                /* 注意：
                    * 实际适用场景，此处应该跳转到 returnUrl，不要停留在 Callback页面上。
                    * 因为当用户刷新此页面 URL 时，实际 code 等参数已失效，用户会受到错误信息。
                    */
                return View(result);
            }
            catch (Exception ex)
            {
                return Content("错误：" + ex.Message);
            }
        }
    }
}

public class Women
{
    uint _age;
    public Women()
    {
        _age = 0;
    }

    public void YearPass()
    {
        _age = _age + 1;

        if (_age > 18)
        {
            _age = 18;
        }
        else
        {

        }
    }
}