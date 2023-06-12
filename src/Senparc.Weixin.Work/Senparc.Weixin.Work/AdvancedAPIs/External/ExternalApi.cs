/*----------------------------------------------------------------
    Copyright(C) 2023 Senparc
    
    文件名：ExternalApi.cs
    文件功能描述：外部联系人接口
    
    
    创建标识：Senparc - 20181009
   
    修改标识：lishewen - 20200318
    修改描述：v3.7.401 新增“获取客户群列表”“获取客户群详情” API
    
    修改标识：gokeiyou - 20201013
    修改描述：v3.7.604 添加外部联系人管理 > 客户管理相关接口
    
    修改标识：Senparc - 20210321
    修改描述：v3.8.201 添加“配置客户联系「联系我」方式”接口
    
    修改标识：Senparc - 20210321
    修改描述：v3.9.101 添加“获取配置了客户联系功能的成员列表”接口

    修改标识：WangDrama - 20210630
    修改描述：v3.9.600 添加：外部联系人 - 客户群统计+联系客户+群直播+客户群事件 相关功能

    修改标识：WangDrama - 20210714
    修改描述：v3.11-preview1

    修改标识：WangDrama - 20210807
    修改描述：v3.12.1 添加企业微信入群欢迎语素材

    修改标识：IcedMango - 20211122
    修改描述：v3.14.1 “企业微信获取客户群详情”接口，增加群内成员名称返回参数

    修改标识：Senparc - 20220501
    修改描述：v3.15.2 添加“用户标签管理”接口

    修改标识：Senparc - 20220703
    修改描述：v3.15.5.1 修复 ExternalApi.GetFollowUserList() 接口请求类型为 GET

    修改标识：Senparc - 20220910
    修改描述：v3.15.7
              1、添加“创建企业群发”接口
              2、添加“获取企业的全部群发记录”接口
              3、添加“发送新客户欢迎语”接口

    修改标识：Senparc - 20220918
    修改描述：v3.15.9 
		      1、补充完整“客户联系「联系我」管理”接口
              2、添加“客户群「加入群聊」管理”接口

----------------------------------------------------------------*/

/*
    官方文档：https://work.weixin.qq.com/api/doc#13473
 */

using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.External;
using Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 外部联系人管理
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
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
        /// <param name="needName">是否需要返回群成员的名字group_chat.member_list.name。</param>
        /// <returns></returns>
        public static GroupChatGetResult GroupChatGet(string accessTokenOrAppKey, string chat_id, bool needName = false, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/get?access_token={0}", accessToken);
                var data = new
                {
                    chat_id,
                    need_name = needName ? "1" : "0"//0-不返回；1-返回。默认不返回
                };
                return CommonJsonSend.Send<GroupChatGetResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }

        #region 客户管理

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="userid">企业成员的userid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetExternalContactListResult GetExternalContactList(string accessTokenOrAppKey, string userid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/list?access_token={accessToken}&userid={userid}";

                return CommonJsonSend.Send<GetExternalContactListResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取客户详情
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="externalUserId">外部联系人的userid，注意不是企业成员的帐号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetExternalContactResultJson GetExternalContactInfo(string accessTokenOrAppKey, string externalUserId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get?access_token={accessToken}&external_userid={externalUserId}";

                return CommonJsonSend.Send<GetExternalContactResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 批量获取客户详情
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/92994</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="userid_list">（必须）企业成员的userid列表，字符串类型，最多支持100个</param>
        /// <param name="cursor">用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填</param>
        /// <param name="limit">返回的最大记录数，整型，最大值100，默认值50，超过最大值时取最大值</param>
        /// <param name="timeOut"></param>
        public static GetExternalContactInfoBatchResult GetExternalContactInfoBatch(string accessTokenOrAppKey, string[] userid_list, string cursor = null, int limit = 50, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/batch/get_by_user?access_token={accessToken}";

                var data = new
                {
                    userid_list,
                    cursor,
                    limit
                };

                return CommonJsonSend.Send<GetExternalContactInfoBatchResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 修改客户备注信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="rquest">请求报文</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult UpdateExternalContactRemark(string accessTokenOrAppKey, UpdateExternalContactRemarkRequest rquest, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/remark?access_token={accessToken}";

                return CommonJsonSend.Send<WorkJsonResult>(null, url, rquest, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 获取配置了客户联系功能的成员列表
        /// <para>权限说明：</para>
        /// <para>企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用</para>
        /// <para>第三方应用需具有“企业客户权限->客户基础信息”权限</para>
        /// <para>第三方/自建应用只能获取到可见范围内的配置了客户联系功能的成员。</para>
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/92571</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult GetFollowUserList(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get_follow_user_list?access_token={accessToken}";

                return CommonJsonSend.Send<WorkJsonResult>(null, url, null, CommonJsonSendType.GET, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 联系我与客户入群方式

        #region 客户联系「联系我」管理

        //文档：https://developer.work.weixin.qq.com/document/path/92228

        /// <summary>
        /// 配置客户联系「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="rquest">请求报文</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddContactWayResult AddContactWay(string accessTokenOrAppKey, AddContactWayRequest rquest, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/add_contact_way?access_token={accessToken}";

                return CommonJsonSend.Send<AddContactWayResult>(null, url, rquest, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取企业已配置的「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetContactWayResult AddContactWay(string accessTokenOrAppKey, string configId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get_contact_way?access_token={accessToken}";

                var data = new { config_id = configId };

                return CommonJsonSend.Send<GetContactWayResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取企业已配置的「联系我」列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="startTime">「联系我」创建起始时间戳, 默认为90天前</param>
        /// <param name="endTime">「联系我」创建结束时间戳, 默认为当前时间</param>
        /// <param name="cursor">分页查询使用的游标，为上次请求返回的 next_cursor</param>
        /// <param name="limit">每次查询的分页大小，默认为100条，最多支持1000条</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ListContactWayResult ListContactWay(string accessTokenOrAppKey, int? startTime = null, int? endTime = null, string? cursor = null, int? limit = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/list_contact_way?access_token={accessToken}";

                var data = new
                {
                    start_time = startTime,
                    end_time = endTime,
                    cursor,
                    limit
                };

                return CommonJsonSend.Send<ListContactWayResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 更新企业已配置的「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult UpdateContactWay(string accessTokenOrAppKey, UpdateContactWayRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/update_contact_way?access_token={accessToken}";


                return CommonJsonSend.Send<WorkJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 删除企业已配置的「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">	企业联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult DeleteContactWay(string accessTokenOrAppKey, string configId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/del_contact_way?access_token={accessToken}";

                var data = new { config_id = configId };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 结束临时会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="userid">企业成员的userid</param>
        /// <param name="externalUserid">客户的外部联系人userid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult CloseTempChat(string accessTokenOrAppKey, string userid, string externalUserid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/close_temp_chat?access_token={accessToken}";

                var data = new { userid = userid, external_userid = externalUserid };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 客户群「加入群聊」管理

        /// <summary>
        /// 配置客户群进群方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="requet">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupChat_AddJoinWayResult GroupChat_AddJoinWay(string accessTokenOrAppKey, GroupChat_AddJoinWayRequest requet, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/groupchat/add_join_way?access_token={accessToken}";

                return CommonJsonSend.Send<GroupChat_AddJoinWayResult>(null, url, requet, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 获取客户群进群方式配置
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupChat_GetJoinWayResult GroupChat_GetJoinWay(string accessTokenOrAppKey, string configId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/groupchat/get_join_way?access_token={accessToken}";

                var data = new { config_id = configId };

                return CommonJsonSend.Send<GroupChat_GetJoinWayResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 获取客户群进群方式配置
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="request">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult GroupChat_UpdateJoinWay(string accessTokenOrAppKey, GroupChat_UpdateJoinWayRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/groupchat/update_join_way?access_token={accessToken}";

                return CommonJsonSend.Send<WorkJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取客户群进群方式配置
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">企业联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult GroupChat_DelJoinWay(string accessTokenOrAppKey, string configId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/groupchat/del_join_way?access_token={accessToken}";

                var data = new { config_id = configId };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #endregion

        #region 统计管理
        /// <summary>
        /// 获取「联系客户统计」数据
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetUserBehaviorDataListResult GetUserBehaviorData(string accessTokenOrAppKey, GetUserBehaviorDataParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_user_behavior_data?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetUserBehaviorDataListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }
        /// <summary>
        /// 获取「群聊数据统计」数据 按群主聚合的方式
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetGroupChatListResult GroupChatStatisticOwner(string accessTokenOrAppKey, GetGroupChatParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/statistic?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetGroupChatListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }


        /// <summary>
        /// 获取「群聊数据统计」数据 按自然日聚合的方式
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetGroupChatGroupByDayListResult GroupChatStatisticGroupByDay(string accessTokenOrAppKey, GetGroupChatGroupByDayParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/statistic_group_by_day?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetGroupChatGroupByDayListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 客户标签管理

        #region 管理企业标签

        /// <summary>
        /// 获取企业标签库
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92117">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="tag_id">（可选）要查询的标签id</param>
        /// <param name="group_id">（可选）要查询的标签组id，返回该标签组以及其下的所有标签信息</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetCorpTagListResult GetCropTagList(string accessTokenOrAppKey, List<string> tag_id = null, List<string> group_id = null, int timeOut = Config.TIME_OUT)
        {
            //文档：https://developer.work.weixin.qq.com/document/path/94882
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_corp_tag_list?access_token={0}", accessToken);
                var data = new
                {
                    tag_id,
                    group_id
                };
                return CommonJsonSend.Send<GetCorpTagListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 添加企业客户标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92117">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddCorpTagResult AddCropTag(string accessTokenOrAppKey, AddCorpTagRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/add_corp_tag?access_token={0}", accessToken);
                return CommonJsonSend.Send<AddCorpTagResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 编辑企业客户标签
        /// <para>注意:修改后的标签组不能和已有的标签组重名，标签也不能和同一标签组下的其他标签重名。</para>
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92117">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="id">（必须）标签或标签组的id</param>
        /// <param name="name">（可选）新的标签或标签组名称，最长为30个字符</param>
        /// <param name="order">（可选）标签/标签组的次序值。order值大的排序靠前。有效的值范围是[0, 2^32)</param>
        /// <param name="agentid">授权方安装的应用agentid。仅旧的第三方多应用套件需要填此参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult EditCropTag(string accessTokenOrAppKey, string id, string name = null, int? order = null, int? agentid = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/edit_corp_tag?access_token={0}", accessToken);
                var data = new
                {
                    id,
                    name,
                    order,
                    agentid
                };
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 删除企业标签，企业可通过此接口删除客户标签库中的标签，或删除整个标签组。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92117">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="tag_id">（可选）标签的id列表</param>
        /// <param name="group_id">（可选）标签组的id列表</param>
        /// <param name="agentid">（可选）授权方安装的应用agentid。仅旧的第三方多应用套件需要填此参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult DeleteCropTag(string accessTokenOrAppKey, List<string> tag_id = null, List<string> group_id = null, int? agentid = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/del_corp_tag?access_token={0}", accessToken);
                var data = new
                {
                    tag_id,
                    group_id,
                    agentid
                };
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 管理企业规则组下的客户标签

        /// <summary>
        /// 获取指定规则组下的企业客户标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="tag_id">（可选）要查询的标签id</param>
        /// <param name="group_id">（可选）要查询的标签组id，返回该标签组以及其下的所有标签信息</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetStrategyTagListResult GetStrategyTagList(string accessTokenOrAppKey, List<string> tag_id = null, List<string> group_id = null, int timeOut = Config.TIME_OUT)
        {
            //文档：https://developer.work.weixin.qq.com/document/path/94882
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_strategy_tag_list?access_token={0}", accessToken);
                var data = new
                {
                    tag_id,
                    group_id
                };
                return CommonJsonSend.Send<GetStrategyTagListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 为指定规则组创建企业客户标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddStrategyTagResult AddStrategyTag(string accessTokenOrAppKey, AddStrategyTagRequest data, int timeOut = Config.TIME_OUT)
        {
            //文档：https://developer.work.weixin.qq.com/document/path/94882
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/add_strategy_tag?access_token={0}", accessToken);
                return CommonJsonSend.Send<AddStrategyTagResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 编辑指定规则组下的企业客户标签
        /// <para>企业可通过此接口编辑指定规则组下的客户标签/标签组的名称或次序值，但不可重新指定标签/标签组所属规则组。</para>
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="id">（必须）标签或标签组的id</param>
        /// <param name="name">（可选）新的标签或标签组名称，最长为30个字符</param>
        /// <param name="order">（可选）标签/标签组的次序值。order值大的排序靠前。有效的值范围是[0, 2^32)</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult EditStrategyTag(string accessTokenOrAppKey, string id, string name = null, int? order = null, int timeOut = Senparc.Weixin.Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/edit_strategy_tag?access_token={0}", accessToken);
                var data = new
                {
                    id,
                    name,
                    order
                };
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 删除指定规则组下的企业客户标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="tag_id">（可选）标签的id列表</param>
        /// <param name="group_id">（可选）标签组的id列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult DeleteStrategyTag(string accessTokenOrAppKey, List<string> tag_id = null, List<string> group_id = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/del_strategy_tag?access_token={0}", accessToken);
                var data = new
                {
                    tag_id,
                    group_id
                };
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        #endregion

        #region 编辑客户企业标签

        /// <summary>
        /// 编辑客户企业标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92118">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="userid">（必须）添加外部联系人的userid</param>
        /// <param name="external_userid">（必须）外部联系人userid</param>
        /// <param name="add_tag">（可选）要标记的标签列表</param>
        /// <param name="remove_tag">（可选）要移除的标签列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult DeleteStrategyTag(string accessTokenOrAppKey, string userid, string external_userid, List<string> add_tag = null, List<string> remove_tag = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/mark_tag?access_token={0}", accessToken);
                var data = new
                {
                    userid,
                    external_userid,
                    add_tag,
                    remove_tag
                };
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #endregion

        #region 朋友圈

        /// <summary>
        /// 获取企业全部的发表内容。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetMomentListResult GetMomentList(string accessTokenOrAppKey, GetMomentListParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_moment_list?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetMomentListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取企业发表的朋友圈成员执行情况
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetMomentTaskResult GetMomentTask(string accessTokenOrAppKey, GetMomentTaskParam data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_moment_task?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetMomentTaskResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 消息推送

        /// <summary>
        /// 创建企业群发
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/92135</para>
        /// <para></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="requestData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddMessageTemplateResult AddMsgTemplate(string accessTokenOrAppKey, AddMessageTemplateRequest requestData, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/add_msg_template?access_token={0}", accessToken);
                return CommonJsonSend.Send<AddMessageTemplateResult>(null, url, requestData, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取群发记录列表
        /// <para>https://developer.work.weixin.qq.com/document/path/93338</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="requestData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetGroupMsgListV2Result GetGroupMsgListV2(string accessTokenOrAppKey, GetGroupMsgListV2Request requestData, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_groupmsg_list_v2?access_token={0}", accessToken);
                return CommonJsonSend.Send<GetGroupMsgListV2Result>(null, url, requestData, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

#nullable enable

        /// <summary>
        /// 获取群发记录列表
        /// <para>https://developer.work.weixin.qq.com/document/path/93338</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="msgid">群发消息的id，通过<see href="https://developer.work.weixin.qq.com/document/path/93338#%E8%8E%B7%E5%8F%96%E7%BE%A4%E5%8F%91%E8%AE%B0%E5%BD%95%E5%88%97%E8%A1%A8">获取群发记录列表</see>接口返回</param>
        /// <param name="limit">返回的最大记录数，整型，最大值1000，默认值500，超过最大值时取默认值</param>
        /// <param name="cursor">用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetGroupMsgTaskResult GetGroupMsgTask(string accessTokenOrAppKey, string msgid, int? limit = null, string? cursor = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_groupmsg_task?access_token={0}", accessToken);
                var data = new
                {
                    msgid,
                    limit,
                    cursor
                };
                return CommonJsonSend.Send<GetGroupMsgTaskResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

#nullable disable


        /// <summary>
        /// 获取企业群发成员执行结果
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="msgid">群发消息的id，通过<see href="https://developer.work.weixin.qq.com/document/path/93338#%E8%8E%B7%E5%8F%96%E7%BE%A4%E5%8F%91%E8%AE%B0%E5%BD%95%E5%88%97%E8%A1%A8">获取群发记录列表</see>接口返回</param>
        /// <param name="userid">发送成员userid，通过<see href="https://developer.work.weixin.qq.com/document/path/93338#%E8%8E%B7%E5%8F%96%E7%BE%A4%E5%8F%91%E6%88%90%E5%91%98%E5%8F%91%E9%80%81%E4%BB%BB%E5%8A%A1%E5%88%97%E8%A1%A8">获取群发成员发送任务列表</see>接口返回</param>
        /// <param name="limit">返回的最大记录数，整型，最大值1000，默认值500，超过最大值时取默认值</param>
        /// <param name="cursor">用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetGroupMsgSendResultResult GetGroupMsgSendResult(string accessTokenOrAppKey, string msgid, string userid, int? limit = null, string? cursor = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_groupmsg_send_result?access_token={0}", accessToken);
                var data = new
                {
                    msgid,
                    userid,
                    limit,
                    cursor
                };
                return CommonJsonSend.Send<GetGroupMsgSendResultResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 发送新客户欢迎语
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="requestData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SendWelcomeMsg(string accessTokenOrAppKey, SendWelcomeMsgRequest requestData, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/send_welcome_msg?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, requestData, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

        #region 入群欢迎语素材管理

        /// <summary>
        /// 添加入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupWelcomeTemplateAddResult GroupWelcomeTemplateAdd(string accessTokenOrAppKey, GroupWelcomeTemplateAddRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/add?access_token={0}", accessToken);
                return CommonJsonSend.Send<GroupWelcomeTemplateAddResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 编辑入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult GroupWelcomeTemplateEdit(string accessTokenOrAppKey, GroupWelcomeTemplateEditRequest data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/edit?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="template_id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GroupWelcomeTemplateGetResult GroupWelcomeTemplateGet(string accessTokenOrAppKey, string template_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    template_id = template_id
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/get?access_token={0}", accessToken);
                return CommonJsonSend.Send<GroupWelcomeTemplateGetResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 删除入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="template_id"></param>
        /// <param name="agentid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult GroupWelcomeTemplateDel(string accessTokenOrAppKey, string template_id, long? agentid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    template_id = template_id,
                    agentid = agentid
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/del?access_token={0}", accessToken);
                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        #endregion

        #endregion

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
        public static async Task<GetFollowUserListResult> TransferExternalAsync(string accessTokenOrAppKey, string ExternalUserId, string handoverUserId, string takeoverUserId, int timeOut = Config.TIME_OUT)
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
                return await CommonJsonSend.SendAsync<GetFollowUserListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】获取外部联系人详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="ExternalUserId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
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
        /// <param name="needName">是否需要返回群成员的名字group_chat.member_list.name。</param>
        /// <returns></returns>
        public static async Task<GroupChatGetResult> GroupChatGetAsync(string accessTokenOrAppKey, string chat_id, bool needName = false, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/get?access_token={0}", accessToken);
                var data = new
                {
                    chat_id,
                    need_name = needName ? "1" : "0"//0-不返回；1-返回。默认不返回
                };
                return await CommonJsonSend.SendAsync<GroupChatGetResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        #region 客户管理

        /// <summary>
        /// 【异步方法】获取客户列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="userid">企业成员的userid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetExternalContactListResult> GetExternalContactListAsync(string accessTokenOrAppKey, string userid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/list?access_token={accessToken}&userid={userid}";

                return await CommonJsonSend.SendAsync<GetExternalContactListResult>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取客户详情
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="externalUserId">外部联系人的userid，注意不是企业成员的帐号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetExternalContactResultJson> GetExternalContactInfoAsync(string accessTokenOrAppKey, string externalUserId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get?access_token={accessToken}&external_userid={externalUserId}";

                return await CommonJsonSend.SendAsync<GetExternalContactResultJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】批量获取客户详情
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/92994</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="userid_list">（必须）企业成员的userid列表，字符串类型，最多支持100个</param>
        /// <param name="cursor">用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填</param>
        /// <param name="limit">返回的最大记录数，整型，最大值100，默认值50，超过最大值时取最大值</param>
        /// <param name="timeOut"></param>
        public static async Task<GetExternalContactInfoBatchResult> GetExternalContactInfoBatchAsync(string accessTokenOrAppKey, string[] userid_list, string cursor = null, int limit = 50, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/batch/get_by_user?access_token={accessToken}";

                var data = new
                {
                    userid_list,
                    cursor,
                    limit
                };

                return await CommonJsonSend.SendAsync<GetExternalContactInfoBatchResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 修改客户备注信息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="rquest">请求报文</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> UpdateExternalContactRemarkAsync(string accessTokenOrAppKey, UpdateExternalContactRemarkRequest rquest, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/remark?access_token={accessToken}";

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, rquest, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】获取配置了客户联系功能的成员列表
        /// <para>权限说明：</para>
        /// <para>企业需要使用“客户联系”secret或配置到“可调用应用”列表中的自建应用secret所获取的accesstoken来调用</para>
        /// <para>第三方应用需具有“企业客户权限->客户基础信息”权限</para>
        /// <para>第三方/自建应用只能获取到可见范围内的配置了客户联系功能的成员。</para>
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/92571</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetFollowUserListResult> GetFollowUserListAsync(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get_follow_user_list?access_token={accessToken}";

                return await CommonJsonSend.SendAsync<GetFollowUserListResult>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 统计管理
        /// <summary>
        /// 获取「联系客户统计」数据
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetUserBehaviorDataListResult> GetUserBehaviorDataAsync(string accessTokenOrAppKey, GetUserBehaviorDataParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_user_behavior_data?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetUserBehaviorDataListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }


        /// <summary>
        /// 获取「群聊数据统计」数据 按群主聚合的方式
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetGroupChatListResult> GroupChatStatisticOwnerAsync(string accessTokenOrAppKey, GetGroupChatParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/statistic?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetGroupChatListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }
        /// <summary>
        /// 获取「群聊数据统计」数据 按自然日聚合的方式
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetGroupChatGroupByDayListResult> GroupChatStatisticGroupByDayAsync(string accessTokenOrAppKey, GetGroupChatGroupByDayParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/groupchat/statistic_group_by_day?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetGroupChatGroupByDayListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion

        #region 客户标签管理

        #region 管理企业标签

        /// <summary>
        /// 【异步方法】获取企业标签库
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="tag_id">（可选）要查询的标签id</param>
        /// <param name="group_id">（可选）要查询的标签组id，返回该标签组以及其下的所有标签信息</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetCorpTagListResult> GetCropTagListAsync(string accessTokenOrAppKey, List<string> tag_id = null, List<string> group_id = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_corp_tag_list?access_token={0}", accessToken);
                var data = new
                {
                    tag_id,
                    group_id
                };
                return await CommonJsonSend.SendAsync<GetCorpTagListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 【异步方法】添加企业客户标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92117">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddCorpTagResult> AddCropTagAsync(string accessTokenOrAppKey, AddCorpTagRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/add_corp_tag?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<AddCorpTagResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】编辑企业客户标签
        /// <para>注意:修改后的标签组不能和已有的标签组重名，标签也不能和同一标签组下的其他标签重名。</para>
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92117">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="id">（必须）标签或标签组的id</param>
        /// <param name="name">（可选）新的标签或标签组名称，最长为30个字符</param>
        /// <param name="order">（可选）标签/标签组的次序值。order值大的排序靠前。有效的值范围是[0, 2^32)</param>
        /// <param name="agentid">授权方安装的应用agentid。仅旧的第三方多应用套件需要填此参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> EditCropTagAsync(string accessTokenOrAppKey, int id, string name = null, int? order = null, int? agentid = null, int timeOut = Senparc.Weixin.Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/edit_corp_tag?access_token={0}", accessToken);
                var data = new
                {
                    id,
                    name,
                    order,
                    agentid
                };
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】删除企业标签，企业可通过此接口删除客户标签库中的标签，或删除整个标签组。
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92117">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="tag_id">（可选）标签的id列表</param>
        /// <param name="group_id">（可选）标签组的id列表</param>
        /// <param name="agentid">（可选）授权方安装的应用agentid。仅旧的第三方多应用套件需要填此参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DeleteCropTagAsync(string accessTokenOrAppKey, List<string> tag_id = null, List<string> group_id = null, int? agentid = null, int timeOut = Senparc.Weixin.Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/del_corp_tag?access_token={0}", accessToken);
                var data = new
                {
                    tag_id,
                    group_id,
                    agentid
                };
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 管理企业规则组下的客户标签

        /// <summary>
        /// 【异步方法】获取指定规则组下的企业客户标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="tag_id">（可选）要查询的标签id</param>
        /// <param name="group_id">（可选）要查询的标签组id，返回该标签组以及其下的所有标签信息</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetStrategyTagListResult> GetStrategyTagListAsync(string accessTokenOrAppKey, List<string> tag_id = null, List<string> group_id = null, int timeOut = Config.TIME_OUT)
        {
            //文档：https://developer.work.weixin.qq.com/document/path/94882
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_strategy_tag_list?access_token={0}", accessToken);
                var data = new
                {
                    tag_id,
                    group_id
                };
                return await CommonJsonSend.SendAsync<GetStrategyTagListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 为指定规则组创建企业客户标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddStrategyTagResult> AddStrategyTagAsync(string accessTokenOrAppKey, AddStrategyTagRequest data, int timeOut = Config.TIME_OUT)
        {
            //文档：https://developer.work.weixin.qq.com/document/path/94882
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/add_strategy_tag?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<AddStrategyTagResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】编辑指定规则组下的企业客户标签
        /// <para>企业可通过此接口编辑指定规则组下的客户标签/标签组的名称或次序值，但不可重新指定标签/标签组所属规则组。</para>
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="id">（必须）标签或标签组的id</param>
        /// <param name="name">（可选）新的标签或标签组名称，最长为30个字符</param>
        /// <param name="order">（可选）标签/标签组的次序值。order值大的排序靠前。有效的值范围是[0, 2^32)</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> EditStrategyTagAsync(string accessTokenOrAppKey, string id, string name = null, int? order = null, int timeOut = Senparc.Weixin.Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/edit_strategy_tag?access_token={0}", accessToken);
                var data = new
                {
                    id,
                    name,
                    order
                };
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】删除指定规则组下的企业客户标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/94882">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="tag_id">（可选）标签的id列表</param>
        /// <param name="group_id">（可选）标签组的id列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DeleteStrategyTagAsync(string accessTokenOrAppKey, List<string> tag_id = null, List<string> group_id = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/del_strategy_tag?access_token={0}", accessToken);
                var data = new
                {
                    tag_id,
                    group_id
                };
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 编辑客户企业标签

        /// <summary>
        /// 【异步方法】编辑客户企业标签
        /// <para><see href="https://developer.work.weixin.qq.com/document/path/92118">官方文档</see></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="userid">（必须）添加外部联系人的userid</param>
        /// <param name="external_userid">（必须）外部联系人userid</param>
        /// <param name="add_tag">（可选）要标记的标签列表</param>
        /// <param name="remove_tag">（可选）要移除的标签列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DeleteStrategyTagAsync(string accessTokenOrAppKey, string userid, string external_userid, List<string> add_tag = null, List<string> remove_tag = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/mark_tag?access_token={0}", accessToken);
                var data = new
                {
                    userid,
                    external_userid,
                    add_tag,
                    remove_tag
                };
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion


        #endregion

        #region 朋友圈

        /// <summary>
        /// 获取企业全部的发表内容。
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetMomentListResult> GetMomentListAsync(string accessTokenOrAppKey, GetMomentListParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_moment_list?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetMomentListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取企业发表的朋友圈成员执行情况
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetMomentTaskResult> GetMomentTaskAsync(string accessTokenOrAppKey, GetMomentTaskParam data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_moment_task?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetMomentTaskResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion

        #region 联系我与客户入群方式


        #region 客户联系「联系我」管理

        //文档：https://developer.work.weixin.qq.com/document/path/92228

        /// <summary>
        /// 配置客户联系「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="rquest">请求报文</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddContactWayResult> AddContactWayAsync(string accessTokenOrAppKey, AddContactWayRequest rquest, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/add_contact_way?access_token={accessToken}";

                return await CommonJsonSend.SendAsync<AddContactWayResult>(null, url, rquest, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】获取企业已配置的「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async static Task<GetContactWayResult> AddContactWayAsync(string accessTokenOrAppKey, string configId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/get_contact_way?access_token={accessToken}";

                var data = new { config_id = configId };

                return await CommonJsonSend.SendAsync<GetContactWayResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 【异步方法】获取企业已配置的「联系我」列表
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="startTime">「联系我」创建起始时间戳, 默认为90天前</param>
        /// <param name="endTime">「联系我」创建结束时间戳, 默认为当前时间</param>
        /// <param name="cursor">分页查询使用的游标，为上次请求返回的 next_cursor</param>
        /// <param name="limit">每次查询的分页大小，默认为100条，最多支持1000条</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async static Task<ListContactWayResult> ListContactWayAsync(string accessTokenOrAppKey, int? startTime = null, int? endTime = null, string? cursor = null, int? limit = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/list_contact_way?access_token={accessToken}";

                var data = new
                {
                    start_time = startTime,
                    end_time = endTime,
                    cursor,
                    limit
                };

                return await CommonJsonSend.SendAsync<ListContactWayResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 【异步方法】更新企业已配置的「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async static Task<WorkJsonResult> UpdateContactWayAsync(string accessTokenOrAppKey, UpdateContactWayRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/update_contact_way?access_token={accessToken}";


                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 【异步方法】删除企业已配置的「联系我」方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">	企业联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async static Task<WorkJsonResult> DeleteContactWayAsync(string accessTokenOrAppKey, string configId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/del_contact_way?access_token={accessToken}";

                var data = new { config_id = configId };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 【异步方法】结束临时会话
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="userid">企业成员的userid</param>
        /// <param name="externalUserid">客户的外部联系人userid</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async static Task<WorkJsonResult> CloseTempChatAsync(string accessTokenOrAppKey, string userid, string externalUserid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/close_temp_chat?access_token={accessToken}";

                var data = new { userid = userid, external_userid = externalUserid };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        #endregion

        #region 客户群「加入群聊」管理

        /// <summary>
        /// 【异步方法】配置客户群进群方式
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="requet">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupChat_AddJoinWayResult> GroupChat_AddJoinWayAsync(string accessTokenOrAppKey, GroupChat_AddJoinWayRequest requet, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/groupchat/add_join_way?access_token={accessToken}";

                return await CommonJsonSend.SendAsync<GroupChat_AddJoinWayResult>(null, url, requet, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 【异步方法】获取客户群进群方式配置
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupChat_GetJoinWayResult> GroupChat_GetJoinWayAsync(string accessTokenOrAppKey, string configId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/groupchat/get_join_way?access_token={accessToken}";

                var data = new { config_id = configId };

                return await CommonJsonSend.SendAsync<GroupChat_GetJoinWayResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        /// 【异步方法】获取客户群进群方式配置
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="request">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> GroupChat_UpdateJoinWayAsync(string accessTokenOrAppKey, GroupChat_UpdateJoinWayRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/groupchat/update_join_way?access_token={accessToken}";

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, request, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取客户群进群方式配置
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证</param>
        /// <param name="configId">企业联系方式的配置id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> GroupChat_DelJoinWayAsync(string accessTokenOrAppKey, string configId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = $"{Config.ApiWorkHost}/cgi-bin/externalcontact/groupchat/del_join_way?access_token={accessToken}";

                var data = new { config_id = configId };

                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #endregion

        #region 消息推送

        /// <summary>
        /// 创建企业群发
        /// <para>文档：https://developer.work.weixin.qq.com/document/path/92135</para>
        /// <para></para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="requestData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddMessageTemplateResult> AddMsgTemplateAsync(string accessTokenOrAppKey, AddMessageTemplateRequest requestData, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/add_msg_template?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<AddMessageTemplateResult>(null, url, requestData, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取群发记录列表
        /// <para>https://developer.work.weixin.qq.com/document/path/93338</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="requestData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetGroupMsgListV2Result> GetGroupMsgListV2Async(string accessTokenOrAppKey, GetGroupMsgListV2Request requestData, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_groupmsg_list_v2?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GetGroupMsgListV2Result>(null, url, requestData, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

#nullable enable

        /// <summary>
        /// 获取群发记录列表
        /// <para>https://developer.work.weixin.qq.com/document/path/93338</para>
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="msgid">群发消息的id，通过<see href="https://developer.work.weixin.qq.com/document/path/93338#%E8%8E%B7%E5%8F%96%E7%BE%A4%E5%8F%91%E8%AE%B0%E5%BD%95%E5%88%97%E8%A1%A8">获取群发记录列表</see>接口返回</param>
        /// <param name="limit">返回的最大记录数，整型，最大值1000，默认值500，超过最大值时取默认值</param>
        /// <param name="cursor">用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetGroupMsgTaskResult> GetGroupMsgTaskAsync(string accessTokenOrAppKey, string msgid, int? limit = null, string? cursor = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_groupmsg_task?access_token={0}", accessToken);
                var data = new
                {
                    msgid,
                    limit,
                    cursor
                };
                return await CommonJsonSend.SendAsync<GetGroupMsgTaskResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

#nullable disable


        /// <summary>
        /// 获取企业群发成员执行结果
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="msgid">群发消息的id，通过<see href="https://developer.work.weixin.qq.com/document/path/93338#%E8%8E%B7%E5%8F%96%E7%BE%A4%E5%8F%91%E8%AE%B0%E5%BD%95%E5%88%97%E8%A1%A8">获取群发记录列表</see>接口返回</param>
        /// <param name="userid">发送成员userid，通过<see href="https://developer.work.weixin.qq.com/document/path/93338#%E8%8E%B7%E5%8F%96%E7%BE%A4%E5%8F%91%E6%88%90%E5%91%98%E5%8F%91%E9%80%81%E4%BB%BB%E5%8A%A1%E5%88%97%E8%A1%A8">获取群发成员发送任务列表</see>接口返回</param>
        /// <param name="limit">返回的最大记录数，整型，最大值1000，默认值500，超过最大值时取默认值</param>
        /// <param name="cursor">用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetGroupMsgSendResultResult> GetGroupMsgSendResultAsync(string accessTokenOrAppKey, string msgid, string userid, int? limit = null, string? cursor = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/get_groupmsg_send_result?access_token={0}", accessToken);
                var data = new
                {
                    msgid,
                    userid,
                    limit,
                    cursor
                };
                return await CommonJsonSend.SendAsync<GetGroupMsgSendResultResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 发送新客户欢迎语
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="requestData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SendWelcomeMsgAsync(string accessTokenOrAppKey, SendWelcomeMsgRequest requestData, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/send_welcome_msg?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, requestData, CommonJsonSendType.POST, timeOut, jsonSetting: new JsonSetting(true));
            }, accessTokenOrAppKey);
        }

        #region 入群欢迎语素材管理
        /// <summary>
        /// 添加入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupWelcomeTemplateAddResult> GroupWelcomeTemplateAddAsync(string accessTokenOrAppKey, GroupWelcomeTemplateAddRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/add?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GroupWelcomeTemplateAddResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 编辑入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> GroupWelcomeTemplateEditAsync(string accessTokenOrAppKey, GroupWelcomeTemplateEditRequest data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/edit?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="template_id"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GroupWelcomeTemplateGetResult> GroupWelcomeTemplateGetAsync(string accessTokenOrAppKey, string template_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    template_id = template_id
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/get?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<GroupWelcomeTemplateGetResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除入群欢迎语素材
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="template_id"></param>
        /// <param name="agentid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> GroupWelcomeTemplateDelAsync(string accessTokenOrAppKey, string template_id, long? agentid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new
                {
                    template_id = template_id,
                    agentid = agentid
                };
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/externalcontact/group_welcome_template/del?access_token={0}", accessToken);
                return await CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #endregion
    }
}
