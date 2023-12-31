/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：IcpApi.cs
    文件功能描述：第三方服务商小程序备案 接口
    
    
    创建标识：Senparc - 20230905

    修改标识：Senparc - 20230805
    修改描述：v4.15.0 完善“第三方服务商小程序备案”接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.NeuChar;
using Senparc.Weixin.Annotations;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.Icp;
using Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 三方平台API - 小程序备案
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class IcpApi
    {
        #region 同步方法

          
        /// <summary>
        /// 查询人脸核身任务状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="task_id">人脸核身任务id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryIcpVerifyTaskResultJson QueryIcpVerifyTask(string accessToken, string task_id, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_verifytask?access_token={0}", accessToken.AsUrlData());
            var postData = new
            {
                task_id
            };

            return CommonJsonSend.Send<QueryIcpVerifyTaskResultJson>(null, url, postData, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发起小程序管理员人脸核身
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static CreateIcpVerifyTaskResultJson CreateIcpVerifyTask(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/create_icp_verifytask?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<CreateIcpVerifyTaskResultJson>(null, url, null, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 上传小程序备案媒体材料
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="type"></param>
        /// <param name="icp_order_field"></param>
        /// <param name="certificate_type"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadIcpMediaResultJson UploadIcpMedia(string accessToken, string type, string icp_order_field, string file, int certificate_type = -1, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/upload_icp_media?access_token={0}", accessToken.AsUrlData());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            fileDictionary["type"] = type;
            fileDictionary["icp_order_field"] = icp_order_field;
            if (certificate_type > -1)
            {
                fileDictionary["certificate_type"] = certificate_type.ToString();
            }
          
            return Post.PostFileGetJson<UploadIcpMediaResultJson>(CommonDI.CommonSP, url, null, fileDictionary, timeOut: timeOut);
        }

        /// <summary>
        /// 撤回小程序备案申请
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult CancelApplyIcpFiling(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/cancel_apply_icp_filing?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<WxJsonResult>(null, url, null, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 申请小程序备案
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="postData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ApplyIcpFilingResultJson ApplyIcpFiling(string accessToken, ApplyIcpFilingData postData, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/apply_icp_filing?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<ApplyIcpFilingResultJson>(null, url, postData, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 注销小程序备案
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancel_type">注销类型：1 -- 注销主体, 2 -- 注销小程序, 3 -- 注销微信小程序</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ApplyIcpFilingResultJson CancelIcpFiling(string accessToken, int cancel_type,int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/cancel_icp_filing?access_token={0}", accessToken.AsUrlData());

            var postData = new
            {
                cancel_type
            };
            return CommonJsonSend.Send<ApplyIcpFilingResultJson>(null, url, postData, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取小程序备案状态及驳回原因
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetIcpEntranceInfoResultJson GetIcpEntranceInfo(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/get_icp_entrance_info?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<GetIcpEntranceInfoResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取小程序已备案详情
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetOnlineIcpOrderResultJson GetOnlineIcpOrder(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/get_online_icp_order?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<GetOnlineIcpOrderResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取小程序服务内容类型
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryIcpServiceContentTypesResultJson QueryIcpServiceContentTypes(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_service_content_types?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<QueryIcpServiceContentTypesResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取证件类型
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryIcpCertificateTypesResultJson QueryIcpCertificateTypes(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_certificate_types?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<QueryIcpCertificateTypesResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryIcpDistrictCodeResultJson QueryIcpDistrictCode(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_district_code?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<QueryIcpDistrictCodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取前置审批项类型
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryIcpNrlxTypesResultJson QueryIcpNrlxTypes(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_nrlx_types?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<QueryIcpNrlxTypesResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取单位性质
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static QueryIcpSubjectTypesResultJson QueryIcpSubjectTypes(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_subject_types?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<QueryIcpSubjectTypesResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 查询人脸核身任务状态
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="task_id">人脸核身任务id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryIcpVerifyTaskResultJson> QueryIcpVerifyTaskAsync(string accessToken, string task_id, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_verifytask?access_token={0}", accessToken.AsUrlData());
            var postData = new
            {
                task_id
            };

            return await CommonJsonSend.SendAsync<QueryIcpVerifyTaskResultJson>(null, url, postData, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发起小程序管理员人脸核身
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<CreateIcpVerifyTaskResultJson> CreateIcpVerifyTaskAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/create_icp_verifytask?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<CreateIcpVerifyTaskResultJson>(null, url, null, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 上传小程序备案媒体材料
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="type"></param>
        /// <param name="icp_order_field"></param>
        /// <param name="certificate_type"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<UploadIcpMediaResultJson> UploadIcpMediaAsync(string accessToken, string type, string icp_order_field, string file, int certificate_type = -1, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/upload_icp_media?access_token={0}", accessToken.AsUrlData());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;
            fileDictionary["type"] = type;
            fileDictionary["icp_order_field"] = icp_order_field;
            if (certificate_type > -1)
                fileDictionary["certificate_type"] = certificate_type.ToString();

            return await Post.PostFileGetJsonAsync<UploadIcpMediaResultJson>(CommonDI.CommonSP, url, null, fileDictionary,timeOut: timeOut);

        }

        /// <summary>
        /// 撤回小程序备案申请
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CancelApplyIcpFilingAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/cancel_apply_icp_filing?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, null, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 申请小程序备案
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="postData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ApplyIcpFilingResultJson> ApplyIcpFilingAsync(string accessToken, ApplyIcpFilingData postData, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/apply_icp_filing?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<ApplyIcpFilingResultJson>(null, url, postData, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 注销小程序备案
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancel_type">注销类型：1 -- 注销主体, 2 -- 注销小程序, 3 -- 注销微信小程序</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> CancelIcpFilingAsync(string accessToken, int cancel_type, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/cancel_icp_filing?access_token={0}", accessToken.AsUrlData());

            var postData = new
            {
                cancel_type
            };
            return await CommonJsonSend.SendAsync<ApplyIcpFilingResultJson>(null, url, postData, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取小程序备案状态及驳回原因
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetIcpEntranceInfoResultJson> GetIcpEntranceInfoAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/get_icp_entrance_info?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<GetIcpEntranceInfoResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取小程序已备案详情
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetOnlineIcpOrderResultJson> GetOnlineIcpOrderAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/get_online_icp_order?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<GetOnlineIcpOrderResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取小程序服务内容类型
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryIcpServiceContentTypesResultJson> QueryIcpServiceContentTypesAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_service_content_types?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<QueryIcpServiceContentTypesResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取证件类型
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryIcpCertificateTypesResultJson> QueryIcpCertificateTypesAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_certificate_types?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<QueryIcpCertificateTypesResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取区域信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryIcpDistrictCodeResultJson> QueryIcpDistrictCodeAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_district_code?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<QueryIcpDistrictCodeResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取前置审批项类型
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryIcpNrlxTypesResultJson> QueryIcpNrlxTypesAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_nrlx_types?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<QueryIcpNrlxTypesResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 获取单位性质
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<QueryIcpSubjectTypesResultJson> QueryIcpSubjectTypesAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_subject_types?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<QueryIcpSubjectTypesResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        #endregion
    }
}
