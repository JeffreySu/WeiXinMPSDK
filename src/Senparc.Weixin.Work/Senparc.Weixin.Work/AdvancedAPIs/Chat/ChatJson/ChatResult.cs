/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：ChatResult.cs
    文件功能描述：会话接口返回结果
    
    
    创建标识：Senparc - 20150728

    修改标识：lishewen - 20180531
    修改描述：v1.6.1 创建会话返回结果

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Chat
{
    /// <summary>
    /// 创建会话返回结果
    /// </summary>
    public class CreateChatResult : WorkJsonResult
    {
        /// <summary>
        /// 会话id
        /// </summary>
        public string chatid { get; set; }
    }
    /// <summary>
    /// 获取会话返回结果
    /// </summary>
    public class GetChatResult : WorkJsonResult
    {
        /// <summary>
        /// 会话信息
        /// </summary>
        public ChatInfo chat_info { get; set; }
    }

    public class ChatInfo
    {
        /// <summary>
        /// 会话id
        /// </summary>
        public string chatid { get; set; }
        /// <summary>
        /// 会话标题
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 管理员userid
        /// </summary>
        public string owner { get; set; }
        /// <summary>
        /// 会话成员列表，成员用userid来标识
        /// </summary>
        public List<string> userlist { get; set; }
    }

    /// <summary>
    /// 设置成员新消息免打扰返回结果
    /// </summary>
    public class SetMuteResult : WorkJsonResult
    {
        /// <summary>
        /// 列表中不存在的成员会返回在invaliduser里，剩余合法成员会继续执行
        /// </summary>
        public List<string> invaliduser { get; set; }
    }
}