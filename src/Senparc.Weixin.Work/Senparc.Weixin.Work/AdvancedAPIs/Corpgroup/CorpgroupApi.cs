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

    文件名：CorpgroupApi.cs
    文件功能描述：上下游及互联企业相关接口


    创建标识：mojinxun - 20230226

----------------------------------------------------------------*/


using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Rule;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup
{
    /// <summary>
    /// 上下游及互联企业相关接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public class CorpgroupApi
    {
        #region 同步方法
        /// <summary>
        /// 获取应用共享信息
        /// 局校互联中的局端或者上下游中的上游企业通过该接口可以获取某个应用分享给的所有企业列表。
        /// 特别注意，对于有敏感权限的应用，需要下级/下游企业确认后才能共享成功，若下级/下游企业未确认，则不会存在于该接口的返回列表
        /// https://developer.work.weixin.qq.com/document/path/95813
        /// </summary>
        /// <param name="accessToken">调用接口凭证，上级/上游企业应用access_token</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static ListAppShareInfoResult CorpListAppShareInfo(string accessToken, ListAppShareInfoRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/list_app_share_info?access_token={0}";
            return CommonJsonSend.Send<ListAppShareInfoResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取下级/下游企业的access_token
        /// 获取应用可见范围内下级/下游企业的access_token，该access_token可用于调用下级/下游企业通讯录的只读接口。
        /// https://developer.work.weixin.qq.com/document/path/95816
        /// </summary>
        /// <param name="accessToken">调用接口凭证，上级/上游企业应用access_token</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static GetTokenResult CorpGetToken(string accessToken, GetTokenRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/gettoken?access_token={0}";
            return CommonJsonSend.Send<GetTokenResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 通过unionid和openid查询external_userid
        /// https://developer.work.weixin.qq.com/document/path/95818
        /// </summary>
        /// <param name="accessToken">上游应用的access_token</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static UnionIdToExternalUserIdResult UnionIdToExternalUserId(string accessToken, UnionIdToExternalUserIdRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/unionid_to_external_userid?access_token={0}";
            return CommonJsonSend.Send<UnionIdToExternalUserIdResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// unionid查询pending_id
        /// https://developer.work.weixin.qq.com/document/path/97357
        /// </summary>
        /// <param name="accessToken">上游应用的access_token</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static UnionIdToPendingIdResult UnionIdToPendingId(string accessToken, UnionIdToPendingIdRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/unionid_to_pending_id?access_token={0}";
            return CommonJsonSend.Send<UnionIdToPendingIdResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取上下游信息
        /// https://developer.work.weixin.qq.com/document/path/95820
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static GetChainListResult CorpGetChainList(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/get_chain_list?access_token={0}";
            return CommonJsonSend.Send<GetChainListResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 提交批量导入上下游联系人任务
        /// https://developer.work.weixin.qq.com/document/path/95821
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static ImportChainContactResult ImportChainContact(string accessToken, ImportChainContactRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/import_chain_contact?access_token={0}";
            return CommonJsonSend.Send<ImportChainContactResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取异步任务结果
        /// https://developer.work.weixin.qq.com/document/path/95823
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="jobid">异步任务id，最大长度为64字节</param>
        /// <returns></returns>
        public static GetResultResult GetResult(string accessToken, string jobid, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/corpgroup/getresult?access_token={0}&jobid={1}", accessToken.AsUrlData(), jobid.AsUrlData());
            return CommonJsonSend.Send<GetResultResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 移除企业
        /// https://developer.work.weixin.qq.com/document/path/95822
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static WorkJsonResult CorpRemoveCorp(string accessToken, RemoveCorpRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/remove_corp?access_token={0}";
            return CommonJsonSend.Send<WorkJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 查询成员自定义id
        /// 上级企业自建应用/代开发应用通过本接口查询成员自定义 id
        /// https://developer.work.weixin.qq.com/document/path/97441
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static GetChainUserCustomIdResult CorpGetChainUserCustomId(string accessToken, GetChainUserCustomIdRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/get_chain_user_custom_id?access_token={0}";
            return CommonJsonSend.Send<GetChainUserCustomIdResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取下级企业加入的上下游
        /// 上级企业自建应用/代开发应用通过本接口查询下级企业所在上下游
        /// https://developer.work.weixin.qq.com/document/path/97442
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static GetCorpSharedChainListResult GetCorpSharedChainList(string accessToken, GetCorpSharedChainListRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/get_corp_shared_chain_list?access_token={0}";
            return CommonJsonSend.Send<GetCorpSharedChainListResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取对接规则id列表
        /// 上下游系统应用可通过该接口获取企业上下游规则id列表
        /// https://developer.work.weixin.qq.com/document/path/95631
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static ListIdsResult RuleListIds(string accessToken, ListIdsRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/list_ids?access_token={0}";
            return CommonJsonSend.Send<ListIdsResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 删除对接规则
        /// 上下游系统应用可通过该接口删除企业上下游规则
        /// https://developer.work.weixin.qq.com/document/path/9562
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static WorkJsonResult RuleDeleteRule(string accessToken, DeleteRuleRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/delete_rule?access_token={0}";
            return CommonJsonSend.Send<WorkJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取对接规则详情
        /// 上下游系统应用可通过该接口获取企业上下游规则详情
        /// https://developer.work.weixin.qq.com/document/path/9563
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static GetRuleInfoResult RuleGetRuleInfo(string accessToken, GetRuleInfoRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/get_rule_info?access_token={0}";
            return CommonJsonSend.Send<GetRuleInfoResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 新增对接规则
        /// 上下游系统应用可通过该接口新增一条对接规则
        /// https://developer.work.weixin.qq.com/document/path/9564
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static AddRuleResult RuleAddRule(string accessToken, AddRuleRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/get_rule_info?access_token={0}";
            return CommonJsonSend.Send<AddRuleResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 更新对接规则
        /// 上下游应用可通过该接口修改一条对接规则
        /// https://developer.work.weixin.qq.com/document/path/9565
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static WorkJsonResult RuleModifyRule(string accessToken, ModifyRuleRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/modify_rule?access_token={0}";
            return CommonJsonSend.Send<WorkJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }
        #endregion


        #region 异步方法
        /// <summary>
        /// 获取应用共享信息
        /// 局校互联中的局端或者上下游中的上游企业通过该接口可以获取某个应用分享给的所有企业列表。
        /// 特别注意，对于有敏感权限的应用，需要下级/下游企业确认后才能共享成功，若下级/下游企业未确认，则不会存在于该接口的返回列表
        /// https://developer.work.weixin.qq.com/document/path/95813
        /// </summary>
        /// <param name="accessToken">调用接口凭证，上级/上游企业应用access_token</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<ListAppShareInfoResult> CorpListAppShareInfoAsync(string accessToken, ListAppShareInfoRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/list_app_share_info?access_token={0}";
            return await CommonJsonSend.SendAsync<ListAppShareInfoResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取下级/下游企业的access_token
        /// 获取应用可见范围内下级/下游企业的access_token，该access_token可用于调用下级/下游企业通讯录的只读接口。
        /// https://developer.work.weixin.qq.com/document/path/95816
        /// </summary>
        /// <param name="accessToken">调用接口凭证，上级/上游企业应用access_token</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<GetTokenResult> CorpGetTokenAsync(string accessToken, GetTokenRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/gettoken?access_token={0}";
            return await CommonJsonSend.SendAsync<GetTokenResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 通过unionid和openid查询external_userid
        /// https://developer.work.weixin.qq.com/document/path/95818
        /// </summary>
        /// <param name="accessToken">上游应用的access_token</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<UnionIdToExternalUserIdResult> UnionIdToExternalUserIdAsync(string accessToken, UnionIdToExternalUserIdRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/unionid_to_external_userid?access_token={0}";
            return await CommonJsonSend.SendAsync<UnionIdToExternalUserIdResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// unionid查询pending_id
        /// https://developer.work.weixin.qq.com/document/path/97357
        /// </summary>
        /// <param name="accessToken">上游应用的access_token</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<UnionIdToPendingIdResult> UnionIdToPendingIdAsync(string accessToken, UnionIdToPendingIdRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/unionid_to_pending_id?access_token={0}";
            return await CommonJsonSend.SendAsync<UnionIdToPendingIdResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取上下游信息
        /// https://developer.work.weixin.qq.com/document/path/95820
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<GetChainListResult> CorpGetChainListAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/get_chain_list?access_token={0}";
            return await CommonJsonSend.SendAsync<GetChainListResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 提交批量导入上下游联系人任务
        /// https://developer.work.weixin.qq.com/document/path/95821
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<ImportChainContactResult> ImportChainContactAsync(string accessToken, ImportChainContactRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/import_chain_contact?access_token={0}";
            return await CommonJsonSend.SendAsync<ImportChainContactResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取异步任务结果
        /// https://developer.work.weixin.qq.com/document/path/95823
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="jobid">异步任务id，最大长度为64字节</param>
        /// <returns></returns>
        public static async Task<GetResultResult> GetResultAsync(string accessToken, string jobid, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = string.Format(Config.ApiWorkHost + "/cgi-bin/corpgroup/getresult?access_token={0}&jobid={1}", accessToken.AsUrlData(), jobid.AsUrlData());
            return await CommonJsonSend.SendAsync<GetResultResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
        }

        /// <summary>
        /// 移除企业
        /// https://developer.work.weixin.qq.com/document/path/95822
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> CorpRemoveCorpAsync(string accessToken, RemoveCorpRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/remove_corp?access_token={0}";
            return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 查询成员自定义id
        /// 上级企业自建应用/代开发应用通过本接口查询成员自定义 id
        /// https://developer.work.weixin.qq.com/document/path/97441
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<GetChainUserCustomIdResult> CorpGetChainUserCustomIdAsync(string accessToken, GetChainUserCustomIdRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/corp/get_chain_user_custom_id?access_token={0}";
            return await CommonJsonSend.SendAsync<GetChainUserCustomIdResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取下级企业加入的上下游
        /// 上级企业自建应用/代开发应用通过本接口查询下级企业所在上下游
        /// https://developer.work.weixin.qq.com/document/path/97442
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<GetCorpSharedChainListResult> GetCorpSharedChainListAsync(string accessToken, GetCorpSharedChainListRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/get_corp_shared_chain_list?access_token={0}";
            return await CommonJsonSend.SendAsync<GetCorpSharedChainListResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取对接规则id列表
        /// 上下游系统应用可通过该接口获取企业上下游规则id列表
        /// https://developer.work.weixin.qq.com/document/path/95631
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<ListIdsResult> RuleListIdsAsync(string accessToken, ListIdsRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/list_ids?access_token={0}";
            return await CommonJsonSend.SendAsync<ListIdsResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 删除对接规则
        /// 上下游系统应用可通过该接口删除企业上下游规则
        /// https://developer.work.weixin.qq.com/document/path/9562
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> RuleDeleteRuleAsync(string accessToken, DeleteRuleRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/delete_rule?access_token={0}";
            return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 获取对接规则详情
        /// 上下游系统应用可通过该接口获取企业上下游规则详情
        /// https://developer.work.weixin.qq.com/document/path/9563
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<GetRuleInfoResult> RuleGetRuleInfoAsync(string accessToken, GetRuleInfoRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/get_rule_info?access_token={0}";
            return await CommonJsonSend.SendAsync<GetRuleInfoResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 新增对接规则
        /// 上下游系统应用可通过该接口新增一条对接规则
        /// https://developer.work.weixin.qq.com/document/path/9564
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<AddRuleResult> RuleAddRuleAsync(string accessToken, AddRuleRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/get_rule_info?access_token={0}";
            return await CommonJsonSend.SendAsync<AddRuleResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }

        /// <summary>
        /// 更新对接规则
        /// 上下游应用可通过该接口修改一条对接规则
        /// https://developer.work.weixin.qq.com/document/path/9565
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="data">请求参数</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> RuleModifyRuleAsync(string accessToken, ModifyRuleRequest data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = Config.ApiWorkHost + "/cgi-bin/corpgroup/rule/modify_rule?access_token={0}";
            return await CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
        }
        #endregion
    }
}
