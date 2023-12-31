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
    
    文件名：OaApi.cs
    文件功能描述：审批接口
    
    
    创建标识：mojinxun - 20230226
    
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.Mobile;
using Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA
{
    /// <summary>
    /// 
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class OaApi
    {
        #region 同步方法
        /// <summary>
        /// 获取审批模板详情
        /// https://developer.work.weixin.qq.com/document/path/91982
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetTemplateDetailResult GetTemplateDetail(string accessToken, GetTemplateDetailRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/gettemplatedetail?access_token={0}";
            return CommonJsonSend.Send<GetTemplateDetailResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 提交审批申请
        /// https://developer.work.weixin.qq.com/document/path/91853
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ApplyEventResult ApplyEvent(string accessToken, ApplyEventRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/applyevent?access_token={0}";
            return CommonJsonSend.Send<ApplyEventResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 批量获取审批单号
        /// https://developer.work.weixin.qq.com/document/path/91816
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetApprovalInfoResult GetApprovalInfo(string accessToken, GetApprovalInfoRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/getapprovalinfo?access_token={0}";
            return CommonJsonSend.Send<GetApprovalInfoResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取审批申请详情
        /// https://developer.work.weixin.qq.com/document/path/91983
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetApprovalDetailResult GetApprovalDetail(string accessToken, string sp_no, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/getapprovaldetail?access_token={0}";
            var data = new
            {
                sp_no
            };
            return CommonJsonSend.Send<GetApprovalDetailResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取企业假期管理配置
        /// https://developer.work.weixin.qq.com/document/path/93375
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static VacationGetCorpConfResult VacationGetCorpConf(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/vacation/getcorpconf?access_token={0}";
            return CommonJsonSend.Send<VacationGetCorpConfResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取企业假期管理配置
        /// https://developer.work.weixin.qq.com/document/path/93376
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static VacationGetUserVacationQuotaResult VacationGetUserVacationQuota(string accessToken, string userid, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/vacation/getuservacationquota?access_token={0}";
            var data = new
            {
                userid
            };
            return CommonJsonSend.Send<VacationGetUserVacationQuotaResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 修改成员假期余额
        /// https://developer.work.weixin.qq.com/document/path/93377
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetOneUserQuota(string accessToken, SetOneUserQuotaRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/vacation/setoneuserquota?access_token={0}";
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 创建审批模板
        /// https://developer.work.weixin.qq.com/document/path/97437
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ApprovalCreateTemplate(string accessToken, ApprovalCreateTemplateRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/approval/create_template?access_token={0}";
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 更新审批模板
        /// https://developer.work.weixin.qq.com/document/path/97438
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ApprovalUpdateTemplate(string accessToken, ApprovalUpdateTemplateRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/approval/update_template?access_token={0}";
            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion


        #region 异步方法
        /// <summary>
        /// 获取审批模板详情
        /// https://developer.work.weixin.qq.com/document/path/91982
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetTemplateDetailResult> GetTemplateDetailAsync(string accessToken, GetTemplateDetailRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/gettemplatedetail?access_token={0}";
            return await CommonJsonSend.SendAsync<GetTemplateDetailResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 提交审批申请
        /// https://developer.work.weixin.qq.com/document/path/91853
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ApplyEventResult> ApplyEventAsync(string accessToken, ApplyEventRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/applyevent?access_token={0}";
            return await CommonJsonSend.SendAsync<ApplyEventResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 批量获取审批单号
        /// https://developer.work.weixin.qq.com/document/path/91816
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetApprovalInfoResult> GetApprovalInfoAsync(string accessToken, GetApprovalInfoRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/getapprovalinfo?access_token={0}";
            return await CommonJsonSend.SendAsync<GetApprovalInfoResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取审批申请详情
        /// https://developer.work.weixin.qq.com/document/path/91983
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetApprovalDetailResult> GetApprovalDetailAsync(string accessToken, string sp_no, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/getapprovaldetail?access_token={0}";
            var data = new
            {
                sp_no
            };
            return await CommonJsonSend.SendAsync<GetApprovalDetailResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取企业假期管理配置
        /// https://developer.work.weixin.qq.com/document/path/93375
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<VacationGetCorpConfResult> VacationGetCorpConfAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/vacation/getcorpconf?access_token={0}";
            return await CommonJsonSend.SendAsync<VacationGetCorpConfResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取企业假期管理配置
        /// https://developer.work.weixin.qq.com/document/path/93376
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<VacationGetUserVacationQuotaResult> VacationGetUserVacationQuotaAsync(string accessToken, string userid, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/vacation/getuservacationquota?access_token={0}";
            var data = new
            {
                userid
            };
            return await CommonJsonSend.SendAsync<VacationGetUserVacationQuotaResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 修改成员假期余额
        /// https://developer.work.weixin.qq.com/document/path/93377
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SetOneUserQuotaAsync(string accessToken, SetOneUserQuotaRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/vacation/setoneuserquota?access_token={0}";
            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 创建审批模板
        /// https://developer.work.weixin.qq.com/document/path/97437
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ApprovalCreateTemplateAsync(string accessToken, ApprovalCreateTemplateRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/approval/create_template?access_token={0}";
            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 更新审批模板
        /// https://developer.work.weixin.qq.com/document/path/97438
        /// </summary>
        /// <param name="accessToken">	调用接口凭证。必须使用审批应用或企业内自建应用的secret获取</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ApprovalUpdateTemplateAsync(string accessToken, ApprovalUpdateTemplateRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/oa/approval/update_template?access_token={0}";
            return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion
    }
}
