/*----------------------------------------------------------------
    Copyright (C) 2020 Senparc
    
    文件名：RequestMessageEvent_Change_ExternalContact.cs
    文件功能描述：上报企业客户变更事件
    
    
    创建标识：OrchesAdam - 2019119
    
    修改标识：Senparc - 20191226
    修改描述：整理格式，添加注释，分配版本号：v3.7.104.2 添加“上报企业客户变更事件”

    修改标识：OrchesAdam  - 20200430
    修改描述：v3.7.502 添加编辑企业客户事件

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities.Request.Event
{
    /// <summary>
    /// 上报企业客户变更事件
    /// </summary>
    public interface IRequestMessageEvent_Change_ExternalContact_Base : IRequestMessageEventBase
    {
        ExternalContactChangeType ChangeType
        {
            get;
        }
    }

    public class RequestMessageEvent_Change_ExternalContact_Base : RequestMessageEventBase, IRequestMessageEvent_Change_ExternalContact_Base
    {
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
        msg_audit_approved
    }
}
