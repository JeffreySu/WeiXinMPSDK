using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar.ApiHandlers;


namespace Senparc.Weixin.WxOpen.AdvancedAPIs
{
    public class WxOpenApiEnlightener : ApiEnlightener
    {
        public static ApiEnlightener Instance = new WxOpenApiEnlightener();
        public override NeuChar.PlatformType PlatformType { get; set; } = NeuChar.PlatformType.WeChat_MiniProgram;

        /// <summary>
        /// 发送文本客服消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public override ApiResult SendText(string accessTokenOrAppId, string openId, string content)
        {
            SenparcTrace.SendCustomLog("wxTest-sendText", "openID：" + openId + " || appID:" + accessTokenOrAppId + "|| content:" + content);

            var reuslt = AdvancedAPIs.CustomApi.SendText(accessTokenOrAppId, openId, content);
            return new ApiResult((int)reuslt.errcode, reuslt.errmsg, reuslt);
        }

        /// <summary>
        /// 发送图片客服消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public override ApiResult SendImage(string accessTokenOrAppId, string openId, string mediaId)
        {
            var reuslt = AdvancedAPIs.CustomApi.SendImage(accessTokenOrAppId, openId, mediaId);
            return new ApiResult((int)reuslt.errcode, reuslt.errmsg, reuslt);
        }
    }
}
