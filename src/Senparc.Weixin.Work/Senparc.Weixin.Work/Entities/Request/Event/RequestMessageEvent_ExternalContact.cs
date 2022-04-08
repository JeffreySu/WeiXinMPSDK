/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
    
    文件名：RequestMessageEvent_Change_ExternalContact.cs
    文件功能描述：上报企业客户变更事件
    
    
    创建标识：OrchesAdam - 2019119
    
    修改标识：Senparc - 20191226
    修改描述：整理格式，添加注释，分配版本号：v3.7.104.2 添加“上报企业客户变更事件”

    修改标识：OrchesAdam  - 20200430
    修改描述：v3.7.502 添加编辑企业客户事件

    修改标识：gokeiyou - 20201013
    修改描述：v3.7.604 添加外部联系人管理 > 客户管理相关接口

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities.Request.Event
{
    /// <summary>
    /// 上报企业客户变更事件 继承服务商接口
    /// </summary>
    public interface IRequestMessageEvent_Change_ExternalContact_Base : IRequestMessageEventBase, IThirdPartyAuthCorpIdInfo, IRequestMessageEventUserID
    {
        ExternalContactChangeType ChangeType
        {
            get;
        }
    }
    public interface IRequestMessageEventUserID
    {
        /// <summary>
        /// 企业服务人员的UserID
        /// </summary>
        string UserID { get; set; }
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

        public virtual ThirdPartyInfo InfoType 
        {
            get { return ThirdPartyInfo.CHANGE_EXTERNAL_CONTACT; }
        }

        public string TimeStamp { get; set; }
        /// <summary>
        /// 企业服务人员的UserID
        /// </summary>
        public string UserID { get; set; }
    }

    /// <summary>
    /// 添加企业客户事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_Add : RequestMessageEvent_Change_ExternalContact_Base
    {
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
        /// 外部联系人的userid
        /// </summary>
        public string ExternalUserID { get; set; }
        /// <summary>
        /// 如果是在职分配，分配成功会有原添加成员删除事件。此字段为DELETE_BY_TRANSFER
        /// <Source><![CDATA[DELETE_BY_TRANSFER]]></Source>
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
    /// 客户同意进行聊天内容存档(灰度)
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_MsgAudit : RequestMessageEvent_Change_ExternalContact_Add
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.msg_audit_approved;
    }

    /// <summary>
    /// 客户接替失败事件
    /// </summary>
    public class RequestMessageEvent_Change_ExternalContact_TransferFail : RequestMessageEvent_Change_ExternalContact_Base
    {
        public override ExternalContactChangeType ChangeType => ExternalContactChangeType.transfer_fail;
        /// <summary>
        /// 接替失败的企业服务人员的UserID
        /// </summary>
        public new string UserID { get; set; }
        /// <summary>
        /// 外部联系人的userid
        /// </summary>
        public string ExternalUserID { get; set; }
        /// <summary>
        /// 接替失败的原因, customer_refused-客户拒绝， customer_limit_exceed-接替成员的客户数达到上限
        /// </summary>
        public string FailReason { get; set; }
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
        transfer_fail
    }
}
