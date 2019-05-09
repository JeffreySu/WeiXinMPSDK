/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：Receiver.cs
    文件功能描述：接收人
    
    
    创建标识：Senparc - 20150728
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 接收人
    /// </summary>
    public class Receiver
    {
        /// <summary>
        /// 接收人类型：single|group，分别表示：群聊|单聊
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 接收人的值，为userid|chatid，分别表示：成员id|会话id
        /// </summary>
        public string Id { get; set; }
    }
}
