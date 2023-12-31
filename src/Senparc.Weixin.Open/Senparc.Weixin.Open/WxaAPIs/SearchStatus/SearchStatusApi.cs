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

    文件名：SearchStatusApi.cs
    文件功能描述：搜索状态接口


    创建标识：Yaofeng - 20220805
        
----------------------------------------------------------------*/

//文档：https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/basic-info-management/getSearchStatus.html

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.ModifyDomain;
using Senparc.Weixin.Open.WxaAPIs.SearchStatus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class SearchStatusApi
    {
        #region 同步方法
        /// <summary>
        /// 获取搜索状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWxaSearchStatusJsonResult GetWxaSearchStatus(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/getwxasearchstatus?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<GetWxaSearchStatusJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 设置搜索状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ChangeWxaSearchStatus(string accessToken, int status, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/changewxasearchstatus?access_token={0}", accessToken.AsUrlData());
            object data = new
            {
                status = status
            };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 获取搜索状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetWxaSearchStatusJsonResult> GetWxaSearchStatusAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/getwxasearchstatus?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<GetWxaSearchStatusJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 设置搜索状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ChangeWxaSearchStatusAsync(string accessToken, int status, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/changewxasearchstatus?access_token={0}", accessToken.AsUrlData());
            object data = new
            {
                status = status
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion
    }
}
