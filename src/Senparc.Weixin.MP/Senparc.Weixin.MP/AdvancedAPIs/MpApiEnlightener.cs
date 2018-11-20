using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.NeuChar;
using Senparc.NeuChar.ApiHandlers;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

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
            try
            {
                var result = AdvancedAPIs.CustomApi.SendText(accessTokenOrAppId, openId, content);
                return new ApiResult((int)result.errcode, result.errmsg, result);
            }
            catch (ErrorJsonResultException ex)
            {
                return new ApiResult(ex.JsonResult.ErrorCodeValue, ex.JsonResult.errmsg, ex.JsonResult);
            }
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
            try
            {
                var result = AdvancedAPIs.CustomApi.SendImage(accessTokenOrAppId, openId, mediaId);
                return new ApiResult((int)result.errcode, result.errmsg, result);
            }
            catch (ErrorJsonResultException ex)
            {
                return new ApiResult(ex.JsonResult.ErrorCodeValue, ex.JsonResult.errmsg, ex.JsonResult);
            }
        }

        public override ApiResult SendNews(string accessTokenOrAppId, string openId, List<Article> articleList)
        {
            try
            {
                var news = articleList.Select(z => new NewsModel()
                {
                    title = z.Title,
                    content = "点击【阅读原文】访问",//内容暂时无法获取到
                    digest = z.Description,
                    content_source_url = z.Url,
                    thumb_url = z.PicUrl
                }).ToArray();
                //上传临时素材
                var newsResult = AdvancedAPIs.MediaApi.UploadTemporaryNews(accessTokenOrAppId, news: news);
                var result = AdvancedAPIs.CustomApi.SendMpNews(accessTokenOrAppId, openId, newsResult.media_id);
                return new ApiResult((int)result.errcode, result.errmsg, result);
            }
            catch (ErrorJsonResultException ex)
            {
                return new ApiResult(ex.JsonResult.ErrorCodeValue, ex.JsonResult.errmsg, ex.JsonResult);
            }
        }
    }
}
