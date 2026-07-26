/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：QueryBlockTemplateMessageResult.cs
    文件功能描述：QueryBlockTemplateMessageResult 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage
{
    /// <summary>
    /// 查询被拦截模板消息结果
    /// </summary>
    public class QueryBlockTemplateMessageResult : WxJsonResult
    {
        /// <summary>被拦截的模板消息信息。</summary>
        public BlockTemplateMessageInfo msginfo { get; set; }
    }

    /// <summary>
    /// BlockTemplateMessage 信息。
    /// </summary>
    public class BlockTemplateMessageInfo
    {
        /// <summary>记录唯一 ID，用于下一页的 largest_id。</summary>
        public long id { get; set; }

        /// <summary>被拦截的模板消息 ID。</summary>
        public string tmpl_msg_id { get; set; }

        /// <summary>模板消息标题。</summary>
        public string title { get; set; }

        /// <summary>模板消息内容。</summary>
        public string content { get; set; }

        /// <summary>下发时间戳。</summary>
        public long send_timestamp { get; set; }

        /// <summary>下发目标用户的 OpenId。</summary>
        public string openid { get; set; }
    }
}
