#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：WxOpenApiEnlightener.cs
    文件功能描述：WxOpenApiEnlightener
    
    
    创建标识：Senparc - 20180910

    
----------------------------------------------------------------*/

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
