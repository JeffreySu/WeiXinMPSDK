/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SendTemplateMessageResult.cs
    文件功能描述：发送模板消息结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// 发送模板消息结果
    /// </summary>
    public class SendTemplateMessageResult : WxJsonResult
    {
        /// <summary>
        /// msgid
        /// </summary>
        public long msgid { get; set; }
    }
}
