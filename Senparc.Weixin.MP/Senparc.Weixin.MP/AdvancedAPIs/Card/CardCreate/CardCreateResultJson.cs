using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 创建卡券返回结果
    /// </summary>
    public class CardCreateResultJson : WxJsonResult
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
       public string card_id { get; set; } 
    }

    /// <summary>
    /// 获取颜色列表返回结果
    /// </summary>
    public class GetColorsResultJson : WxJsonResult
    {
        /// <summary>
        /// 颜色列表
        /// </summary>
        public List<Card_Color> colors { get; set; }
    }
    
    public class Card_Color
    {
        /// <summary>
        /// 可以填入的color 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 对应的颜色数值
        /// </summary>
        public string value { get; set; }
    }
    /// <summary>
    /// 生成卡券二维码返回结果
    /// </summary>
    public class CreateQRResultJson : WxJsonResult
    {
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket 可以在有效时间内换取二维码。
        /// </summary>
        public string ticket { get; set; }
    }

    /// <summary>
    /// 消耗code返回结果
    /// </summary>
    public class CardConsumeResultJson : WxJsonResult
    {
        public CardId card { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
    }

    public class CardId
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    /// code 解码
    /// </summary>
    public class CardDecryptResultJson : WxJsonResult
    {
        public string code { get; set; }
    }
}
