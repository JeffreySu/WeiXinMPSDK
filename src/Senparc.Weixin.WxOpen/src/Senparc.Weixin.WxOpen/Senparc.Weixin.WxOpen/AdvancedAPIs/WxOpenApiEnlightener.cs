using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar.ApiHandlers;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.NeuralSystems;

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

            var result = AdvancedAPIs.CustomApi.SendText(accessTokenOrAppId, openId, content);
            return new ApiResult((int)result.errcode, result.errmsg, result);
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
            var result = AdvancedAPIs.CustomApi.SendImage(accessTokenOrAppId, openId, mediaId);
            return new ApiResult((int)result.errcode, result.errmsg, result);
        }

        /// <summary>
        /// 返回多图文消息（转成文字发送）
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="articleList"></param>
        /// <returns></returns>
        public override ApiResult SendNews(string accessTokenOrAppId, string openId, List<Article> articleList)
        {
            ApiResult apiResult = null;
            int i = 0;
            foreach (var article in articleList)
            {
                var result = AdvancedAPIs.CustomApi.SendLink(accessTokenOrAppId, openId, article.Title, article.Description, article.Url, article.PicUrl);
                if (i == articleList.Count() - 1)
                {
                    apiResult = new ApiResult((int)result.errcode, result.errmsg, result);
                }
                i++;
            }

            return apiResult ?? new ApiResult();
        }
    }
}
