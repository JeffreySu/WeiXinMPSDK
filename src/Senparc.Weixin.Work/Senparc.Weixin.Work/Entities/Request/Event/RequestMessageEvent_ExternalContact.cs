/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_Change_ExternalContact.cs
    文件功能描述：上报企业客户变更事件
    
    
    创建标识：OrchesAdam - 2019119
    
    修改标识：Senparc - 20191226
    修改描述：整理格式，添加注释，分配版本号：v3.7.104.2 添加“上报企业客户变更事件”

    修改标识：OrchesAdam  - 20200430
    修改描述：v3.7.502 添加编辑企业客户事件

    修改标识：gokeiyou - 20201013
    修改描述：v3.7.604 添加外部联系人管理 > 客户管理相关接口

    修改标识：Senparc - 20231026
    修改描述：v3.17.0 成员对外联系 > 客户消息通知处理

    修改标识：XiaopPoTian - 20231121
    修改描述：v3.18.1 删除企业客户事件 > 新加Source,删除客户的操作来源

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities.Request.Event
{
    /// <summary>
    /// 上报企业客户变更事件
    /// </summary>
    public interface IRequestMessageEvent_Change_ExternalContact_Base : IRequestMessageEventBase
    {
        string SuiteId { get; set; }
        string AuthCorpId { get; set; }
        ExternalContactChangeType ChangeType
        {
            get;
        }
    }

    public class RequestMessageEvent_Change_ExternalContact_Base : RequestMessageEventBase, IRequestMessageEvent_Change_ExternalContact_Base
    {
        public string SuiteId { get; set; }
        public string AuthCorpId { get; set; }
        public override Work.Event Event
        {
            get { return Work.Event.CHANGE_EXTERNAL_CONTACT; }
        }
        public virtual ExternalContactChangeType ChangeType
        {
            get { return ExternalContactChangeType.add_external_contact; }
        }
    }

    /// <summary>
    /// 添加企业客户事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Add : RequestMessageEvent_Change_ExternalContact_Base
    {
        /// <summary>
        /// 企业服务人员的UserID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 外部联系人的userid
        /// </summary>
        public string ExternalUserID { get; set; }
        /// <summary>
        /// 添加此用户的「联系我」方式配置的state参数，可用于识别添加此用户的渠道
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 欢迎语code，可用于发送欢迎语
        /// </summary>
        public string WelcomeCode { get; set; }
    }

    /// <summary>
    /// 编辑企业客户事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Modified : RequestMessageEvent_Change_ExternalContact_Base
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.edit_external_contact;

        /// <summary>
        /// 企业服务人员的UserID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 外部联系人的userid，注意不是企业成员的帐号
        /// </summary>
        public string ExternalUserID { get; set; }
        /// <summary>
        /// 添加此用户的「联系我」方式配置的state参数，可用于识别添加此用户的渠道
        /// </summary>
        public string State { get; set; }
    }

    /// <summary>
    /// 外部联系人免验证添加成员事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Add_Half : RequestMessageEvent_Change_ExternalContact_Add
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.add_half_external_contact;
    }

    /// <summary>
    /// 删除跟进成员事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Del : RequestMessageEvent_Change_ExternalContact_Base
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.del_external_contact;

        /// <summary>
        /// 企业服务人员的UserID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 外部联系人的userid
        /// </summary>
        public string ExternalUserID { get; set; }
        /// <summary>
        /// 删除客户的操作来源，DELETE_BY_TRANSFER表示此客户是因在职继承自动被转接成员删除
        /// 
        /// </summary>
        public string Source { get; set; }
    }

    /// <summary>
    /// 删除跟进成员事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Del_FollowUser : RequestMessageEvent_Change_ExternalContact_Del
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.del_follow_user;
    }

    /// <summary>
    /// 客户接替失败事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Transfer_Fail : RequestMessageEvent_Change_ExternalContact_Base
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.transfer_fail;

        /// <summary>
        /// 接替失败的原因, customer_refused-客户拒绝， customer_limit_exceed-接替成员的客户数达到上限
        /// </summary>
        public string FailReason { get; set; }
        /// <summary>
        /// 企业服务人员的UserID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 外部联系人的userid
        /// </summary>
        public string ExternalUserID { get; set; }
    }

    /// <summary>
    /// 客户同意进行聊天内容存档(灰度)
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_MsgAudit : RequestMessageEvent_Change_ExternalContact_Add
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.msg_audit_approved;
    }

    /// <summary>
    /// 客户群创建事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Create : RequestMessageEvent_Change_ExternalContact_Base
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.create;
    }

    /// <summary>
    /// 客户群变更事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Update : RequestMessageEvent_Change_ExternalContact_Base
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.update;

        /// <summary>
        /// 变更详情。目前有以下几种：
        /// add_member : 成员入群
        /// del_member : 成员退群
        /// change_owner : 群主变更
        /// change_name : 群名变更
        /// change_notice : 群公告变更
        /// </summary>
        public string UpdateDetail { get; set; }

        /// <summary>
        /// 当是成员入群时有值。表示成员的入群方式
        /// 0 - 由成员邀请入群（包括直接邀请入群和通过邀请链接入群）
        /// 3 - 通过扫描群二维码入群
        /// </summary>
        public string JoinScene { get; set; }

        /// <summary>
        /// 当是成员退群时有值。表示成员的退群方式
        /// 0 - 自己退群
        /// 1 - 群主/群管理员移出
        /// </summary>
        public string QuitScene { get; set; }

        /// <summary>
        /// 当是成员入群或退群时有值。表示成员变更数量
        /// </summary>
        public int MemChangeCnt { get; set; }
    }

    /// <summary>
    /// 客户群解散事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Dismiss : RequestMessageEvent_Change_ExternalContact_Base
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.dismiss;
    }


    /// <summary>
    /// 企业客户（外部联系人）变更事件
    /// </summary>
    public enum ExternalContactChangeType
    {
        /// <summary>
        /// 添加企业客户事件
        /// </summary>
        add_external_contact,
        /// <summary>
        /// 外部联系人免验证添加成员事件
        /// </summary>
        add_half_external_contact,
        /// <summary>
        /// 编辑企业客户事件
        /// </summary>
        edit_external_contact,
        /// <summary>
        /// 删除企业客户事件
        /// </summary>
        del_external_contact,
        /// <summary>
        /// 删除跟进成员事件
        /// </summary>
        del_follow_user,
        /// <summary>
        /// 客户同意进行聊天内容存档事件回调(此功能仍在灰度)
        /// </summary>
        msg_audit_approved,
        /// <summary>
        /// 客户接替失败事件
        /// </summary>
        transfer_fail,
        /// <summary>
        /// 客户群创建事件
        /// </summary>
        create,
        /// <summary>
        /// 客户群变更事件
        /// </summary>
        update,
        /// <summary>
        /// 客户群解散事件
        /// </summary>
        dismiss,
    }
}
