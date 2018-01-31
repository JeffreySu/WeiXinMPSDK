#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：CommentApi.cs
    文件功能描述：评论数据管理
    
    
    创建标识：Senparc - 20180131
    
----------------------------------------------------------------*/

/* 
   API地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1494572718_WzHIY
*/


using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 评论数据管理
    /// </summary>
    public static class CommentApi
    {

        #region 同步方法

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult Open(string accessTokenOrAppId, UInt32 msg_data_id, UInt32? index  , int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/comment/open?access_token={0}";
                var data = new
                {
                    msg_data_id = msg_data_id,
                    index = index
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法



        #endregion
#endif
    }
}
