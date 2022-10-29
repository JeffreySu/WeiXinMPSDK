using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.Containers;

namespace Senparc.Weixin.Sample.Work.Controllers
{
    public class AdvancedApiController : Controller
    {
        /// <summary>
        /// 接口调用方式一：使用 AppKey
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> TryApiByAppKey()
        {
            // 获取注册信息
            var workWeixinSetting = Config.SenparcWeixinSetting.WorkSetting;
            // 获取 AppKey
            var appKey = AccessTokenContainer.BuildingKey(workWeixinSetting);
            //发送请求
            try
            {
                //发送文字提醒
                var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendTextAsync(appKey, "001", "这是一条来企业微信的消息");
                return Content("OK");
            }
            catch (ErrorJsonResultException ex)
            {
                return Content($"出错啦：{ex.Message}");
            }
        }

        /// <summary>
        /// 接口调用方式二：使用 AccessToken
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> TryApiByAccessToken()
        {
            // 获取注册信息
            var workWeixinSetting = Config.SenparcWeixinSetting.WorkSetting;
            // 获取 AccessToken
            var accessToken = await AccessTokenContainer.GetTokenAsync(workWeixinSetting.WeixinCorpId, workWeixinSetting.WeixinCorpSecret);
            //发送请求
            try
            {
                //发送文字提醒
                var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendTextAsync(accessToken, "001", "这是一条来企业微信的消息");
                return Content("OK");
            }
            catch (ErrorJsonResultException ex)
            {
                return Content($"出错啦：{ex.Message}");
            }
        }
    }
}
