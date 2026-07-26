/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MailListCurrentApi.cs
    文件功能描述：企业微信当前通讯录补充接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐加入企业二维码与单个部门详情接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    public static partial class MailListApi
    {
        /// <summary>获取加入企业二维码。</summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证或已注册的 AppKey。</param>
        /// <param name="sizeType">二维码尺寸类型；不传时使用企业微信默认尺寸。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        public static GetJoinQrcodeResult GetJoinQrcode(string accessTokenOrAppKey, int? sizeType = null,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/corp/get_join_qrcode?access_token=" +
                          accessToken.AsUrlData();
                if (sizeType.HasValue)
                {
                    url += "&size_type=" + sizeType.Value;
                }

                return CommonJsonSend.Send<GetJoinQrcodeResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>异步获取加入企业二维码。</summary>
        public static Task<GetJoinQrcodeResult> GetJoinQrcodeAsync(string accessTokenOrAppKey,
            int? sizeType = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/corp/get_join_qrcode?access_token=" +
                          accessToken.AsUrlData();
                if (sizeType.HasValue)
                {
                    url += "&size_type=" + sizeType.Value;
                }

                return await CommonJsonSend.SendAsync<GetJoinQrcodeResult>(null, url, null,
                    CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
        }

        /// <summary>获取单个部门详情。</summary>
        public static GetDepartmentResult GetDepartment(string accessTokenOrAppKey, long id,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/department/get?access_token=" +
                          accessToken.AsUrlData() + "&id=" + id;
                return CommonJsonSend.Send<GetDepartmentResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>异步获取单个部门详情。</summary>
        public static Task<GetDepartmentResult> GetDepartmentAsync(string accessTokenOrAppKey, long id,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/department/get?access_token=" +
                          accessToken.AsUrlData() + "&id=" + id;
                return await CommonJsonSend.SendAsync<GetDepartmentResult>(null, url, null,
                    CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
        }
    }
}
