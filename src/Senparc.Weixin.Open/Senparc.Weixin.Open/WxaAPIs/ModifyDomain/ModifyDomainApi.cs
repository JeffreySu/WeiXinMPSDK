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

    文件名：ModifyDomainApi.cs
    文件功能描述：修改域名接口


    创建标识：Senparc - 20170601

    修改标识：Senparc - 20171201
    修改描述：v1.7.3 修复ModifyDomainApi.ModifyDomain()方法判断问题
        
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Open.WxaAPIs.ModifyDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class ModifyDomainApi
    {
        #region 同步方法

        /// <summary>
        /// 修改服务器地址 接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="requestdomain">request合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="wsrequestdomain">socket合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="uploaddomain">uploadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="downloaddomain">downloadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "ModifyDomainApi.ModifyDomain", true)]
        public static ModifyDomainResultJson ModifyDomain(string accessToken, ModifyDomainAction action,
            List<string> requestdomain,
            List<string> wsrequestdomain,
            List<string> uploaddomain,
            List<string> downloaddomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/modify_domain?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == ModifyDomainAction.get)
            {
                data = new
                {
                    action = action.ToString()
                };
            }
            else
            {
                data = new
                {
                    action = action.ToString(),
                    requestdomain = requestdomain,
                    wsrequestdomain = wsrequestdomain,
                    uploaddomain = uploaddomain,
                    downloaddomain = downloaddomain
                };
            }

            return CommonJsonSend.Send<ModifyDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        #endregion



        #region 异步方法

        /// <summary>
        /// 【异步方法】修改服务器地址 接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="requestdomain">request合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="wsrequestdomain">socket合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="uploaddomain">uploadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="downloaddomain">downloadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Open, "ModifyDomainApi.ModifyDomainAsync", true)]
        public static async Task<ModifyDomainResultJson> ModifyDomainAsync(string accessToken, ModifyDomainAction action,
            List<string> requestdomain,
            List<string> wsrequestdomain,
            List<string> uploaddomain,
            List<string> downloaddomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/modify_domain?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == ModifyDomainAction.get)
            {
                data = new
                {
                    action = action.ToString()
                };
            }
            else
            {
                data = new
                {
                    action = action.ToString(),
                    requestdomain = requestdomain,
                    wsrequestdomain = wsrequestdomain,
                    uploaddomain = uploaddomain,
                    downloaddomain = downloaddomain
                };
            }

            return await CommonJsonSend.SendAsync<ModifyDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }


        #endregion
    }
}
