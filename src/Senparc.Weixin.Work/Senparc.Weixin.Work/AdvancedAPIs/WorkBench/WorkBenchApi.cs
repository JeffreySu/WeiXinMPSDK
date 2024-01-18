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
    Copyright (C) 2024 Senparc
    
    文件名：WorkBenchApi
    文件功能描述：自定义工作台相关接口
    
    
    创建标识：chinanhb - 20230603
----------------------------------------------------------------*/

/*
    API：https://developer.work.weixin.qq.com/document/path/92535
    
 */
#endregion Apache License Version 2.0
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.WorkBench.WorkBenchJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.Work.AdvancedAPIs.WorkBench
{
    /// <summary>
    /// 自定义工作台接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public class WorkBenchApi
    {
        #region 同步方法
        /// <summary>
        /// 指定应用自定义模版类型
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static SetWorkBenchTemplateJsonResult SetWorkBenchTemplate(string accessTokenOrAppKey,SetWorkBenchTemplateModel data,int timeOut=Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/agent/set_workbench_template?access_token={0}", accessToken);
                return CommonJsonSend.Send<SetWorkBenchTemplateJsonResult>(null, url, data,CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey); 
        }
        /// <summary>
        /// 获取应用在工作台展示的模版
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetWorkBenchTemplateJsonResult GetWorkBenchTemplate(string accessTokenOrAppKey, GetWorkBenchTemplateModel data, int timeOut=Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/agent/get_workbench_template?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetWorkBenchTemplateJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 设置应用在用户工作台展示的数据
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WorkJsonResult SetWorkBenchData(string accessTokenOrAppKey,SetWorkBenchDataModel data,int timeOut=Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/agent/set_workbench_data?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】指定应用自定义模版类型
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<SetWorkBenchTemplateJsonResult> SetWorkBenchTemplateAsync(string accessTokenOrAppKey, SetWorkBenchTemplateModel data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/agent/set_workbench_template?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<SetWorkBenchTemplateJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】获取应用在工作台展示的模版
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetWorkBenchTemplateJsonResult> GetWorkBenchTemplateAsync(string accessTokenOrAppKey, int agentid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/agent/get_workbench_template?access_token={0}", accessToken);
                var data = new
                {
                    agentid,
                };
                return await CommonJsonSend.SendAsync<GetWorkBenchTemplateJsonResult>(accessToken, url, data, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 【异步方法】设置应用在用户工作台展示的数据
        /// </summary>
        /// <param name="accessTokenOrAppKey">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SetWorkBenchDataAsync(string accessTokenOrAppKey, SetWorkBenchDataModel data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/agent/set_workbench_data?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion
    }
}
