/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RecordJson.cs
    文件功能描述：客服记录消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 客服记录消息
    /// </summary>
    public class RecordJson 
    {
        /// <summary>
        /// 客服账号
        /// </summary>
        public string worker { get; set; }
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 操作ID（会话状态）
        /// </summary>
        public Opercode opercode { get; set; }
        /// <summary>
        /// 操作时间，UNIX时间戳
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 聊天记录
        /// </summary>
        public string text { get; set; }
    }

    /// <summary>
    /// 操作ID(会化状态）定义
    /// </summary>
    public enum Opercode
    {
        创建未接入会话 = 1000,
        接入会话 = 1001,
        主动发起会话 = 1002,
        转接会话 = 1003,
        关闭会话 = 1004,
        抢接会话 = 1005,
        公众号收到消息 = 2001,
        客服发送消息 = 2002,
        客服收到消息 = 2003
    }
}
