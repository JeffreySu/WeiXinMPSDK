using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.NeuChar.ApiHandlers;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    public class WorkApiEnlightener : ApiEnlightener
    {
        public static ApiEnlightener Instance = new WorkApiEnlightener();

        public override NeuChar.PlatformType PlatformType { get; set; } = NeuChar.PlatformType.WeChat_Work;

        /// <summary>
        /// 发送文本客服消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public override ApiResult SendText(string accessTokenOrAppId, string openId, string content)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override ApiResult SendNews(string accessTokenOrAppId, string openId, List<Article> articleList)
        {
            throw new NotImplementedException();
        }
    }
}
