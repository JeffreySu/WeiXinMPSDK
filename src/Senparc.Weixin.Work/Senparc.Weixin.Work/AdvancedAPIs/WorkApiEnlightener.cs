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
  
    文件名：Register.cs
    文件功能描述：注册小程序信息
    
    
    创建标识：Senparc - 20180905

    修改标识：Senparc - 20191004
    修改描述：使用异步方法

    修改标识：Senparc - 20230709
    修改描述：v3.16.0 MessageHandler 和客服接口支持长文本自动切割后连续发送

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="limitedBytes">最大允许发送限制，如果超出限制，则分多条发送</param>
        /// <returns></returns>
        public override async Task<ApiResult> SendText(string accessTokenOrAppId, string openId, string content, int limitedBytes = 2048)
        {
            var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendTextAsync(accessTokenOrAppId, openId, content);
            return new ApiResult((int)result.errcode, result.errmsg, result);
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
            var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendImageAsync(accessTokenOrAppId, openId, mediaId);
            return new ApiResult((int)result.errcode, result.errmsg, result);
        }

        public override async Task<ApiResult> SendNews(string accessTokenOrAppId, string openId, List<Article> articleList)
        {
            var result = await Senparc.Weixin.Work.AdvancedAPIs.MassApi.SendNewsAsync(accessTokenOrAppId, openId, articleList);
            return new ApiResult((int)result.errcode, result.errmsg, result);
        }
    }
}
