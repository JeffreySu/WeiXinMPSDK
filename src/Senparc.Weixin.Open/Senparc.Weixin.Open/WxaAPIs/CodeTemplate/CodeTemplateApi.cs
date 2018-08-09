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
    
    文件名：CodeApi.cs
    文件功能描述：小程序代码模版库管理
    
    
    创建标识：Senparc - 20171215


----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Entities;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 小程序代码模版库管理
    /// </summary>
    public class CodeTemplateApi
    {
        #region 同步方法
        /// <summary>
        /// 获取草稿箱内的所有临时代码草稿
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetTemplateDraftListResultJson GetTemplateDraftList(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/gettemplatedraftlist?access_token={0}", accessToken.AsUrlData());
            
            return CommonJsonSend.Send<GetTemplateDraftListResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取代码模版库中的所有小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetTemplateListResultJson GetTemplateList(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/gettemplatelist?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetTemplateListResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 将草稿箱的草稿选为小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="draft_id">草稿ID，本字段可通过“获取草稿箱内的所有临时代码草稿”接口获得</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult AddToTemplate(string accessToken, int draft_id, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/addtotemplate?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                draft_id = draft_id
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 删除指定小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="template_id">要删除的模版ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult DeleteTemplate(string accessToken, int template_id, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/deletetemplate?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                template_id = template_id
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 获取草稿箱内的所有临时代码草稿
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetTemplateDraftListResultJson> GetTemplateDraftListAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/gettemplatedraftlist?access_token={0}", accessToken.AsUrlData());

            return await CommonJsonSend.SendAsync<GetTemplateDraftListResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取代码模版库中的所有小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetTemplateListResultJson> GetTemplateListAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/gettemplatelist?access_token={0}", accessToken.AsUrlData());

            return await CommonJsonSend.SendAsync<GetTemplateListResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 将草稿箱的草稿选为小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="draft_id">草稿ID，本字段可通过“获取草稿箱内的所有临时代码草稿”接口获得</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> AddToTemplateAsync(string accessToken, int draft_id, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/addtotemplate?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                draft_id = draft_id
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 删除指定小程序代码模版
        /// </summary>
        /// <param name="accessToken">第三方平台自己的component_access_token</param>
        /// <param name="template_id">要删除的模版ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> DeleteTemplateAsync(string accessToken, int template_id, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/deletetemplate?access_token={0}", accessToken.AsUrlData());

            object data;

            data = new
            {
                template_id = template_id
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion
#endif
    }
}
