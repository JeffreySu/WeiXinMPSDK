/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ExternalApi.cs
    文件功能描述：外部联系人接口
    
    
    创建标识：Senparc - 20181009
    
    
----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#13473
 */

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.External;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 外部联系人管理
    /// </summary>
    public static class ExternalApi
    {
        #region 同步方法

        /// <summary>
        /// 离职成员的外部联系人再分配
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="handoverUserId"></param>
        /// <param name="takeoverUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ExternalApi.TransferExternal", true)]
        public static WorkJsonResult TransferExternal(string accessTokenOrAppKey, string ExternalUserId, string handoverUserId, string takeoverUserId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/crm/transfer_external_contact?access_token={0}", accessToken);
                var data = new
                {
                    external_userid = ExternalUserId,
                    handover_userid = handoverUserId,
                    takeover_userid = takeoverUserId
                };
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 获取外部联系人详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ExternalApi.GetExternalContact", true)]
        public static GetExternalContactResultJson GetExternalContact(string accessTokenOrAppKey, string ExternalUserId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/crm/get_external_contact?access_token={0}&external_userid={1}", accessToken, ExternalUserId);

                return CommonJsonSend.Send<GetExternalContactResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);

        }

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】离职成员的外部联系人再分配
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="handoverUserId"></param>
        /// <param name="takeoverUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ExternalApi.TransferExternalAsync", true)]
        public static async Task<WorkJsonResult> TransferExternalAsync(string accessTokenOrAppKey, string ExternalUserId, string handoverUserId, string takeoverUserId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/crm/transfer_external_contact?access_token={0}", accessToken);
                var data = new
                {
                    external_userid = ExternalUserId,
                    handover_userid = handoverUserId,
                    takeover_userid = takeoverUserId
                };
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】获取外部联系人详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ExternalApi.GetExternalContactAsync", true)]
        public static async Task<GetExternalContactResultJson> GetExternalContactAsync(string accessTokenOrAppKey, string ExternalUserId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/crm/get_external_contact?access_token={0}&external_userid={1}", accessToken, ExternalUserId);

                return await CommonJsonSend.SendAsync<GetExternalContactResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        #endregion
    }
}
