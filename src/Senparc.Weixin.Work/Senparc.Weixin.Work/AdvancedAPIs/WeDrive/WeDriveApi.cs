#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 chinanhb & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
/*----------------------------------------------------------------
    Copyright (C) 2023 Senparc
    
    文件名：WeiSpaceApi
    文件功能描述：企业微信微盘接口
    
    
    创建标识：chinanhb - 20230831
----------------------------------------------------------------*/

/*
    API：https://developer.work.weixin.qq.com/document/path/93654
    
 */
#endregion Apache License Version 2.0
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive
{
    /// <summary>
    /// 企业微信微盘接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public class WeDriveApi
    {
        #region 同步方法
        /// <summary>
        /// 微盘内新建空间，创建者为应用本身
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SpaceCreateJsonResult SpaceCreate(string accessTokenOrAppKey, SpaceCreateModel data, int timeOut = Config.TIME_OUT)
        {
            ///https://developer.work.weixin.qq.com/document/path/93655
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {

                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_create?access_token={0}", accessToken);
                return CommonJsonSend.Send<SpaceCreateJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】微盘内新建空间，创建者为应用本身
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<SpaceCreateJsonResult> SpaceCreateAsync(string accessTokenOrAppKey, SpaceCreateModel data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/wedrive/space_create?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<SpaceCreateJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion
    }
}
