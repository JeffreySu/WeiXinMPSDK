#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：SenparcWeixinSettingItem.cs
    文件功能描述：Senparc.Weixin SDK 中单个公众号配置信息
    
    
    创建标识：Senparc - 20180915

	修改标识：Senparc - 20191004
    修改描述：使用异步方法

	修改标识：Senparc - 20230709
    修改描述：v16.19.0 MessageHandler 和客服接口支持长文本自动切割后连续发送

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="limitedBytes">最大允许发送限制，如果超出限制，则分多条发送</param>
        /// <returns></returns>
        public override async Task<ApiResult> SendText(string accessTokenOrAppId, string openId, string content, int limitedBytes = 2048)
        {
            try
            {
                var result = await AdvancedAPIs.CustomApi.SendTextAsync(accessTokenOrAppId, openId, content, limitedBytes: limitedBytes);
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
        public override async Task<ApiResult> SendImage(string accessTokenOrAppId, string openId, string mediaId)
        {
            try
            {
                var result = await AdvancedAPIs.CustomApi.SendImageAsync(accessTokenOrAppId, openId, mediaId);
                return new ApiResult((int)result.errcode, result.errmsg, result);
            }
            catch (ErrorJsonResultException ex)
            {
                return new ApiResult(ex.JsonResult.ErrorCodeValue, ex.JsonResult.errmsg, ex.JsonResult);
            }
        }

        public override async Task<ApiResult> SendNews(string accessTokenOrAppId, string openId, List<Article> articleList)
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
                var newsResult = await AdvancedAPIs.MediaApi.UploadTemporaryNewsAsync(accessTokenOrAppId, news: news);
                var result = await AdvancedAPIs.CustomApi.SendMpNewsAsync(accessTokenOrAppId, openId, newsResult.media_id);
                return new ApiResult((int)result.errcode, result.errmsg, result);
            }
            catch (ErrorJsonResultException ex)
            {
                return new ApiResult(ex.JsonResult.ErrorCodeValue, ex.JsonResult.errmsg, ex.JsonResult);
            }
        }
    }
}
