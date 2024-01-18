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
  
    文件名：GroupChatGetResult.cs
    文件功能描述：获取客户群详情 返回结果
    
    
    创建标识：lishewen - 20200318

    修改标识：XiaoPoTian - 20231122
    修改描述：v3.18.1 添加“当前群成员版本号。可以配合客户群变更事件减少主动调用本接口的次数”（member_version）


----------------------------------------------------------------*/


using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    ///     获取客户群详情 返回结果
    /// </summary>
    public class GroupChatGetResult : WorkJsonResult
    {
        /// <summary>
        ///     客户群详情
        /// </summary>
        public Group_Chat group_chat { get; set; }
    }

    /// <summary>
    ///     客户群详情
    /// </summary>
    public class Group_Chat
    {
        /// <summary>
        ///     客户群ID
        /// </summary>
        public string chat_id { get; set; }

        /// <summary>
        ///     群名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///     群主ID
        /// </summary>
        public string owner { get; set; }

        /// <summary>
        ///     群的创建时间
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        ///     群公告
        /// </summary>
        public string notice { get; set; }

        /// <summary>
        /// 当前群成员版本号。可以配合客户群变更事件减少主动调用本接口的次数
        /// </summary>
        public string member_version {  get; set; }

        /// <summary>
        ///     群成员列表
        /// </summary>
        public Member_List[] member_list { get; set; }

        /// <summary>
        ///     群管理员列表
        /// </summary>
        public Admin_List[] admin_list { get; set; }
    }

    /// <summary>
    ///     群成员列表
    /// </summary>
    public class Member_List
    {
        /// <summary>
        ///     群成员id
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        ///     在群里的昵称
        /// </summary>
        public string group_nickname { get; set; }

        /// <summary>
        ///     名字。仅当 need_name = 1 时返回
        ///     如果是微信用户，则返回其在微信中设置的名字
        ///     如果是企业微信联系人，则返回其设置对外展示的别名或实名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///     成员类型。
        ///     1 - 企业成员
        ///     2 - 外部联系人
        /// </summary>
        public int type { get; set; }

        /// <summary>
        ///     入群时间
        /// </summary>
        public int join_time { get; set; }

        /// <summary>
        ///     入群方式。
        ///     1 - 由成员邀请入群（直接邀请入群）
        ///     2 - 由成员邀请入群（通过邀请链接入群）
        ///     3 - 通过扫描群二维码入群
        /// </summary>
        public int join_scene { get; set; }


        /// <summary>
        ///     外部联系人在微信开放平台的唯一身份标识（微信unionid），通过此字段企业可将外部联系人与公众号/小程序用户关联起来。仅当群成员类型是微信用户（包括企业成员未添加好友），且企业或第三方服务商绑定了微信开发者ID有此字段
        /// </summary>
        public string unionid { get; set; }

        /// <summary>
        ///     邀请者。目前仅当是由本企业内部成员邀请入群时会返回该值
        /// </summary>
        public Invitor invitor { get; set; }
    }

    /// <summary>
    ///     邀请者。目前仅当是由本企业内部成员邀请入群时会返回该值
    /// </summary>
    public class Invitor
    {
        public string userid { get; set; }
    }

    /// <summary>
    ///     群管理员列表
    /// </summary>
    public class Admin_List
    {
        /// <summary>
        ///     群管理员userid
        /// </summary>
        public string userid { get; set; }
    }
}