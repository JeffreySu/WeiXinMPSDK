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
  
    文件名：Register.cs
    文件功能描述：注册小程序信息
    
    
    创建标识：Senparc - 20180905

    修改标识：Senparc - 20191004
    修改描述：使用异步方法

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
        /// <returns></returns>
        public override async Task<ApiResult> SendText(string accessTokenOrAppId, string openId, string content)
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
        public override async Task<ApiResult> SendImage(string accessTokenOrAppId, string openId, string mediaId)
        {
            throw new NotImplementedException();
        }

        public override async Task<ApiResult> SendNews(string accessTokenOrAppId, string openId, List<Article> articleList)
        {
            throw new NotImplementedException();
        }
    }
}
