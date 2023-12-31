/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_Location.cs
    文件功能描述：事件之上报地理位置事件
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：WangDrama - 20210630
    修改描述：v3.9.600 添加群直播回调事件

    修改标识：XiaoPoTian - 20231122
    修改描述：v3.18.1 添加客户群变更事件(MemChangeList,LastMemVer,CurMemVer)

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 客户群变更事件
    /// </summary>
    public class RequestMessageEvent_Change_External_Chat_Base : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.CHANGE_EXTERNAL_CHAT; }
        }

        /// <summary>
        /// 群ID
        /// </summary>
        public string ChatId { get; set; }

        public virtual ExternalChatChangeType ChangeType { get { return ExternalChatChangeType.create; } }
    }

    public class RequestMessageEvent_Change_External_Chat_Create : RequestMessageEvent_Change_External_Chat_Base
    {

    }

    public class RequestMessageEvent_Change_External_Chat_Update : RequestMessageEvent_Change_External_Chat_Base
    {
        public RequestMessageEvent_Change_External_Chat_Update(XElement memChangeListElement)
        {
            if(memChangeListElement != null)
            {
                MemberChangeList = memChangeListElement.Elements().Select(u => u.Value).ToArray();
            }
        }
        public override ExternalChatChangeType ChangeType => ExternalChatChangeType.update;
        /// <summary>
        /// 变更详情
        /// </summary>
        public ExternalChatUpdateDetailType UpdateDetail { get; set; }
        /// <summary>
        /// 当是成员入群时有值。表示成员的入群方式0 - 由成员邀请入群（包括直接邀请入群和通过邀请链接入群）3 - 通过扫描群二维码入群
        /// </summary>
        public int JoinScene { get; set; }

        /// <summary>
        /// 当是成员退群时有值。表示成员的退群方式0 - 自己退群1 - 群主/群管理员移出
        /// </summary>
        public int QuitScene { get; set; }

        /// <summary>
        /// 当是成员入群或退群时有值。表示成员变更数量
        /// </summary>
        public int MemChangeCnt { get; set; }
        /// <summary>
        /// 当是成员入群或退群时有值。变更的成员列表
        /// </summary>
        public string[] MemberChangeList { get; set; }
        /// <summary>
        /// 当是成员入群或退群时有值。 变更前的群成员版本号
        /// </summary>
        public string LastMemVer { get; set; }
        /// <summary>
        /// 当是成员入群或退群时有值。变更后的群成员版本号
        /// </summary>
        public string CurMemVer { get; set; }

    }
    public class RequestMessageEvent_Change_External_Chat_Dismiss : RequestMessageEvent_Change_External_Chat_Base
    {
        public override ExternalChatChangeType ChangeType => ExternalChatChangeType.dismiss;
    }

    public enum ExternalChatChangeType
    {
        create,
        update,
        dismiss
    }


    public enum ExternalChatUpdateDetailType
    {
        /// <summary>
        /// 成员入群
        /// </summary>
        add_member,
        /// <summary>
        /// 成员退群
        /// </summary>
        del_member,
        /// <summary>
        /// 群主变更
        /// </summary>
        change_owner,
        /// <summary>
        /// 群名变更
        /// </summary>
        change_name,
        /// <summary>
        /// 群公告变更
        /// </summary>
        change_notice
    }
}
