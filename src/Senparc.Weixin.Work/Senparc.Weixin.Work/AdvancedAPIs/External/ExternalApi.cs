/*----------------------------------------------------------------
    Copyright (C) 2020 Senparc
    
    文件名：ExternalApi.cs
    文件功能描述：外部联系人接口
    
    
    创建标识：Senparc - 20181009
   
    修改标识：lishewen - 20200318
    修改描述：v3.7.401 新增“获取客户群列表”“获取客户群详情” API
    
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
        /// <summary>
        /// 获取客户群列表
        /// 权限说明:
        /// 企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用。
        /// 暂不支持第三方调用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="data">查询参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ExternalApi.GroupChatList", true)]
        public static GroupChatListResult GroupChatList(string accessTokenOrAppKey, GroupChatListParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/list?access_token={0}", accessToken);
                return CommonJsonSend.Send<GroupChatListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }
        /// <summary>
        /// 获取客户群详情
        /// 权限说明:
        /// 企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用。
        /// 暂不支持第三方调用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="chat_id">客户群ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ExternalApi.GroupChatGet", true)]
        public static GroupChatGetResult GroupChatGet(string accessTokenOrAppKey, string chat_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/get?access_token={0}", accessToken);
                var data = new
                {
                    chat_id
                };
                return CommonJsonSend.Send<GroupChatGetResult>(null, url, data, CommonJsonSendType.POST, timeOut);
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
        /// <summary>
        /// 【异步方法】获取客户群列表
        /// 权限说明:
        /// 企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用（accesstoken如何获取？）。
        /// 暂不支持第三方调用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="data">查询参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ExternalApi.GroupChatListAsync", true)]
        public static async Task<GroupChatListResult> GroupChatListAsync(string accessTokenOrAppKey, GroupChatListParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/list?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GroupChatListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }
        /// <summary>
        /// 【异步方法】获取客户群详情
        /// 权限说明:
        /// 企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用。
        /// 暂不支持第三方调用。
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="chat_id">客户群ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "ExternalApi.GroupChatGetAsync", true)]
        public static async Task<GroupChatGetResult> GroupChatGetAsync(string accessTokenOrAppKey, string chat_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/get?access_token={0}", accessToken);
                var data = new
                {
                    chat_id
                };
                return await CommonJsonSend.SendAsync<GroupChatGetResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }
        #endregion
    }
}
