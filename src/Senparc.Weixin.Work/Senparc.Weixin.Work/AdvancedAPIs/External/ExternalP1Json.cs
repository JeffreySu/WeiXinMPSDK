/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalP1Json.cs
    文件功能描述：ExternalP1Json 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// OnJobTransferGroupChat 接口请求参数。
    /// </summary>
    public class OnJobTransferGroupChatRequest
    {
        public IList<string> chat_id_list { get; set; }
        public string new_owner { get; set; }
    }

    /// <summary>
    /// OnJobTransferGroupChat 接口返回结果。
    /// </summary>
    public class OnJobTransferGroupChatResult : WorkJsonResult
    {
        public IList<FailedTransferGroupChat> failed_chat_list { get; set; }
    }

    /// <summary>
    /// FailedTransferGroupChat 微信接口数据模型。
    /// </summary>
    public class FailedTransferGroupChat
    {
        public string chat_id { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }

    /// <summary>
    /// GroupMessageOperation 接口请求参数。
    /// </summary>
    public class GroupMessageOperationRequest
    {
        public string msgid { get; set; }
    }

    /// <summary>
    /// CancelMomentTask 接口请求参数。
    /// </summary>
    public class CancelMomentTaskRequest
    {
        public string moment_id { get; set; }
    }

    /// <summary>
    /// ServedExternalContactList 接口请求参数。
    /// </summary>
    public class ServedExternalContactListRequest
    {
        public string cursor { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>
    /// ServedExternalContactList 接口返回结果。
    /// </summary>
    public class ServedExternalContactListResult : WorkJsonResult
    {
        public IList<ServedExternalContact> info_list { get; set; }
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// ServedExternalContact 微信接口数据模型。
    /// </summary>
    public class ServedExternalContact
    {
        public bool is_customer { get; set; }
        public string tmp_openid { get; set; }
        public string external_userid { get; set; }
        public string name { get; set; }
        public string follow_userid { get; set; }
        public string chat_id { get; set; }
        public string chat_name { get; set; }
        public long add_time { get; set; }
    }
}
