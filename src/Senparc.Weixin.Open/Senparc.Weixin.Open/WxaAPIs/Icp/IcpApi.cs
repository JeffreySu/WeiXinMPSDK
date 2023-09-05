using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
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
    public class IcpApi
    {
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

            return CommonJsonSend.Send<QueryIcpVerifyTaskResultJson>(null, url, postData);
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

            return CommonJsonSend.Send<CreateIcpVerifyTaskResultJson>(null, url, null);
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
        public static UploadIcpMediaResultJson UploadIcpMedia(string accessToken, string type, string icp_order_field, string file, int certificate_type=-1, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/upload_icp_media?access_token={0}", accessToken.AsUrlData());
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = file;

            var dataDictionary = new Dictionary<string, string>();
            dataDictionary["type"] = type;
            dataDictionary["icp_order_field"] = icp_order_field;
            dataDictionary["certificate_type"] = certificate_type.ToString();

            return Post.PostFileGetJson<UploadIcpMediaResultJson>(CommonDI.CommonSP, url, null, fileDictionary, dataDictionary, timeOut: timeOut);

        }


        public static WxJsonResult CancelApplyIcpFiling(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/cancel_apply_icp_filing?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<WxJsonResult>(null, url, null);
        }

        public static ApplyIcpFilingResultJson ApplyIcpFiling(string accessToken, ApplyIcpFilingData postData, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/apply_icp_filing?access_token={0}", accessToken.AsUrlData());

            return CommonJsonSend.Send<ApplyIcpFilingResultJson>(null, url, postData);
        }

        /// <summary>
        /// 注销小程序备案
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancel_type">注销类型：1 -- 注销主体, 2 -- 注销小程序, 3 -- 注销微信小程序</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult CancelIcpFiling(string accessToken, int cancel_type,int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/cancel_icp_filing?access_token={0}", accessToken.AsUrlData());

            var postData = new
            {
                cancel_type
            };


            return CommonJsonSend.Send<ApplyIcpFilingResultJson>(null, url, postData);
        }

        public static GetIcpEntranceInfoResultJson GetIcpEntranceInfo(string accessToken,int timeOut=Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/get_icp_entrance_info?access_token={0}", accessToken.AsUrlData());


            return CommonJsonSend.Send<GetIcpEntranceInfoResultJson>(null, url, null);
        }

        public static GetOnlineIcpOrderResultJson GetOnlineIcpOrder(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/get_online_icp_order?access_token={0}", accessToken.AsUrlData());


            return CommonJsonSend.Send<GetOnlineIcpOrderResultJson>(null, url, null);
        }

        public static QueryIcpServiceContentTypesResultJson QueryIcpServiceContentTypes(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_service_content_types?access_token={0}", accessToken.AsUrlData());


            return CommonJsonSend.Send<QueryIcpServiceContentTypesResultJson>(null, url, null);
        }

        public static QueryIcpCertificateTypesResultJson QueryIcpCertificateTypes(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_certificate_types?access_token={0}", accessToken.AsUrlData());


            return CommonJsonSend.Send<QueryIcpCertificateTypesResultJson>(null, url, null);
        }

        public static QueryIcpDistrictCodeResultJson QueryIcpDistrictCode(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/icp/query_icp_district_code?access_token={0}", accessToken.AsUrlData());


            return CommonJsonSend.Send<QueryIcpDistrictCodeResultJson>(null, url, null);
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


            return CommonJsonSend.Send<QueryIcpNrlxTypesResultJson>(null, url, null);
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


            return CommonJsonSend.Send<QueryIcpSubjectTypesResultJson>(null, url, null);
        }
    }
}
