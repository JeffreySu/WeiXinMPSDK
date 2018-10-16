using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.NeuChar;
using Senparc.NeuChar.ApiHandlers;


namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class MpApiEnlightener : ApiEnlightener
    {
        public static ApiEnlightener Instance = new MpApiEnlightener();

        public override NeuChar.PlatformType PlatformType { get; set; } = NeuChar.PlatformType.WeChat_OfficialAccount;

        /// <summary>
        /// 发送文本客服消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public override ApiResult SendText(string accessTokenOrAppId, string openId, string content)
        {
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
