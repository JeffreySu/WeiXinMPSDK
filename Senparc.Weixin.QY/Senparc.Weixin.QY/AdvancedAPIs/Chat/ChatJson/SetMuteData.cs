/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SetMuteData.cs
    文件功能描述：成员新消息免打扰参数
    
    
    创建标识：Senparc - 20150728
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.AdvancedAPIs.Chat
{
    /// <summary>
    /// 成员新消息免打扰参数
    /// </summary>
    public class UserMute
    {
        /// <summary>
        /// 成员UserID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 免打扰状态
        /// </summary>
        public Mute_Status status { get; set; }
    }
}